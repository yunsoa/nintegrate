using System;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    [DataContract(Namespace = "http://nintegrate.com")]
    [KnownType("KnownTypes")]
    public class ParameterEqualsCondition : Condition
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal ParameterEqualsCondition(IExpression left, ExpressionOperator op, IExpression right)
            : base(left, op, right)
        {
        }

        #endregion

        #region Public Properties

        public new IParameterExpression LeftExpression
        {
            get
            {
                return base.LeftExpression as IParameterExpression;
            }
        }

        public new IParameterExpression RightExpression
        {
            get
            {
                return base.RightExpression as IParameterExpression;
            }
        }

        #endregion
    }
}
