using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;
using System.Linq;

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
    public sealed class QueryCriteria : ICloneable
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
        private bool _readOnly;

        #region Events

        public event EventHandler Changed;

        #endregion

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

        internal QueryCriteria(string tableName, string connectionStringName, bool readOnly, IEnumerable<IColumn> predefinedColumns)
            : this()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
            _readOnly = readOnly;
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

        public bool ReadOnly
        {
            get { return _readOnly; }
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

            OnChanged();

            return this;
        }

        public QueryCriteria SetIsDistinct(bool isDistinct)
        {
            _isDistinct = isDistinct;

            OnChanged();

            return this;
        }

        public QueryCriteria SetMaxResults(int n)
        {
            _maxResults = n;

            OnChanged();

            return this;
        }

        public QueryCriteria SetSkipResults(int n)
        {
            _skipResults = n;

            OnChanged();

            return this;
        }

        public QueryCriteria AddSortBy(IColumn column, bool isDescendent)
        {
            if (ReferenceEquals(column, null))
                throw new ArgumentNullException("column");

            if (!_sortBys.ContainsKey(column))
                _sortBys.Add(column, isDescendent);

            OnChanged();

            return this;
        }

        public QueryCriteria And(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.And);
            _conditions.Add(condition);

            OnChanged();

            return this;
        }

        public QueryCriteria Or(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.Or);
            _conditions.Add(condition);

            OnChanged();

            return this;
        }

        public QueryCriteria Where(Condition condition)
        {
            OnChanged();

            return And(condition);
        }

        public void ClearResultColumns()
        {
            _resultColumns.Clear();

            OnChanged();
        }

        public void ClearConditions()
        {
            _conditionAndOrs.Clear();
            _conditions.Clear();

            OnChanged();
        }

        public void ClearSortBys()
        {
            _sortBys.Clear();

            OnChanged();
        }

        public QueryCriteria Insert(params Assignment[] assignments)
        {
            if (assignments == null || assignments.Length == 0)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Insert;

            _assignments.AddRange(assignments);

            OnChanged();

            return this;
        }

        public QueryCriteria Update(params Assignment[] assignments)
        {
            if (assignments == null || assignments.Length == 0)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Update;

            _assignments.AddRange(assignments);

            OnChanged();

            return this;
        }

        public QueryCriteria Delete()
        {
            _queryType = QueryType.Delete;

            OnChanged();

            return this;
        }

        public QueryCriteria Clone()
        {
            var clone = new QueryCriteria();

            clone._assignments.AddRange(_assignments);
            clone._conditionAndOrs.AddRange(_conditionAndOrs);
            clone._conditions.AddRange(_conditions);
            clone._connectionStringName = _connectionStringName;
            clone._isDistinct = _isDistinct;
            clone._maxResults = _maxResults;
            clone._predefinedColumns.AddRange(_predefinedColumns);
            clone._queryType = _queryType;
            clone._readOnly = _readOnly;
            clone._resultColumns.AddRange(_resultColumns);
            clone._skipResults = _skipResults;
            foreach (var item in _sortBys)
            {
                clone._sortBys.Add(item.Key, item.Value);
            }
            clone._tableName = _tableName;

            return clone;
        }

        #endregion

        #region Non-Public Methods

        internal void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        internal void UpdateIdentifiedParameterValue(string id, object value)
        {
            foreach (var condition in _conditions)
            {
                condition.UpdateIdentifiedParameterValue(id, value);
            }
        }

        #endregion

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}
