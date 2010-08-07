using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum QueryType
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Select,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Insert,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Update,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Delete,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Sproc
    }

    /// <summary>
    /// A QueryCriteria instance represents a C/R/U/D query on a query table.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
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
        private List<Assignment> _assignments;

        [DataMember]
        private bool _readOnly;

        [DataMember]
        private readonly List<ParameterEqualsCondition> _sprocParameterConditions;

        #region Events

        /// <summary>
        /// Occurs when changed.
        /// </summary>
        public event EventHandler Changed;

        #endregion

        #region KnownTypes

        /// <summary>
        /// Knowns the types.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCriteria"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <param name="predefinedColumns">The predefined columns.</param>
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

        /// <summary>
        /// Gets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        internal string TableName
        {
            get { return _tableName; }
        }

        /// <summary>
        /// Gets the name of the connection string.
        /// </summary>
        /// <value>The name of the connection string.</value>
        internal string ConnectionStringName
        {
            get { return _connectionStringName; }
        }

        /// <summary>
        /// Gets the predefined columns to return if no columns is specified in Select() method.
        /// </summary>
        /// <value>The predefined columns.</value>
        internal ReadOnlyCollection<IColumn> PredefinedColumns
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
        internal IDictionary<IColumn, bool> SortBys
        {
            get { return _sortBys; }
        }

        /// <summary>
        /// Get the And/Or logic operators of the query conditions.
        /// </summary>
        /// <value>The condition and ors.</value>
        internal ReadOnlyCollection<ConditionAndOr> ConditionAndOrs
        {
            get { return new ReadOnlyCollection<ConditionAndOr>(_conditionAndOrs); }
        }

        /// <summary>
        /// Get the query conditions.
        /// </summary>
        /// <value>The conditions.</value>
        internal ReadOnlyCollection<Condition> Conditions
        {
            get { return new ReadOnlyCollection<Condition>(_conditions); }
        }

        /// <summary>
        /// Get the assignments for Insert & Update operations.
        /// </summary>
        /// <value>The assignments.</value>
        internal ReadOnlyCollection<Assignment> Assignments
        {
            get { return new ReadOnlyCollection<Assignment>(_assignments); }
        }

        internal void SetAssignments(Assignment[] assignments)
        {
            if (assignments == null || assignments.Length == 0)
                throw new ArgumentNullException("assignments");

            _assignments = new List<Assignment>(assignments);
        }

        /// <summary>
        /// Gets the sproc parameter conditions.
        /// </summary>
        /// <value>The sproc parameter conditions.</value>
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
            internal set { _queryType = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the is distinct.
        /// </summary>
        /// <param name="isDistinct">if set to <c>true</c> [is distinct].</param>
        /// <returns></returns>
        public QueryCriteria SetIsDistinct(bool isDistinct)
        {
            _isDistinct = isDistinct;

            OnChanged();

            return this;
        }

        /// <summary>
        /// Sets the max results.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public QueryCriteria SetMaxResults(int n)
        {
            _maxResults = n;

            OnChanged();

            return this;
        }

        /// <summary>
        /// Sets the skip results.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <returns></returns>
        public QueryCriteria SetSkipResults(int n)
        {
            _skipResults = n;

            OnChanged();

            return this;
        }

        /// <summary>
        /// Sorts the by.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="isDescendent">if set to <c>true</c> [is descendent].</param>
        /// <returns></returns>
        public QueryCriteria SortBy(IColumn column, bool isDescendent)
        {
            if (ReferenceEquals(column, null))
                throw new ArgumentNullException("column");

            if (!_sortBys.ContainsKey(column))
                _sortBys.Add(column, isDescendent);

            OnChanged();

            return this;
        }

        /// <summary>
        /// Thens the sort by.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="isDescendent">if set to <c>true</c> [is descendent].</param>
        /// <returns></returns>
        public QueryCriteria ThenSortBy(IColumn column, bool isDescendent)
        {
            return SortBy(column, isDescendent);
        }

        /// <summary>
        /// Ands the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public QueryCriteria And(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.And);
            _conditions.Add(condition);

            OnChanged();

            return this;
        }

        /// <summary>
        /// Ors the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public QueryCriteria Or(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.Or);
            _conditions.Add(condition);

            OnChanged();

            return this;
        }

        /// <summary>
        /// Wheres the specified condition.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public QueryCriteria Where(Condition condition)
        {
            return And(condition);
        }

        /// <summary>
        /// Clears the result columns.
        /// </summary>
        public void ClearResultColumns()
        {
            _resultColumns.Clear();

            OnChanged();
        }

        /// <summary>
        /// Clears the conditions.
        /// </summary>
        public void ClearConditions()
        {
            _conditionAndOrs.Clear();
            _conditions.Clear();

            OnChanged();
        }

        /// <summary>
        /// Clears the sort bys.
        /// </summary>
        public void ClearSortBys()
        {
            _sortBys.Clear();

            OnChanged();
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Selects the specified columns.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns></returns>
        internal QueryCriteria Select(params IColumn[] columns)
        {
            _queryType = QueryType.Select;

            if (columns != null && columns.Length > 0)
            {
                bool changed = false;
                foreach (var column in columns)
                {
                    if (!_resultColumns.Contains(column))
                    {
                        _resultColumns.Add(column);
                        changed = true;
                    }
                }

                if (changed)
                    OnChanged();
            }

            return this;
        }

        /// <summary>
        /// Inserts the specified assignments.
        /// </summary>
        /// <param name="assignments">The assignments.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the specified assignments.
        /// </summary>
        /// <param name="assignments">The assignments.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        /// <returns></returns>
        internal QueryCriteria Delete()
        {
            if (_readOnly)
                throw new InvalidOperationException("Readonly Criteria could not be used for delete.");

            _queryType = QueryType.Delete;

            OnChanged();

            return this;
        }

        /// <summary>
        /// Sprocs the specified parameter conditions.
        /// </summary>
        /// <param name="parameterConditions">The parameter conditions.</param>
        /// <returns></returns>
        internal QueryCriteria Sproc(params ParameterEqualsCondition[] parameterConditions)
        {
            _queryType = QueryType.Sproc;

            if (parameterConditions != null)
            {
                _sprocParameterConditions.AddRange(parameterConditions);
            }

            return this;
        }

        /// <summary>
        /// Called when [changed].
        /// </summary>
        internal void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the identified parameter value.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        internal void UpdateIdentifiedParameterValue(string id, object value)
        {
            foreach (var condition in _conditions)
            {
                condition.UpdateIdentifiedParameterValue(id, value);
            }
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
    }
}
