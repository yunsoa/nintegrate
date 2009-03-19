using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Reflection;
using System.Data;

namespace NIntegrate.Query.Command
{
    public abstract class QueryCommandBuilder : IQueryCommandBuilder
    {
        #region Private Fields

        private static readonly Dictionary<string, DbCommand> _cachedCommands
            = new Dictionary<string, DbCommand>();

        #endregion

        #region Private Methods

        private static void AppendResultColumns(StringBuilder sb, IList<IColumn> columns)
        {
            var separate = "";
            foreach (var column in columns)
            {
                sb.Append(separate);
                sb.Append(column.ToSelectColumnName());

                separate = ", ";
            }
        }

        #endregion

        #region Protected Methods

        protected string BuildCommandText(string cacheableSql, DbParameterCollection parameterCollection)
        {
            var splittedSql = cacheableSql.Split('?');
            var sb = new StringBuilder();
            sb.Append(splittedSql[0]);
            for (var i = 1; i < splittedSql.Length; ++i)
            {
                sb.Append(parameterCollection[i - 1].ParameterName);
                sb.Append(splittedSql[i]);
            }

            //replace table or column name quote charactors
            var quoteChars = GetDatabaseObjectNameQuoteCharactors();
            if (quoteChars.Length > 0)
            {
                var leftChar = quoteChars[0];
                var rightChar = (quoteChars.Length > 1 ? quoteChars[1] : leftChar);
                return sb.ToString().Replace('[', leftChar).Replace(']', rightChar);
            }

            return sb.ToString();
        }

        protected string BuildCacheableSql(string tableName, Criteria criteria, bool isCountCommand)
        {
            if (isCountCommand || criteria._skipResults == 0)
                return BuildNoPagingCacheableSql(tableName, criteria, isCountCommand);
            return BuildPagingCacheableSql(tableName, criteria);
        }

        protected abstract string BuildPagingCacheableSql(string tableName, Criteria criteria);

        protected abstract string BuildNoPagingCacheableSql(string tableName, Criteria criteria, bool isCountCommand);

        protected static void AppendSortBys(Criteria criteria, StringBuilder sb)
        {
            var separate = "";
            var en = criteria._sortBys.GetEnumerator();
            while (en.MoveNext())
            {
                sb.Append(separate);

                if (en.Current.Key.ColumnName.ToDatabaseObjectName() != en.Current.Key.Sql) continue;
                sb.Append(en.Current.Key.ToExpressionCacheableSql());
                if (en.Current.Value)
                {
                    sb.Append(" DESC");
                }
                separate = ", ";
            }
        }

        protected static void AppendConditions(Criteria criteria, StringBuilder sb)
        {
            for (var i = 0; i < criteria._conditions.Count; ++i)
            {
                if (i > 0)
                {
                    if (criteria._conditionAndOrs[i] == ConditionAndOr.And)
                    {
                        sb.Append(" AND ");
                    }
                    else
                    {
                        sb.Append(" OR ");
                    }
                }

                sb.Append(criteria._conditions[i].ToConditionCacheableSql());
            }
        }

        protected static void AppendFrom(string tableName, StringBuilder sb)
        {
            sb.Append("FROM ");
            sb.Append(tableName.ToDatabaseObjectName());
        }

        protected static void AppendResultColumns(Criteria criteria, StringBuilder sb)
        {
            if (criteria._resultColumns.Count == 0)
            {
                var noPredefinedColumns = true;
                var separate = "";
                foreach (FieldInfo field in criteria.GetType().GetFields())
                {
                    if (!typeof (IColumn).IsAssignableFrom(field.FieldType)) continue;
                    sb.Append(separate);
                    var column = (IColumn)field.GetValue(criteria);
                    sb.Append(column.ToSelectColumnName());

                    separate = ", ";
                    noPredefinedColumns = false;
                }
                foreach (var property in criteria.GetType().GetProperties())
                {
                    if (!property.CanRead || !typeof (IColumn).IsAssignableFrom(property.PropertyType)) continue;
                    sb.Append(separate);
                    var column = (IColumn)property.GetValue(criteria, null);
                    sb.Append(column.ToSelectColumnName());

                    separate = ", ";
                    noPredefinedColumns = false;
                }

                if (noPredefinedColumns)
                {
                    sb.Append("*");
                }
            }
            else
            {
                AppendResultColumns(sb, criteria._resultColumns);
            }
        }

