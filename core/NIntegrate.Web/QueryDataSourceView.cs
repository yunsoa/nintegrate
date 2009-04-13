using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using NIntegrate.Query;
using System.Data;
using NIntegrate.Web.EventArgs;

namespace NIntegrate.Web
{
    internal sealed class QueryDataSourceView : DataSourceView
    {
        #region Private Fields

        private readonly QueryDataSource _owner;
        private readonly static Condition _neverEquivalentCondition 
            = new Condition(NullExpression.Value, ExpressionOperator.Equals, NullExpression.Value);

        #endregion

        #region Private Methods

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
            var childExprs = new List<IExpression> {QueryHelper.CreateParameterExpression(value)};
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

        private static void InsertSortExpressinAtTopOfSortBys(string sortExpression, Criteria criteria)
        {
            if (criteria._sortBys.Count == 0)
            {
                AppendSortExpression(criteria, sortExpression);
                return;
            }

            var sortBys = new Dictionary<IColumn, bool>();
            var en = criteria._sortBys.GetEnumerator();
            while (en.MoveNext())
                sortBys.Add((IColumn)en.Current.Key.Clone(), en.Current.Value);
            criteria._sortBys.Clear();
            AppendSortExpression(criteria, sortExpression);
            en = sortBys.GetEnumerator();
            while (en.MoveNext())
                criteria._sortBys.Add(en.Current.Key, en.Current.Value);
        }

        private static void AppendSortExpression(Criteria criteria, string sortExpression)
        {
            var sortBy = sortExpression;
            var isDesc = false;
            if (sortExpression.ToUpperInvariant().EndsWith(" DESC"))
            {
                isDesc = true;
                sortBy = sortExpression.Substring(0, sortExpression.Length - " DESC".Length);
            }
            else if (sortExpression.ToUpperInvariant().EndsWith(" ASC"))
            {
                sortBy = sortExpression.Substring(0, sortExpression.Length - " ASC".Length);
            }
            criteria._sortBys.Add(new Int32Column(sortBy), isDesc);
        }

        private static object TransformType(Type targetType, object value)
        {
            if (targetType == null)
                throw new ArgumentNullException("targetType");

            var stringValue = value as string;
            if (stringValue == null)
                return QueryHelper.DefaultValue(targetType);

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

        private Criteria PrepareCriteriaForUpdate()
        {
            var criteria = _owner.Criteria.ToBaseCriteria();
            criteria._skipResults = 0;
            criteria._maxResults = 0;
            criteria._isDistinct = false;
            criteria._sortBys.Clear();
            criteria._conditionAndOrs.Clear();
            criteria._conditions.Clear();
            return criteria;
        }

        private static void DetectDataRowConflicts(IDictionary oldValues, DataRow row)
        {
            if ((oldValues == null) || (oldValues.Count == 0))
                new ArgumentException("oldValues could not be null or empty when ConflictDetection is CompareAllValues.");

            var conflitCheckFailed = false;
            foreach (DictionaryEntry item in oldValues)
            {
                if (row.Table.Columns.Contains(item.Key.ToString()))
                {
                    if (
                        !Equals(row[item.Key.ToString()],
                                TransformType(row.Table.Columns[item.Key.ToString()].DataType, item.Value)))
                        conflitCheckFailed = true;
                }
            }
            if (conflitCheckFailed)
                throw new DataException("Conflict detection failed, the row is changed by others!");
        }

        #endregion

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
                return true;
            }
        }

        public override bool CanUpdate
        {
            get
            {
                return true;
            }
        }

