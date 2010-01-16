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
        Delete,

        [EnumMember]
        Sproc
    }

    /// <summary>
    /// A QueryCriteria instance represents a C/R/U/D query on a query table.
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
        private readonly List<IColumn> _predefinedColumns;

        [DataMember]
        private readonly List<IColumn> _resultColumns;

        [DataMember]
        private bool _isDistinct;

        [DataMember]
        private int _maxResults;

        [DataMember]
        private int _skipResults;

        [DataMember]
        private readonly Dictionary<IColumn, bool> _sortBys;

        [DataMember]
        private readonly List<ConditionAndOr> _conditionAndOrs;

        [DataMember]
        private readonly List<Condition> _conditions;

        [DataMember]
        private readonly List<Assignment> _assignments;

        [DataMember]
        private bool _readOnly;

        [DataMember]
        private readonly List<ParameterEqualsCondition> _sprocParameterConditions;

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
            _predefinedColumns = new List<IColumn>();
            _resultColumns = new List<IColumn>();
            _sortBys = new Dictionary<IColumn, bool>();
            _conditionAndOrs = new List<ConditionAndOr>();
            _conditions = new List<Condition>();
            _assignments = new List<Assignment>();
            _sprocParameterConditions = new List<ParameterEqualsCondition>();
            _queryType = QueryType.Select;
        }

        internal QueryCriteria(string tableName, string connectionStringName, bool readOnly, IEnumerable<IColumn> predefinedColumns)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
            _readOnly = readOnly;
            _predefinedColumns = new List<IColumn>();
            if (predefinedColumns != null)
                _predefinedColumns.AddRange(predefinedColumns);
            _resultColumns = new List<IColumn>();
            _sortBys = new Dictionary<IColumn, bool>();
            _conditionAndOrs = new List<ConditionAndOr>();
            _conditions = new List<Condition>();
            _assignments = new List<Assignment>();
            _sprocParameterConditions = new List<ParameterEqualsCondition>();
            _queryType = QueryType.Select;
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

        /// <summary>
        /// Gets the predefined columns to return if no columns is specified in Select() method.
        /// </summary>
        /// <value>The predefined columns.</value>
        public ReadOnlyCollection<IColumn> PredefinedColumns
        {
            get { return new ReadOnlyCollection<IColumn>(_predefinedColumns); }
        }

        /// <summary>
        /// The result columns specified to return in select.
        /// </summary>
        /// <value>The result columns.</value>
        public ReadOnlyCollection<IColumn> ResultColumns
        {
            get { return new ReadOnlyCollection<IColumn>(_resultColumns); }
        }

        /// <summary>
        /// Gets a value indicating whether the select returns distinct results.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if return distinct results; otherwise, <c>false</c>.
        /// </value>
        public bool IsDistinct
        {
            get { return _isDistinct; }
        }

        /// <summary>
        /// Get the max number of results to return.
        /// </summary>
        /// <value>The max results.</value>
        public int MaxResults
        {
            get { return _maxResults; }
        }

        /// <summary>
        /// Get the skip number of results.
        /// </summary>
        /// <value>The skip results.</value>
        public int SkipResults
        {
            get { return _skipResults; }
        }

        /// <summary>
        /// Get the sort bys.
        /// </summary>
        /// <value>The sort bys.</value>
        public IDictionary<IColumn, bool> SortBys
        {
            get { return _sortBys; }
        }

        /// <summary>
        /// Get the And/Or logic operators of the query conditions.
        /// </summary>
        /// <value>The condition and ors.</value>
        public ReadOnlyCollection<ConditionAndOr> ConditionAndOrs
        {
            get { return new ReadOnlyCollection<ConditionAndOr>(_conditionAndOrs); }
        }

        /// <summary>
        /// Get the query conditions.
        /// </summary>
        /// <value>The conditions.</value>
        public ReadOnlyCollection<Condition> Conditions
        {
            get { return new ReadOnlyCollection<Condition>(_conditions); }
        }

        /// <summary>
        /// Get the assignments for Insert & Update operations.
        /// </summary>
        /// <value>The assignments.</value>
        public ReadOnlyCollection<Assignment> Assignments
        {
            get { return new ReadOnlyCollection<Assignment>(_assignments); }
        }

        internal ReadOnlyCollection<ParameterEqualsCondition> SprocParameterConditions
        {
            get { return new ReadOnlyCollection<ParameterEqualsCondition>(_sprocParameterConditions); }
        }

        /// <summary>
        /// Gets a value indicating whether the query is readonly.
        /// </summary>
        /// <value><c>true</c> if the query is readonly; otherwise, <c>false</c>.</value>
        public bool ReadOnly
        {
            get { return _readOnly; }
        }

        /// <summary>
        /// Get the QueryType.
        /// </summary>
        /// <value>The QueryType.</value>
        public QueryType QueryType
        {
            get { return _queryType; }
        }

        #endregion

        #region Public Methods

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

        public QueryCriteria SortBy(IColumn column, bool isDescendent)
        {
            if (ReferenceEquals(column, null))
                throw new ArgumentNullException("column");

            if (!_sortBys.ContainsKey(column))
                _sortBys.Add(column, isDescendent);

            OnChanged();

            return this;
        }

        public QueryCriteria ThenSortBy(IColumn column, bool isDescendent)
        {
            return SortBy(column, isDescendent);
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

        public QueryCriteria Clone()
        {
            var clone = new QueryCriteria();

            clone._assignments.AddRange(_assignments);
            clone._sprocParameterConditions.AddRange(_sprocParameterConditions);
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

        internal QueryCriteria Select(params IColumn[] columns)
        {
            _queryType = QueryType.Select;

            foreach (var column in columns)
            {
                if (!_resultColumns.Contains(column))
                    _resultColumns.Add(column);
            }

            OnChanged();

            return this;
        }

        internal QueryCriteria Insert(params Assignment[] assignments)
        {
            if (_readOnly)
                throw new InvalidOperationException("Readonly Criteria could not be used for insert.");

            if (assignments == null || assignments.Length == 0)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Insert;

            _assignments.AddRange(assignments);

            OnChanged();

            return this;
        }

        internal QueryCriteria Update(params Assignment[] assignments)
        {
            if (_readOnly)
                throw new InvalidOperationException("Readonly Criteria could not be used for update.");

            if (assignments == null || assignments.Length == 0)
                throw new ArgumentNullException("assignments");

            _queryType = QueryType.Update;

            _assignments.AddRange(assignments);

            OnChanged();

            return this;
        }

        internal QueryCriteria Delete()
        {
            if (_readOnly)
                throw new InvalidOperationException("Readonly Criteria could not be used for delete.");

            _queryType = QueryType.Delete;

            OnChanged();

            return this;
        }

        internal QueryCriteria Sproc(params ParameterEqualsCondition[] parameterConditions)
        {
            _queryType = QueryType.Sproc;

            if (parameterConditions != null)
            {
                _sprocParameterConditions.AddRange(parameterConditions);
            }

            return this;
        }

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
