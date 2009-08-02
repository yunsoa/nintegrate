using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    [DataContract]
    [KnownType("KnownTypes")]
    public sealed class Assignment
    {
        [DataMember]
        private IColumn _left;

        [DataMember]
        private IExpression _right;

        [DataMember]
        private List<Assignment> _linkedAssignments = new List<Assignment>();

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment"/> class.
        /// </summary>
        /// <param name="left">The left column.</param>
        /// <param name="right">The right expression.</param>
        internal Assignment(IColumn left, IExpression right)
        {
            if (ReferenceEquals(left, null))
                throw new ArgumentNullException("left");

            if (ReferenceEquals(right, null))
                right = NullExpression.Value;

            _left = left;
            _right = right;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the left column of the assignment.
        /// </summary>
        /// <value>The left column.</value>
        [ComVisible(false)]
        public IColumn LeftColumn
        {
            get { return _left; }
        }

        /// <summary>
        /// Get the right expression of the assignment.
        /// </summary>
        /// <value>The right expression.</value>
        [ComVisible(false)]
        public IExpression RightExpression
        {
            get { return _right; }
        }

        #endregion
    }
}