        protected void BuildCommandParameters(Criteria criteria, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            if (criteria == null) return;
            foreach (var column in criteria._resultColumns)
            {
                AddExpressionParameters(column, parameterCollection, setParameterValueOnly);
            }

            foreach (var condition in criteria._conditions)
            {
                AddConditionParameters(condition, parameterCollection, setParameterValueOnly);
            }
        }

        protected void AddConditionParameters(Condition condition, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            AddExpressionParameters(condition.LeftExpression, parameterCollection, setParameterValueOnly);
            AddExpressionParameters(condition.RightExpression, parameterCollection, setParameterValueOnly);

            foreach (var linkedCondition in condition.LinkedConditions)
            {
                AddConditionParameters(linkedCondition, parameterCollection, setParameterValueOnly);
            }
        }

        protected void AddExpressionParameters(IExpression expr, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            var parameterExpr = expr as IParameterExpression;
            if (parameterExpr != null)
            {
                AddParameter(parameterExpr, parameterCollection, setParameterValueOnly);
            }

            foreach (var childExpr in expr.ChildExpressions)
            {
                AddExpressionParameters(childExpr, parameterCollection, setParameterValueOnly);
            }
        }

        protected abstract void AdjustParameterProperties(IParameterExpression parameterExpr, DbParameter parameter);

        protected void AddParameter(IParameterExpression parameterExpr, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            if (setParameterValueOnly)
            {
                foreach (DbParameter parameter in parameterCollection)
                {
                    if (parameter.Value != null) continue;
                    parameter.Value = parameterExpr.Value ?? DBNull.Value;
                    AdjustParameterProperties(parameterExpr, parameter);
                    break;
                }
            }
            else
            {
                var parameter = GetDbProviderFactory().CreateParameter();
                parameter.ParameterName = ToParameterName("p" + (parameterCollection.Count + 1));
                parameter.Value = parameterExpr.Value;
                AdjustParameterProperties(parameterExpr, parameter);
                parameterCollection.Add(parameter);
            }
        }

        protected virtual DbCommand CloneCommand(DbCommand cmd)
        {
            return (DbCommand)(cmd as ICloneable).Clone();
        }

        #endregion

        #region Public Methods

        public abstract string ToParameterName(string name);

        public abstract string GetDatabaseObjectNameQuoteCharactors();

        public abstract DbProviderFactory GetDbProviderFactory();

        public virtual DbCommand BuildQueryCommand(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            DbCommand cmd;

            var cacheableSql = BuildCacheableSql(criteria._tableName, criteria, false);
            if (!_cachedCommands.ContainsKey(cacheableSql))
            {
                lock (_cachedCommands)
                {
                    if (!_cachedCommands.ContainsKey(cacheableSql))
                    {
                        cmd = GetDbProviderFactory().CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        BuildCommandParameters(criteria, cmd.Parameters, false);
                        cmd.CommandText = BuildCommandText(cacheableSql, cmd.Parameters);

                        var cachedCmd = CloneCommand(cmd);
                        foreach (DbParameter p in cachedCmd.Parameters)
                        {
                            p.Value = null;
                        }
                        _cachedCommands.Add(cacheableSql, cachedCmd);

                        return cmd;
                    }
                }
            }

            cmd = CloneCommand(_cachedCommands[cacheableSql]);
            BuildCommandParameters(criteria, cmd.Parameters, true);

            return cmd;
        }

        public virtual DbCommand BuildCountCommand(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            DbCommand cmd;

            var cacheableSql = BuildCacheableSql(criteria._tableName, criteria, true);
            if (!_cachedCommands.ContainsKey(cacheableSql))
            {
                lock (_cachedCommands)
                {
                    if (!_cachedCommands.ContainsKey(cacheableSql))
                    {
                        cmd = GetDbProviderFactory().CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        BuildCommandParameters(criteria, cmd.Parameters, false);
                        cmd.CommandText = BuildCommandText(cacheableSql, cmd.Parameters);

                        var cachedCmd = CloneCommand(cmd);
                        foreach (DbParameter p in cachedCmd.Parameters)
                        {
                            p.Value = null;
                        }
                        _cachedCommands.Add(cacheableSql, cachedCmd);

                        return cmd;
                    }
                }
            }

            cmd = CloneCommand(_cachedCommands[cacheableSql]);
            BuildCommandParameters(criteria, cmd.Parameters, true);

            return cmd;
        }

        #endregion
    }
}
