﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Text;
using System.Data;

namespace NIntegrate.Data
{
    /// <summary>
    /// The base class for all QueryCommandBuilders which provides most resuable mfunctions.
    /// </summary>
    [ComVisible(false)]
    public abstract class QueryCommandBuilder
    {
        private static readonly Dictionary<string, DbCommand> _cachedCommands
            = new Dictionary<string, DbCommand>();
        private static readonly object _cachedCommandsLock = new object();

        #region Public Methods

        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <returns></returns>
        public abstract DbProviderFactory GetDbProviderFactory();

        public DbCommand BuildCommand(QueryCriteria criteria, bool isCountCommand)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            DbCommand cmd;

            var cacheableSql = BuildCacheableSql(criteria, isCountCommand);
            if (!_cachedCommands.ContainsKey(cacheableSql))
            {
                lock (_cachedCommandsLock)
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

        #region Protected Methods

        /// <summary>
        /// Format a name string to a parameter name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected abstract string ToParameterName(string name);

        /// <summary>
        /// Gets the database object name quote characters.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDatabaseObjectNameQuoteCharacters();

        /// <summary>
        /// Builds the command text.
        /// </summary>
        /// <param name="cacheableSql">The cacheable SQL.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <returns></returns>
        protected string BuildCommandText(string cacheableSql, DbParameterCollection parameterCollection)
        {
            var splittedSql = cacheableSql.Split('?');

            if (splittedSql.Length <= 1)
                return cacheableSql;

            var sb = new StringBuilder();
            sb.Append(splittedSql[0]);
            for (var i = 1; i < splittedSql.Length; ++i)
            {
                sb.Append(parameterCollection[i - 1].ParameterName);
                sb.Append(splittedSql[i]);
            }

            //replace table or column name quote charactors
            var quoteChars = GetDatabaseObjectNameQuoteCharacters();
            if (quoteChars.Length > 0)
            {
                var leftChar = quoteChars[0];
                var rightChar = (quoteChars.Length > 1 ? quoteChars[1] : leftChar);
                return sb.ToString().Replace('[', leftChar).Replace(']', rightChar);
            }

            return sb.ToString();
        }

        protected string BuildCacheableSql(QueryCriteria criteria, bool isCountCommand)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (isCountCommand)
                return BuildNoPagingCacheableSql(criteria, isCountCommand);

            switch (criteria.QueryType)
            {
                case QueryType.Select:
                    if (criteria.SkipResults == 0)
                        return BuildNoPagingCacheableSql(criteria, isCountCommand);
                    return BuildPagingCacheableSql(criteria);
                case QueryType.Insert:
                    return BuildInsertCacheableSql(criteria);
                case QueryType.Update:
                    return BuildUpdateCacheableSql(criteria);
                case QueryType.Delete:
                    return BuildDeleteCacheableSql(criteria);
            }

            return null;
        }

        private string BuildInsertCacheableSql(QueryCriteria criteria)
        {
            var columnValues = GetColumnValues(criteria.Assignments);
            
            var sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.Append(criteria.TableName.ToDatabaseObjectName());
            sb.Append(" (");
            var separate = "";
            foreach (var columnValue in columnValues)
            {
                sb.Append(separate);
                sb.Append(columnValue.Key);
                separate = ", ";
            }
            sb.Append(") VALUES (");
            separate = "";
            foreach (var columnValue in columnValues)
            {
                sb.Append(separate);
                sb.Append(ReferenceEquals(columnValue.Value, null)
                              ? "NULL"
                              : columnValue.Value.ToExpressionCacheableSql());
                separate = ", ";
            }
            sb.Append(")");

            return sb.ToString();
        }

        private Dictionary<string, IExpression> GetColumnValues(ICollection<Assignment> assignments)
        {
            var result = new Dictionary<string, IExpression>();

            if (assignments != null)
            {
                foreach (var assignment in assignments)
                {
                    result[assignment.LeftColumn.ColumnName.ToDatabaseObjectName()] = assignment.RightExpression;

                    var linkedColumnValues = GetColumnValues(assignment.LinkedAssignments);
                    foreach (var linkedColumnValue in linkedColumnValues)
                    {
                        result[linkedColumnValue.Key] = linkedColumnValue.Value;
                    }
                }
            }

            return result;
        }

        private string BuildUpdateCacheableSql(QueryCriteria criteria)
        {
            var columnValues = GetColumnValues(criteria.Assignments);

            var sb = new StringBuilder();
            sb.Append("UPDATE ");
            sb.Append(criteria.TableName.ToDatabaseObjectName());
            sb.Append(" SET ");
            var separate = "";
            foreach (var columnValue in columnValues)
            {
                sb.Append(separate);
                sb.Append(columnValue.Key);
                sb.Append(" = ");
                sb.Append(ReferenceEquals(columnValue.Value, null)
                              ? "NULL"
                              : columnValue.Value.ToExpressionCacheableSql());
                separate = ", ";
            }
            if (criteria.Conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }

            return sb.ToString();
        }

