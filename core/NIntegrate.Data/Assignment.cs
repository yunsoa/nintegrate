using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    [DataContract]
    [KnownType("KnownTypes")]
    public sealed class Assignment : ICloneable
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

        [ComVisible(false)]
        public IColumn LeftColumn
        {
            get { return _left; }
        }

        [ComVisible(false)]
        public IExpression RightExpression
        {
            get { return _right; }
        }

        [ComVisible(false)]
        public ICollection<Assignment> LinkedAssignments
        {
            get { return _linkedAssignments; }
        }

        #endregion

        #region Public Methods

        public Assignment And(Assignment assignment)
        {
            if (ReferenceEquals(assignment, null))
                throw new ArgumentNullException("assignment");

            var result = (Assignment)Clone();
            result.LinkedAssignments.Add(assignment);

            return result;
        }

        #endregion

        #region Operators

        public static bool operator true(Assignment right)
        {
            return false;
        }

        public static bool operator false(Assignment right)
        {
            return false;
        }

        public static Assignment operator &(Assignment left, Assignment right)
        {
            if (ReferenceEquals(left, null))
                return right;

            if (ReferenceEquals(right, null))
                return left;

            return left.And(right);
        }

        public static Assignment operator |(Assignment left, Assignment right)
        {
            throw new NotSupportedException();
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

        #region ICloneable Members

        [ComVisible(false)]
        public object Clone()
        {
            var rightExpr = (RightExpression == null ? null : (IExpression)RightExpression.Clone());
            var clone = new Assignment((IColumn)LeftColumn.Clone(), rightExpr);
            foreach (var item in LinkedAssignments)
            {
                clone.LinkedAssignments.Add((Assignment)item.Clone());
            }

            return clone;
        }

        #endregion
    }

}
