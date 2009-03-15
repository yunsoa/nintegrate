using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
namespace NBear.Query
{
    /// <summary>
    /// Base class of all Custom Criterias
    /// </summary>
    [DataContract]
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
    [KnownType(typeof(Condition))]
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
    public class Criteria
    {
        #region Protected Fields

        [DataMember]
        internal protected string _tableName;
        [DataMember]
        internal protected string _connectionStringName;

        [DataMember]
        internal protected readonly List<IColumn> _resultColumns = new List<IColumn>();
        [DataMember]
        internal protected bool _isDistinct;
        [DataMember]
        internal protected int _maxResults;
        [DataMember]
        internal protected int _skipResults;
        [DataMember]
        internal protected readonly Dictionary<IColumn, bool> _sortBys = new Dictionary<IColumn, bool>();
        [DataMember]
        internal protected readonly List<ConditionAndOr> _conditionAndOrs = new List<ConditionAndOr>();
        [DataMember]
        internal protected readonly List<Condition> _conditions = new List<Condition>();

        #endregion

        #region Constructors

        protected Criteria(string tableName, string connectionStringName)
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

        public Criteria Distinct()
        {
            _isDistinct = true;

            return this;
        }

        public Criteria MaxResults(int n)
        {
            _maxResults = n;

            return this;
        }

        public Criteria SkipResults(int n)
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

        public DateTimeExpression GetCurrentDate()
        {
            return new DateTimeExpression("getdate()", null);
        }

        public DateTimeExpression GetCurrentUtcDate()
        {
            return new DateTimeExpression("getutcdate()", null);
        }

        #endregion
    }
}
