using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using NIntegrate.Query.SqlClient;

namespace NIntegrate.Query
{
    [DataContract]
    public enum ConditionAndOr
    {
        [EnumMember]
        And,
        [EnumMember]
        Or
    }

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
    public sealed class Condition : BooleanExpression
    {
        #region Private Fields

        [DataMember]
        private IExpression _left;
        [DataMember]
        private ExpressionOperator _operator;
        [DataMember]
        private IExpression _right;

        [DataMember]
        private bool _notFlag;
        [DataMember]
        private List<ConditionAndOr> _linkedConditionAndOrs = new List<ConditionAndOr>();
        [DataMember]
        private List<Condition> _linkedConditions = new List<Condition>();

        #endregion

        #region Properties

        [ComVisible(false)]
        public IExpression LeftExpression
        {
            get { return _left; }
        }

        [ComVisible(false)]
        public ExpressionOperator Operator
        {
            get { return _operator; }
        }

        [ComVisible(false)]
        public IExpression RightExpression
        {
            get { return _right; }
        }

        [ComVisible(false)]
        public bool NotFlag
        {
            get { return _notFlag; }
        }

        [ComVisible(false)]
        public IList<ConditionAndOr> LinkedConditionAndOrs
        {
            get { return _linkedConditionAndOrs; }
        }

        [ComVisible(false)]
        public IList<Condition> LinkedConditions
        {
            get { return _linkedConditions; }
        }

        #endregion

        #region Constructors

        internal Condition(IExpression left, ExpressionOperator op, IExpression right)
        {
            _left = left;
            _operator = op;
            _right = right;
        }

        #endregion

        #region Public Methods

        public Condition And(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            var retCondition = (Condition)Clone();
            retCondition._linkedConditionAndOrs.Add((int)ConditionAndOr.And);
            retCondition._linkedConditions.Add(condition);

            return retCondition;
        }

        public Condition Or(Condition condition)
        {
            if (ReferenceEquals(condition, null))
                throw new ArgumentNullException("condition");

            var retCondition = (Condition)Clone();
            retCondition._linkedConditionAndOrs.Add(ConditionAndOr.Or);
            retCondition._linkedConditions.Add(condition);

            return retCondition;
        }

        public Condition Not()
        {
            var retCondition = (Condition)Clone();
            retCondition._notFlag = !retCondition._notFlag;
            return retCondition;
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var rightExpr = (RightExpression == null ? null : (IExpression)RightExpression.Clone());
            var clone = new Condition((IExpression)LeftExpression.Clone(), Operator, rightExpr) {_notFlag = _notFlag};
            for (var i = 0; i < _linkedConditions.Count; ++i)
            {
                clone._linkedConditionAndOrs.Add(_linkedConditionAndOrs[i]);
                clone._linkedConditions.Add((Condition)_linkedConditions[i].Clone());
            }

            return clone;
        }

        internal override void UpdateIdentifiedParameterValue(string id, object value)
        {
            var leftExpr = LeftExpression as Expression;
            if (!ReferenceEquals(leftExpr, null))
                leftExpr.UpdateIdentifiedParameterValue(id, value);
            var rightExpr = RightExpression as Expression;
            if (!ReferenceEquals(rightExpr, null))
                rightExpr.UpdateIdentifiedParameterValue(id, value);

            foreach (var condition in _linkedConditions)
            {
                condition.UpdateIdentifiedParameterValue(id, value);
            }
        }

        #endregion

        #region Operators

        public static bool operator true(Condition right)
        {
            return false;
        }

        public static bool operator false(Condition right)
        {
            return false;
        }

        public static Condition operator &(Condition left, Condition right)
        {
            if (ReferenceEquals(left, null))
                return right;
            
            if (ReferenceEquals(right, null))
                return left;

            return left.And(right);
        }

        public static Condition operator |(Condition left, Condition right)
        {
            if (ReferenceEquals(left, null))
                return right;

            if (ReferenceEquals(right, null))
                return left;

            return left.Or(right);
        }

        public static Condition operator !(Condition right)
        {
            return right.Not();
        }

        [ComVisible(false)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        [ComVisible(false)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
