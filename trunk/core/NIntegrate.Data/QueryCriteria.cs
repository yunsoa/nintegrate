using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    [DataContract]
    public enum QueryType
    {
        [EnumMember]
        Select,

        [EnumMember]
        Insert,

        [EnumMember]
        Update,

        [EnumMember]
        Delete
    }

    /// <summary>
    /// Base class of all Custom Query Criterias
    /// </summary>
    [DataContract]
    [KnownType("KnownTypes")]
    public sealed class QueryCriteria
    {
        [DataMember]
        private QueryType _queryType;

        [DataMember]
        private string _tableName;

        [DataMember]
        private string _connectionStringName;

        [DataMember]
        private readonly List<IColumn> _predefinedColumns = new List<IColumn>();

        [DataMember]
        private readonly List<IColumn> _resultColumns = new List<IColumn>();

        [DataMember]
        private bool _isDistinct;

        [DataMember]
        private int _maxResults;

        [DataMember]
        private int _skipResults;

        [DataMember]
        private readonly Dictionary<IColumn, bool> _sortBys = new Dictionary<IColumn, bool>();

        [DataMember]
        private readonly List<ConditionAndOr> _conditionAndOrs = new List<ConditionAndOr>();

        [DataMember]
        private readonly List<Condition> _conditions = new List<Condition>();

        [DataMember]
        private readonly List<Assignment> _assignments = new List<Assignment>();

        [DataMember]
        private bool _isReadonly;

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        private QueryCriteria()
        {
            _queryType = QueryType.Select;
        }

        internal QueryCriteria(string tableName, string connectionStringName, bool isReadonly, IEnumerable<IColumn> predefinedColumns)
            : this()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
            _isReadonly = isReadonly;
            if (predefinedColumns != null)
                _predefinedColumns.AddRange(predefinedColumns);
        }

        #endregion

        #region Properties

        public string TableName
        {
            get { return _tableName; }
        }

        public string ConnectionStringName
        {
            get { return _connectionStringName; }
        }

        public ReadOnlyCollection<IColumn> PredefinedColumns
        {
            get { return new ReadOnlyCollection<IColumn>(_predefinedColumns); }
        }

        public ReadOnlyCollection<IColumn> ResultColumns
        {
            get { return new ReadOnlyCollection<IColumn>(_resultColumns); }
        }

        public bool IsDistinct
        {
            get { return _isDistinct; }
        }

        public int MaxResults
        {
            get { return _maxResults; }
        }

        public int SkipResults
        {
            get { return _skipResults; }
        }

        public IDictionary<IColumn, bool> SortBys
        {
            get { return _sortBys; }
        }

        public ReadOnlyCollection<ConditionAndOr> ConditionAndOrs
        {
            get { return new ReadOnlyCollection<ConditionAndOr>(_conditionAndOrs); }
        }

        public ReadOnlyCollection<Condition> Conditions
        {
            get { return new ReadOnlyCollection<Condition>(_conditions); }
        }

        public ReadOnlyCollection<Assignment> Assignments
        {
            get { return new ReadOnlyCollection<Assignment>(_assignments); }
        }

        public bool IsReadOnly
        {
            get { return _isReadonly; }
        }

        public QueryType QueryType
        {
            get { return _queryType; }
        }

        #endregion

        #region Public Methods

        public QueryCriteria Select(params IColumn[] columns)
        {
            if (columns == null || columns.Length == 0)
                throw new ArgumentNullException("columns");

            _queryType = QueryType.Select;

            foreach (var column in columns)
            {
                if (!_resultColumns.Contains(column))
                    _resultColumns.Add(column);
            }

            return this;
        }

        public QueryCriteria SetIsDistinct(bool isDistinct)
        {
            _isDistinct = isDistinct;

            return this;
        }

        public QueryCriteria SetMaxResults(int n)
        {
            _maxResults = n;

            return this;
        }

        public QueryCriteria SetSkipResults(int n)
        {
            _skipResults = n;

            return this;
        }

        public QueryCriteria AddSortBy(IColumn column, bool isDescendent)
        {
            if (ReferenceEquals(column, null))
                throw new ArgumentNullException("column");

            if (!_sortBys.ContainsKey(column))
                _sortBys.Add(column, isDescendent);

            return this;
        }

        public QueryCriteria And(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.And);
            _conditions.Add(condition);

            return this;
        }

        public QueryCriteria Or(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.Or);
            _conditions.Add(condition);

            return this;
        }

        public QueryCriteria Where(Condition condition)
        {
            return And(condition);
        }

        //public QueryCriteria ToSerializableCriteria()
        //{
        //    var clone = new QueryCriteria();
        //    CloneTo(clone);
        //    return clone;
        //}

        public void ClearResultColumns()
        {
            _resultColumns.Clear();
        }

        public void ClearConditions()
        {
            _conditionAndOrs.Clear();
            _conditions.Clear();
        }

        public void ClearSortBys()
        {
            _sortBys.Clear();
        }

        public QueryCriteria Insert(Assignment assignments)
        {
            if (assignments == null)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Insert;

            _assignments.Add(assignments);

            return this;
        }

        public QueryCriteria Update(Assignment assignments)
        {
            if (assignments == null)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Update;

            _assignments.Add(assignments);

            return this;
        }

        public QueryCriteria Delete()
        {
            _queryType = QueryType.Delete;

            return this;
        }

        #endregion

        #region Non-Public Methods

        internal void UpdateIdentifiedParameterValue(string id, object value)
        {
            foreach (var condition in _conditions)
            {
                condition.UpdateIdentifiedParameterValue(id, value);
            }
        }

        internal QueryCriteria CloneTo(QueryCriteria clone)
        {
            if (clone == null)
                throw new ArgumentNullException("clone");

            clone._tableName = _tableName;
            clone._connectionStringName = _connectionStringName;
            foreach (var column in _resultColumns)
                clone._resultColumns.Add((IColumn)column.Clone());
            clone._isDistinct = _isDistinct;
            clone._maxResults = _maxResults;
            clone._skipResults = _skipResults;
            clone._queryType = _queryType;
            clone._isReadonly = _isReadonly;
            var en = _sortBys.GetEnumerator();
            while (en.MoveNext())
                clone._sortBys.Add((IColumn)en.Current.Key.Clone(), en.Current.Value);
            foreach (var andOr in _conditionAndOrs)
                clone._conditionAndOrs.Add(andOr);
            foreach (var condition in _conditions)
                clone._conditions.Add((Condition)condition.Clone());
            foreach (var column in _predefinedColumns)
                clone._predefinedColumns.Add((IColumn)column.Clone());

            return clone;
        }

        #endregion
    }
}
