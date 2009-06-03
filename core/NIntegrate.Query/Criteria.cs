using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.Serialization;
using NIntegrate.Query.SqlClient;

namespace NIntegrate.Query
{
    /// <summary>
    /// Base class of all Custom Criterias
    /// </summary>
    [DataContract]
    [KnownType(typeof(Condition))]
    [KnownType(typeof(NullExpression))]
    [KnownType(typeof(BooleanExpression))]
    [KnownType(typeof(ByteExpression))]
    [KnownType(typeof(Int16Expression))]
    [KnownType(typeof(Int32Expression))]
    [KnownType(typeof(Int64Expression))]
    [KnownType(typeof(DateTimeExpression))]
    [KnownType(typeof(StringExpression))]
    [KnownType(typeof(GuidExpression))]
    [KnownType(typeof(DoubleExpression))]
    [KnownType(typeof(DecimalExpression))]
    [KnownType(typeof(ExpressionCollection))]
    [KnownType(typeof(BooleanParameterExpression))]
    [KnownType(typeof(ByteParameterExpression))]
    [KnownType(typeof(Int16ParameterExpression))]
    [KnownType(typeof(Int32ParameterExpression))]
    [KnownType(typeof(Int64ParameterExpression))]
    [KnownType(typeof(DateTimeParameterExpression))]
    [KnownType(typeof(StringParameterExpression))]
    [KnownType(typeof(GuidParameterExpression))]
    [KnownType(typeof(DoubleParameterExpression))]
    [KnownType(typeof(DecimalParameterExpression))]
    [KnownType(typeof(BooleanColumn))]
    [KnownType(typeof(ByteColumn))]
    [KnownType(typeof(Int16Column))]
    [KnownType(typeof(Int32Column))]
    [KnownType(typeof(Int64Column))]
    [KnownType(typeof(DateTimeColumn))]
    [KnownType(typeof(StringColumn))]
    [KnownType(typeof(GuidColumn))]
    [KnownType(typeof(DoubleColumn))]
    [KnownType(typeof(DecimalColumn))]
    [KnownType(typeof(SqlBooleanColumn))]
    [KnownType(typeof(SqlByteColumn))]
    [KnownType(typeof(SqlInt16Column))]
    [KnownType(typeof(SqlInt32Column))]
    [KnownType(typeof(SqlInt64Column))]
    [KnownType(typeof(SqlDateTimeColumn))]
    [KnownType(typeof(SqlStringColumn))]
    [KnownType(typeof(SqlGuidColumn))]
    [KnownType(typeof(SqlDoubleColumn))]
    [KnownType(typeof(SqlDecimalColumn))]
    public class Criteria
    {
        #region Protected Fields

        [DataMember]
        protected string _tableName;
        [DataMember]
        protected string _connectionStringName;
        [DataMember]
        protected readonly List<IColumn> _predefinedColumns = new List<IColumn>();
        [DataMember]
        protected readonly List<IColumn> _resultColumns = new List<IColumn>();
        [DataMember]
        protected bool _isDistinct;
        [DataMember]
        protected int _maxResults;
        [DataMember]
        protected int _skipResults;
        [DataMember]
        protected readonly Dictionary<IColumn, bool> _sortBys = new Dictionary<IColumn, bool>();
        [DataMember]
        protected readonly List<ConditionAndOr> _conditionAndOrs = new List<ConditionAndOr>();
        [DataMember]
        protected readonly List<Condition> _conditions = new List<Condition>();

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

        public IList<IColumn> ResultColumns
        {
            get { return _resultColumns; }
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

        #endregion

        #region Private Methods

        private void ParsePredefinedColumns()
        {
            foreach (FieldInfo field in GetType().GetFields())
            {
                if (!typeof(IColumn).IsAssignableFrom(field.FieldType)) continue;
                var column = (IColumn)field.GetValue(this);
                _predefinedColumns.Add(column);
            }
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                if (!property.CanRead || !typeof(IColumn).IsAssignableFrom(property.PropertyType)) continue;
                var column = (IColumn)property.GetValue(this, null);
                _predefinedColumns.Add(column);
            }
        }

        #endregion

        #region Constructors

        internal Criteria()
        {
            ParsePredefinedColumns();
        }

        protected Criteria(string tableName, string connectionStringName)
            : this()
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
        }

        #endregion

        #region Public Methods

        public Criteria AddResultColumn(IColumn column)
        {
            if (!_resultColumns.Contains(column))
                _resultColumns.Add(column);

            return this;
        }

        public Criteria SetIsDistinct(bool isDistinct)
        {
            _isDistinct = isDistinct;

            return this;
        }

        public Criteria SetMaxResults(int n)
        {
            _maxResults = n;

            return this;
        }

        public Criteria SetSkipResults(int n)
        {
            _skipResults = n;

            return this;
        }

        public Criteria AddSortBy(IColumn column, bool isDescendent)
        {
            if (ReferenceEquals(column, null))
                throw new ArgumentNullException("column");

            if (!_sortBys.ContainsKey(column))
                _sortBys.Add(column, isDescendent);

            return this;
        }

        public Criteria And(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.And);
            _conditions.Add(condition);

            return this;
        }

        public Criteria Or(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            _conditionAndOrs.Add(ConditionAndOr.Or);
            _conditions.Add(condition);

            return this;
        }

        internal void UpdateIdentifiedParameterValue(string id, object value)
        {
            foreach (var condition in _conditions)
            {
                condition.UpdateIdentifiedParameterValue(id, value);
            }
        }

        internal protected Criteria CloneTo(Criteria clone)
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

        public Criteria ToSerializableCriteria()
        {
            var clone = new Criteria();
            CloneTo(clone);
            return clone;
        }

        public void ClearResultColumns()
        {
            _resultColumns.Clear();
        }

        public void ClearConditions()
        {
            _conditionAndOrs.Clear();
            _conditions.Clear();
        }

        #endregion
    }
}
