using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using NIntegrate.Data;
using System.Data;
using NIntegrate.Web.EventArgs;
using System.Globalization;

namespace NIntegrate.Web
{
    internal sealed class QueryDataSourceView : DataSourceView
    {
        private readonly QueryDataSource _owner;

        #region Constructors

        internal QueryDataSourceView(QueryDataSource owner)
            : base(owner, "Default")
        {
            if (owner == null)
                throw new ArgumentNullException("owner");

            _owner = owner;
        }

        #endregion

        #region Public Properties

        public override bool CanInsert
        {
            get
            {
                return !_owner.Criteria.ReadOnly;
            }
        }

        public override bool CanUpdate
        {
            get
            {
                return !_owner.Criteria.ReadOnly;
            }
        }

        public override bool CanDelete
        {
            get
            {
                return !_owner.Criteria.ReadOnly;
            }
        }

        public override bool CanRetrieveTotalRowCount
        {
            get
            {
                return true;
            }
        }

        public override bool CanPage
        {
            get
            {
                return true;
            }
        }

        public override bool CanSort
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region Override CRUD Methods

        protected override int ExecuteInsert(IDictionary values)
        {
            if (_owner.Criteria == null)
                throw new ArgumentException("Missing QueryTableType or Criteria setting on QueryDataSource");
            if (values == null || values.Count == 0)
                throw new ArgumentNullException("values");

            var insertingArgs = new DataSourceInsertingEventArgs(values);
            _owner.OnInserting(insertingArgs);
            if (insertingArgs.Cancel)
                return 0;

            var criteria = CreateInsertCriteria(values);
            var affectedRows = _owner.QueryService.Execute(criteria, false);

            var statusArgs = new DataSourceStatusEventArgs(this, affectedRows);
            _owner.OnInserted(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            if (_owner.Criteria == null)
                throw new ArgumentException("Missing QueryTableType or Criteria setting on QueryDataSource");
            if (keys == null || keys.Count == 0)
                throw new ArgumentNullException("keys");
            if (values == null || values.Count == 0)
                throw new ArgumentNullException("values");

            var updatingArgs = new DataSourceUpdatingEventArgs(GetReadOnlyDictionary(keys), values, oldValues);
            _owner.OnUpdating(updatingArgs);
            if (updatingArgs.Cancel)
                return 0;

            if (_owner.ConflictDetection == ConflictOptions.CompareAllValues)
            {
                DetectCompareAllValuesConflicts(oldValues, keys);
            }

            var criteria = CreateUpdateCriteria(keys, values);
            var affectedRows = _owner.QueryService.Execute(criteria, false);

            var statusArgs = new DataSourceStatusEventArgs(this, affectedRows);
            _owner.OnUpdated(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
        {
            if (_owner.Criteria == null)
                throw new ArgumentException("Missing QueryTableType or Criteria setting on QueryDataSource");
            if (keys == null || keys.Count == 0)
                throw new ArgumentNullException("keys");

            var deletingArgs = new DataSourceDeletingEventArgs(GetReadOnlyDictionary(keys), oldValues);
            _owner.OnDeleting(deletingArgs);
            if (deletingArgs.Cancel)
                return 0;

            if (_owner.ConflictDetection == ConflictOptions.CompareAllValues)
            {
                DetectCompareAllValuesConflicts(oldValues, keys);
            }

            var criteria = CreateDeleteCriteria(keys);

            var affectedRows = _owner.QueryService.Execute(criteria, false);
            var statusArgs = new DataSourceStatusEventArgs(this, affectedRows);
            _owner.OnDeleted(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
            if (_owner.Criteria == null)
                throw new ArgumentException("Missing QueryTableType or Criteria setting on QueryDataSource");

            var adjustedParameterValues = _owner.CriteriaParameters.GetValues(HttpContext.Current, _owner);
            var en = adjustedParameterValues.GetEnumerator();
            while (en.MoveNext())
            {
                _owner.Criteria.UpdateIdentifiedParameterValue(en.Key.ToString(), en.Value);
            }
            var selectingArgs = new DataSourceSelectingEventArgs(_owner.Criteria, adjustedParameterValues);
            _owner.OnSelecting(selectingArgs);
            if (selectingArgs.Cancel)
            {
                return selectingArgs.Result != null ? new DataView(selectingArgs.Result) : null;
            }

            var criteria = _owner.Criteria.Clone();

            if (arguments != null && arguments != DataSourceSelectArguments.Empty)
            {
                //adjust criteria according to arguments
                if (arguments.RetrieveTotalRowCount)
                {
                    arguments.TotalRowCount = _owner.QueryService.Execute(criteria, true);
                    _owner.LastTotalCount = arguments.TotalRowCount;
                }
                if (arguments.MaximumRows > 0)
                    criteria.SetMaxResults(arguments.MaximumRows);
                if (arguments.StartRowIndex > 0)
                    criteria.SetSkipResults(arguments.StartRowIndex);
                if (!string.IsNullOrEmpty(arguments.SortExpression))
                {
                    if (_owner.AlwaysAppendDefaultSortBysWhenSorting)
                        InsertSortExpressinAtTopOfSortBys(arguments.SortExpression, criteria);
                    else
                    {
                        criteria.SortBys.Clear();
                        AppendSortExpression(criteria, arguments.SortExpression);
                    }
                }
            }

            var result = _owner.QueryService.Query(criteria);

            _owner.OnSelected(new DataSourceSelectedEventArgs(result));

            return new DataView(result);
        }

        #endregion

        #region Non-Public Methods

        internal void SelectParametersChangedEventHandler(object o, System.EventArgs e)
        {
            OnDataSourceViewChanged(System.EventArgs.Empty);
        }

        private static Condition BuildLoadByKeysCondition(IDictionary keys)
        {
            if (keys == null || keys.Count == 0)
                throw new ArgumentNullException("keys");

            var en = keys.GetEnumerator();
            if (en.MoveNext())
            {
                var condition = CreateColumnEqualsCondition(en.Key.ToString(), en.Value);
                while (en.MoveNext())
                    condition = condition.And(CreateColumnEqualsCondition(en.Key.ToString(), en.Value));
                return condition;
            }

            return null;
        }

        private static Condition CreateColumnEqualsCondition(string columnName, object value)
        {
            var childExprs = new List<IExpression> { ExpressionHelper.CreateParameterExpression(value) };
            var condition = new Condition(new Int32Expression(columnName.ToDatabaseObjectName() + " " +
                (childExprs[0] is NullExpression ? "IS" : "=") + " ?", childExprs),
                ExpressionOperator.None, null);
            return condition;
        }

        private static IDictionary GetReadOnlyDictionary(IDictionary dictionary)
        {
            var result = new OrderedDictionary();
            foreach (DictionaryEntry entry in dictionary)
            {
                result.Add(entry.Key, entry.Value);
            }
            return result.AsReadOnly();
        }

        private static void InsertSortExpressinAtTopOfSortBys(string sortExpression, QueryCriteria criteria)
        {
            if (criteria.SortBys.Count == 0)
            {
                AppendSortExpression(criteria, sortExpression);
                return;
            }

            var sortBys = new Dictionary<IColumn, bool>();
            var en = criteria.SortBys.GetEnumerator();
            while (en.MoveNext())
                sortBys.Add((IColumn)en.Current.Key.Clone(), en.Current.Value);
            criteria.SortBys.Clear();
            AppendSortExpression(criteria, sortExpression);
            en = sortBys.GetEnumerator();
            while (en.MoveNext())
                criteria.SortBys.Add(en.Current.Key, en.Current.Value);
        }

        private static void AppendSortExpression(QueryCriteria criteria, string sortExpression)
        {
            var sortBy = sortExpression;
            var isDesc = false;
            if (sortExpression.EndsWith(" DESC", StringComparison.OrdinalIgnoreCase))
            {
                isDesc = true;
                sortBy = sortExpression.Substring(0, sortExpression.Length - " DESC".Length);
            }
            else if (sortExpression.EndsWith(" ASC", StringComparison.OrdinalIgnoreCase))
            {
                sortBy = sortExpression.Substring(0, sortExpression.Length - " ASC".Length);
            }
            criteria.SortBys.Add(new Int32Column(sortBy), isDesc);
        }

        private static object TransformType(Type targetType, object value, bool nullable)
        {
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            if (value == DBNull.Value)
                value = null;

            if (value != null)
            {
                var valueType = value.GetType();
                if (valueType != typeof(string))
                {
                    if (valueType == targetType)
                        return value;

                    return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
                }
            }

            if (value == null && nullable)
                return value;

            var stringValue = value as string;
            if (stringValue == null && !nullable)
                return ExpressionHelper.DefaultValue(targetType);

            if (targetType == typeof(bool) || targetType == typeof(bool?))
            {
                switch (stringValue)
                {
                    case "0":
                        stringValue = "false";
                        break;
                    case "1":
                        stringValue = "true";
                        break;
                }
            }
            var converter = TypeDescriptor.GetConverter(targetType);
            try
            {
                value = converter.ConvertFromString(stringValue);
            }
            catch
            {
                throw new InvalidOperationException("Cannot convert type from " + typeof(string).FullName + " to " + targetType.FullName);
            }
            return value;
        }

        private IColumn GetColumn(string columnName)
        {
            foreach (var column in _owner.Criteria.PredefinedColumns)
            {
                if (string.Compare(columnName, column.ColumnName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return column;
                }
            }

            return null;
        }

        private QueryCriteria CreateInsertCriteria(IDictionary values)
        {
            var assignments = new List<Assignment>(values.Count);
            var en = values.GetEnumerator();
            while (en.MoveNext())
            {
                assignments.Add(
                    new Assignment(
                        GetColumn(en.Key.ToString()),
                        ExpressionHelper.CreateParameterExpression(en.Value)));
            }

            var criteria = new QueryCriteria(_owner.Criteria.TableName,
                                             _owner.Criteria.ConnectionStringName,
                                             false, null);
            criteria.Insert(assignments.ToArray());

            return criteria;
        }

        private QueryCriteria CreateUpdateCriteria(IDictionary keys, IDictionary values)
        {
            var assignments = new List<Assignment>(values.Count);
            var en = values.GetEnumerator();
            while (en.MoveNext())
            {
                assignments.Add(
                    new Assignment(
                        GetColumn(en.Key.ToString()),
                        ExpressionHelper.CreateParameterExpression(en.Value)));
            }

            var criteria = new QueryCriteria(_owner.Criteria.TableName,
                                             _owner.Criteria.ConnectionStringName,
                                             false, null);
            criteria.Update(assignments.ToArray());
            criteria.Where(BuildLoadByKeysCondition(keys));

            return criteria;
        }

        private QueryCriteria CreateDeleteCriteria(IDictionary keys)
        {
            var criteria = new QueryCriteria(_owner.Criteria.TableName,
                                             _owner.Criteria.ConnectionStringName,
                                             false, null);
            criteria.Delete().Where(BuildLoadByKeysCondition(keys));

            return criteria;
        }

        private void DetectCompareAllValuesConflicts(IDictionary oldValues, IDictionary keys)
        {
            if ((oldValues == null) || (oldValues.Count == 0))
                throw new ArgumentException("oldValues could not be null or empty when ConflictDetection is CompareAllValues.");
            if ((keys == null) || (keys.Count == 0))
                throw new ArgumentException("keys could not be null or empty when ConflictDetection is CompareAllValues.");

            var criteria = new QueryCriteria(_owner.Criteria.TableName,
                                             _owner.Criteria.ConnectionStringName,
                                             false, _owner.Criteria.PredefinedColumns);
            criteria.Where(BuildLoadByKeysCondition(keys));
            var table = _owner.QueryService.Query(criteria);

            var conflitCheckFailed = false;
            DataRow row;
            if (table != null && table.Rows.Count == 1)
            {
                row = table.Rows[0];

                foreach (DictionaryEntry item in oldValues)
                {
                    if (row.Table.Columns.Contains(item.Key.ToString()))
                    {
                        if (row.Table.Columns[item.Key.ToString()].DataType == typeof(string))
                        {
                            if (
                            !Equals(row[item.Key.ToString()],
                                    TransformType(row.Table.Columns[item.Key.ToString()].DataType,
                                                  item.Value ?? DBNull.Value,
                                                  row.Table.Columns[item.Key.ToString()].AllowDBNull) ?? DBNull.Value))
                                conflitCheckFailed = true;
                        }
                        else if (
                            !Equals(string.Format("{0}", row[item.Key.ToString()]),
                                    string.Format("{0}", TransformType(row.Table.Columns[item.Key.ToString()].DataType,
                                                  item.Value ?? DBNull.Value,
                                                  row.Table.Columns[item.Key.ToString()].AllowDBNull) ?? DBNull.Value)))
                            conflitCheckFailed = true;
                    }
                }
            }
            else
            {
                conflitCheckFailed = true;
            }

            if (conflitCheckFailed)
                throw new DataException("Conflict detection failed, the row is changed by others or is not unique!");
        }

        #endregion
    }
}
