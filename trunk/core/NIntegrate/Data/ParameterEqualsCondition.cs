using System;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    /// <summary>
    /// 
    /// </summary>
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

        /// <summary>
        /// Gets the left expression.
        /// </summary>
        /// <value>The left expression.</value>
        public new IParameterExpression LeftExpression
        {
            get
            {
                return base.LeftExpression as IParameterExpression;
            }
        }

        /// <summary>
        /// Gets the right expression.
        /// </summary>
        /// <value>The right expression.</value>
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