        private string BuildDeleteCacheableSql(QueryCriteria criteria)
        {
            var sb = new StringBuilder();
            sb.Append("DELETE FROM ");
            sb.Append(criteria.TableName.ToDatabaseObjectName());
            if (criteria.Conditions.Count > 0)
            {
                sb.Append(" ");
                sb.Append("WHERE ");
                AppendConditions(criteria, sb);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Builds the paging cacheable SQL.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        protected abstract string BuildPagingCacheableSql(QueryCriteria criteria);

        protected abstract string BuildNoPagingCacheableSql(QueryCriteria criteria, bool isCountCommand);

        /// <summary>
        /// Appends the sort bys.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sb">The sb.</param>
        protected static void AppendSortBys(QueryCriteria criteria, StringBuilder sb)
        {
            var separate = "";
            var en = criteria.SortBys.GetEnumerator();
            while (en.MoveNext())
            {
                sb.Append(separate);
                sb.Append(en.Current.Key.ToExpressionCacheableSql());
                if (en.Current.Value)
                {
                    sb.Append(" DESC");
                }
                separate = ", ";
            }
        }

        /// <summary>
        /// Appends the conditions.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sb">The sb.</param>
        protected static void AppendConditions(QueryCriteria criteria, StringBuilder sb)
        {
            for (var i = 0; i < criteria.Conditions.Count; ++i)
            {
                if (i > 0)
                {
                    if (criteria.ConditionAndOrs[i] == ConditionAndOr.And)
                    {
                        sb.Append(" AND ");
                    }
                    else
                    {
                        sb.Append(" OR ");
                    }
                }

                sb.Append(criteria.Conditions[i].ToConditionCacheableSql());
            }
        }

        /// <summary>
        /// Appends from.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="sb">The sb.</param>
        protected static void AppendFrom(string tableName, StringBuilder sb)
        {
            sb.Append("FROM ");
            sb.Append(tableName.ToDatabaseObjectName());
        }

        /// <summary>
        /// Appends the result columns.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="sb">The sb.</param>
        protected static void AppendResultColumns(QueryCriteria criteria, StringBuilder sb)
        {
            if (criteria.ResultColumns.Count == 0)
            {
                if (criteria.PredefinedColumns.Count > 0)
                {
                    var separate = "";
                    foreach (var column in criteria.PredefinedColumns)
                    {
                        sb.Append(separate);
                        sb.Append(column.ToSelectColumnName());

                        separate = ", ";
                    }
                }
                else
                {
                    sb.Append("*");
                }
            }
            else
            {
                AppendResultColumns(sb, criteria.ResultColumns);
            }
        }

        /// <summary>
        /// Builds the command parameters.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="setParameterValueOnly">if set to <c>true</c> [set parameter value only].</param>
        protected void BuildCommandParameters(QueryCriteria criteria, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            if (criteria == null) return;
            foreach (var column in criteria.ResultColumns)
            {
                AddExpressionParameters(column, parameterCollection, setParameterValueOnly);
            }

            foreach (var assignment in criteria.Assignments)
            {
                AddAssignmentParameters(assignment, parameterCollection, setParameterValueOnly);
            }

            foreach (var condition in criteria.Conditions)
            {
                AddConditionParameters(condition, parameterCollection, setParameterValueOnly);
            }
        }

        /// <summary>
        /// Adds the assignment parameters.
        /// </summary>
        /// <param name="assignment">The assignment.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="setParameterValueOnly">if set to <c>true</c> [set parameter value only].</param>
        protected void AddAssignmentParameters(Assignment assignment, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            AddExpressionParameters(assignment.LeftColumn, parameterCollection, setParameterValueOnly);
            if (!ReferenceEquals(assignment.RightExpression, null))
                AddExpressionParameters(assignment.RightExpression, parameterCollection, setParameterValueOnly);

            foreach (var linkedAssignment in assignment.LinkedAssignments)
            {
                AddAssignmentParameters(linkedAssignment, parameterCollection, setParameterValueOnly);
            }
        }

        /// <summary>
        /// Adds the condition parameters.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="setParameterValueOnly">if set to <c>true</c> [set parameter value only].</param>
        protected void AddConditionParameters(Condition condition, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            AddExpressionParameters(condition.LeftExpression, parameterCollection, setParameterValueOnly);
            if (!ReferenceEquals(condition.RightExpression, null))
                AddExpressionParameters(condition.RightExpression, parameterCollection, setParameterValueOnly);

            foreach (var linkedCondition in condition.LinkedConditions)
            {
                AddConditionParameters(linkedCondition, parameterCollection, setParameterValueOnly);
            }
        }

        /// <summary>
        /// Adds the expression parameters.
        /// </summary>
        /// <param name="expr">The expr.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="setParameterValueOnly">if set to <c>true</c> [set parameter value only].</param>
        protected void AddExpressionParameters(IExpression expr, DbParameterCollection parameterCollection, bool setParameterValueOnly)
        {
            var parameterExpr = expr as IParameterExpression;
            if (parameterExpr != null)
            {
                AddParameter(parameterExpr, parameterCollection, setParameterValueOnly);
            }

            if (expr.ChildExpressions != null)
            {
                foreach (var childExpr in expr.ChildExpressions)
                {
                    AddExpressionParameters(childExpr, parameterCollection, setParameterValueOnly);
                }
            }
        }

        /// <summary>
        /// Adjusts the parameter properties.
        /// </summary>
        /// <param name="parameterExpr">The parameter expr.</param>
        /// <param name="parameter">The parameter.</param>
        protected abstract void AdjustParameterProperties(IParameterExpression parameterExpr, DbParameter parameter);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="parameterExpr">The parameter expr.</param>
        /// <param name="parameterCollection">The parameter collection.</param>
        /// <param name="setParameterValueOnly">if set to <c>true</c> [set parameter value only].</param>
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

        /// <summary>
        /// Clones the command.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <returns></returns>
        protected virtual DbCommand CloneCommand(DbCommand cmd)
        {
            var cloneable = cmd as ICloneable;
            if (cloneable != null)
                return (DbCommand)cloneable.Clone();

            return null;
        }

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
    }
}