        public override bool CanDelete
        {
            get
            {
                return true;
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

        #region Protected Methods

        protected override int ExecuteInsert(IDictionary values)
        {
            if (values == null || values.Count == 0)
                throw new ArgumentNullException("values");

            var insertingArgs = new DataSourceInsertingEventArgs(values);
            _owner.OnInserting(insertingArgs);
            if (insertingArgs.Cancel)
                return 0;

            var criteria = PrepareCriteriaForUpdate();
            criteria.And(_neverEquivalentCondition);

            var table = _owner._service.Select(criteria);
            var row = table.NewRow();
            var en = values.GetEnumerator();
            while (en.MoveNext())
            {
                if (table.Columns.Contains(en.Key.ToString()))
                    row[en.Key.ToString()] = TransformType(table.Columns[en.Key.ToString()].DataType, en.Value);
            }
            table.Rows.Add(row);

            var conflictDetection = ConflictOption.OverwriteChanges;
            if (_owner.ConflictDetection == ConflictOptions.CompareAllValues)
            {
                conflictDetection = ConflictOption.CompareAllSearchableValues;
            }

            var affectedRows = _owner._service.Update(_owner.Criteria, table, conflictDetection);

            var statusArgs = new DataSourceStatusEventArgs(row, affectedRows);
            _owner.OnInserted(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
        {
            if (keys == null || keys.Count == 0)
                throw new ArgumentNullException("keys");
            if (values == null || values.Count == 0)
                throw new ArgumentNullException("values");

            var updatingArgs = new DataSourceUpdatingEventArgs(GetReadOnlyDictionary(keys), values, oldValues);
            _owner.OnUpdating(updatingArgs);
            if (updatingArgs.Cancel)
                return 0;

            var criteria = PrepareCriteriaForUpdate();
            criteria.And(BuildLoadByKeysCondition(keys));

            var table = _owner._service.Select(criteria);
            if (table == null || table.Rows.Count == 0)
                throw new DataException("No row is matching specified key values.");
            if (table.Rows.Count > 1)
                throw new DataException("More than one rows are matching specified key values, please check the key columns setting.");
            var row = table.Rows[0];
            var conflictDetection = ConflictOption.OverwriteChanges;
            if (_owner.ConflictDetection == ConflictOptions.CompareAllValues)
            {
                DetectDataRowConflicts(oldValues, row);
                conflictDetection = ConflictOption.CompareAllSearchableValues;
            }

            var en = values.GetEnumerator();
            while (en.MoveNext())
            {
                if (table.Columns.Contains(en.Key.ToString()))
                    row[en.Key.ToString()] = TransformType(table.Columns[en.Key.ToString()].DataType, en.Value);
            }

            var affectedRows = _owner._service.Update(_owner.Criteria, table, conflictDetection);

            var statusArgs = new DataSourceStatusEventArgs(row, affectedRows);
            _owner.OnUpdated(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
        {
            if (keys == null || keys.Count == 0)
                throw new ArgumentNullException("keys");

            var deletingArgs = new DataSourceDeletingEventArgs(GetReadOnlyDictionary(keys), oldValues);
            _owner.OnDeleting(deletingArgs);
            if (deletingArgs.Cancel)
                return 0;

            var criteria = PrepareCriteriaForUpdate();
            criteria.And(BuildLoadByKeysCondition(keys));

            var table = _owner._service.Select(criteria);
            if (table == null || table.Rows.Count == 0)
                throw new DataException("No row is matching specified key values.");
            if (table.Rows.Count > 1)
                throw new DataException("More than one rows are matching specified key values, please check the key columns setting.");

            var row = table.Rows[0];
            var conflictDetection = ConflictOption.OverwriteChanges;
            if (_owner.ConflictDetection == ConflictOptions.CompareAllValues)
            {
                DetectDataRowConflicts(oldValues, row);
                conflictDetection = ConflictOption.CompareAllSearchableValues;
            }

            row.Delete();

            var affectedRows = _owner._service.Update(_owner.Criteria, table, conflictDetection);
            var statusArgs = new DataSourceStatusEventArgs(table, affectedRows);
            _owner.OnDeleted(statusArgs);

            if (affectedRows > 0)
                OnDataSourceViewChanged(System.EventArgs.Empty);
            return affectedRows;
        }

        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
        {
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
                return null;
            }

            var criteria = _owner.Criteria.ToBaseCriteria();

            if (arguments != null && arguments != DataSourceSelectArguments.Empty)
            {
                //adjust criteria according to arguments
                if (arguments.RetrieveTotalRowCount)
                {
                    arguments.TotalRowCount = _owner._service.SelectCount(criteria);
                    _owner.LastTotalCount = arguments.TotalRowCount;
                }
                if (arguments.MaximumRows > 0)
                    criteria.MaxResults(arguments.MaximumRows);
                if (arguments.StartRowIndex > 0)
                    criteria.SkipResults(arguments.StartRowIndex);
                if (!string.IsNullOrEmpty(arguments.SortExpression))
                {
                    if (_owner.AlwaysAppendDefaultSortBysWhenSorting)
                        InsertSortExpressinAtTopOfSortBys(arguments.SortExpression, criteria);
                    else
                    {
                        criteria._sortBys.Clear();
                        AppendSortExpression(criteria, arguments.SortExpression);
                    }
                }
            }

            return new DataView(_owner._service.Select(criteria));
        }

        #endregion
    }
}
