using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum ConditionAndOr
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        And,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Or
    }

    [DataContract(Namespace = "http://nintegrate.com")]
    [KnownType("KnownTypes")]
    public class Condition : BooleanExpression
    {
        [DataMember]
        private IExpression _left;

        [DataMember]
        private ExpressionOperator _operator;

        [DataMember]
        private IExpression _right;

        [DataMember]
        private bool _isNot;

        [DataMember]
        private List<ConditionAndOr> _linkedConditionAndOrs = new List<ConditionAndOr>();

        [DataMember]
        private List<Condition> _linkedConditions = new List<Condition>();

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
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
        public bool IsNot
        {
            get { return _isNot; }
        }

        [ComVisible(false)]
        public ICollection<ConditionAndOr> LinkedConditionAndOrs
        {
            get { return _linkedConditionAndOrs; }
        }

        [ComVisible(false)]
        public ICollection<Condition> LinkedConditions
        {
            get { return _linkedConditions; }
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
            retCondition._isNot = !retCondition._isNot;
            return retCondition;
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var rightExpr = (RightExpression == null ? null : (IExpression)RightExpression.Clone());
            var clone = new Condition((IExpression)LeftExpression.Clone(), Operator, rightExpr) {_isNot = _isNot};
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
