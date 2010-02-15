using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    [DataContract]
    [KnownType("KnownTypes")]
    public abstract class Expression : IExpression
    {
        [DataMember]
        private string _sql;

        [DataMember]
        private List<IExpression> _childExpressions = new List<IExpression>();

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        protected Expression() { }

        protected Expression(string sql, IList<IExpression> childExpressions)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            Sql = sql;
            if (childExpressions != null)
            {
                foreach (IExpression expr in childExpressions)
                    _childExpressions.Add(expr);
            }
        }

        #endregion

        #region IExpression Members

        [ComVisible(false)]
        public string Sql
        {
            get { return _sql; }
            internal set { _sql = value; }
        }

        [ComVisible(false)]
        ICollection<IExpression> IExpression.ChildExpressions
        {
            get { return _childExpressions; }
        }

        internal List<IExpression> ChildExpressions
        {
            get { return _childExpressions; }
        }

        public Int32Expression Count()
        {
            return Count(false);
        }

        public Int32Expression Count(bool distinct)
        {
            var list = new List<IExpression>();
            foreach (var item in _childExpressions)
            {
                list.Add((IExpression)item.Clone());
            }
            var countExpr = new Int32Expression(Sql, list);
            if (distinct)
                countExpr.Sql = "COUNT(DISTINCT " + countExpr.Sql + ")";
            else
                countExpr.Sql = "COUNT(" + countExpr.Sql + ")";

            return countExpr;
        }

        public abstract Condition In(IEnumerable values);

        public Condition NotIn(IEnumerable values)
        {
            return In(values).Not();
        }

        #endregion

        #region ICloneable Members

        [ComVisible(false)]
        public virtual object Clone()
        {
            var clone = (Expression)CreateInstance();

            clone.Sql = Sql;
            foreach (var expr in _childExpressions)
            {
                clone._childExpressions.Add((IExpression)expr.Clone());
            }

            return clone;
        }

        [ComVisible(false)]
        protected abstract object CreateInstance();

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            var objExpr = obj as Expression;
            if (objExpr == null)
                return false;

            if (Sql == objExpr.Sql && _childExpressions.Count == objExpr._childExpressions.Count)
            {
                for (int i = 0; i < _childExpressions.Count; ++i)
                {
                    if (!_childExpressions[i].Equals(objExpr._childExpressions[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }

        [ComVisible(false)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [ComVisible(false)]
        internal virtual void UpdateIdentifiedParameterValue(string id, object value)
        {
            var paramExpr = this as IParameterExpression;
            if (paramExpr != null && paramExpr.ID == id)
                paramExpr.Value = value;

            foreach (var item in _childExpressions)
            {
                var expr = item as Expression;
                if (!ReferenceEquals(expr, null))
                    expr.UpdateIdentifiedParameterValue(id, value);
            }
        }

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class BooleanExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal BooleanExpression()
        {
            Sql = "0";
        }

        internal BooleanExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new BooleanExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(bool value)
        {
            return new Condition(this, ExpressionOperator.Equals, new BooleanParameterExpression(value));
        }

        public Condition Equals(BooleanExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(bool value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new BooleanParameterExpression(value));
        }

        public Condition NotEquals(BooleanExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(BooleanExpression left, BooleanExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(BooleanExpression left, BooleanExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(BooleanExpression left, bool right)
        {
            return new Condition(left, ExpressionOperator.Equals, new BooleanParameterExpression(right));
        }

        public static Condition operator !=(BooleanExpression left, bool right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new BooleanParameterExpression(right));
        }

        public static Condition operator ==(bool left, BooleanExpression right)
        {
            return new Condition(new BooleanParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(bool left, BooleanExpression right)
        {
            return new Condition(new BooleanParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as BooleanColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (BooleanColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new BooleanColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (var value in values)
                {
                    if (value is bool)
                        expr.Add(new BooleanParameterExpression((bool)value));
                    else
                        throw new ArgumentException("Each value in values should be boolean type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class ByteExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal ByteExpression()
        {
            Sql = "0";
        }

        internal ByteExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new ByteExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(byte value)
        {
            return new Condition(this, ExpressionOperator.Equals, new ByteParameterExpression(value));
        }

        public Condition Equals(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(byte value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new ByteParameterExpression(value));
        }

        public Condition NotEquals(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.Equals, new ByteParameterExpression(right));
        }

        public static Condition operator !=(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new ByteParameterExpression(right));
        }

        public static Condition operator ==(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Bitwise

        public ByteExpression BitwiseAnd(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression BitwiseAnd(ByteExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public ByteExpression BitwiseOr(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression BitwiseOr(ByteExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public ByteExpression BitwiseXor(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression BitwiseXor(ByteExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public ByteExpression BitwiseNot()
        {
            var expr = (ByteExpression)Clone();

            expr.Sql = ExpressionHelper.ToString(ExpressionOperator.BitwiseNot) + "(" + expr.Sql + ")";

            return expr;
        }

        #endregion

        #region Between

        public Condition Between(byte left, byte right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new ByteParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new ByteParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(ByteExpression left, byte right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new ByteParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(byte left, ByteExpression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new ByteParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(ByteExpression left, ByteExpression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(byte value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new ByteParameterExpression(value));
        }

        public Condition GreaterThan(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(byte value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new ByteParameterExpression(value));
        }

        public Condition LessThan(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new ByteParameterExpression(right));
        }

        public static Condition operator <(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new ByteParameterExpression(right));
        }

        public static Condition operator >(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(byte value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new ByteParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(byte value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new ByteParameterExpression(value));
        }

        public Condition LessThanOrEquals(ByteExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(ByteExpression left, ByteExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new ByteParameterExpression(right));
        }

        public static Condition operator <=(ByteExpression left, byte right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new ByteParameterExpression(right));
        }

        public static Condition operator >=(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(byte left, ByteExpression right)
        {
            return new Condition(new ByteParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public ByteExpression Add(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression Add(ByteExpression value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public ByteExpression Subtract(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression Subtract(ByteExpression value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public ByteExpression Multiply(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression Multiply(ByteExpression value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public ByteExpression Divide(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression Divide(ByteExpression value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public ByteExpression Mod(byte value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(value));

            return expr;
        }

        public ByteExpression Mod(ByteExpression value)
        {
            var expr = (ByteExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static ByteExpression operator +(ByteExpression left, ByteExpression right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator +(byte left, ByteExpression right)
        {
            var expr = new ByteParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator +(ByteExpression left, byte right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(right));

            return expr;
        }

        public static ByteExpression operator -(ByteExpression left, ByteExpression right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator -(byte left, ByteExpression right)
        {
            var expr = new ByteParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator -(ByteExpression left, byte right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(right));

            return expr;
        }

        public static ByteExpression operator *(ByteExpression left, ByteExpression right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator *(byte left, ByteExpression right)
        {
            var expr = new ByteParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator *(ByteExpression left, byte right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(right));

            return expr;
        }

        public static ByteExpression operator /(ByteExpression left, ByteExpression right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator /(byte left, ByteExpression right)
        {
            var expr = new ByteParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator /(ByteExpression left, byte right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(right));

            return expr;
        }

        public static ByteExpression operator %(ByteExpression left, ByteExpression right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator %(byte left, ByteExpression right)
        {
            var expr = new ByteParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static ByteExpression operator %(ByteExpression left, byte right)
        {
            var expr = (ByteExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new ByteParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public ByteExpression Avg()
        {
            var expr = (ByteExpression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public ByteExpression Max()
        {
            var expr = (ByteExpression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public ByteExpression Min()
        {
            var expr = (ByteExpression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public ByteExpression Sum()
        {
            var expr = (ByteExpression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as ByteColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (ByteColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new ByteColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is byte)
                        expr.Add(new ByteParameterExpression((byte)value));
                    else
                        throw new ArgumentException("Each value in values should be byte type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class Int16Expression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal Int16Expression()
        {
            Sql = "0";
        }

        internal Int16Expression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new Int16Expression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(short value)
        {
            return new Condition(this, ExpressionOperator.Equals, new Int16ParameterExpression(value));
        }

        public Condition Equals(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(short value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new Int16ParameterExpression(value));
        }

        public Condition NotEquals(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.Equals, new Int16ParameterExpression(right));
        }

        public static Condition operator !=(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new Int16ParameterExpression(right));
        }

        public static Condition operator ==(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Bitwise

        public Int16Expression BitwiseAnd(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression BitwiseAnd(Int16Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int16Expression BitwiseOr(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression BitwiseOr(Int16Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int16Expression BitwiseXor(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression BitwiseXor(Int16Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int16Expression BitwiseNot()
        {
            var expr = (Int16Expression)Clone();

            expr.Sql = ExpressionHelper.ToString(ExpressionOperator.BitwiseNot) + "(" + expr.Sql + ")";

            return expr;
        }

        #endregion

        #region Between

        public Condition Between(short left, short right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int16ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new Int16ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int16Expression left, short right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new Int16ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(short left, Int16Expression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int16ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int16Expression left, Int16Expression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(short value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new Int16ParameterExpression(value));
        }

        public Condition GreaterThan(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(short value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new Int16ParameterExpression(value));
        }

        public Condition LessThan(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new Int16ParameterExpression(right));
        }

        public static Condition operator <(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new Int16ParameterExpression(right));
        }

        public static Condition operator >(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(short value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new Int16ParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(short value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new Int16ParameterExpression(value));
        }

        public Condition LessThanOrEquals(Int16Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(Int16Expression left, Int16Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new Int16ParameterExpression(right));
        }

        public static Condition operator <=(Int16Expression left, short right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new Int16ParameterExpression(right));
        }

        public static Condition operator >=(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(short left, Int16Expression right)
        {
            return new Condition(new Int16ParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public Int16Expression Add(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression Add(Int16Expression value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int16Expression Subtract(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression Subtract(Int16Expression value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int16Expression Multiply(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression Multiply(Int16Expression value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int16Expression Divide(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression Divide(Int16Expression value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int16Expression Mod(short value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(value));

            return expr;
        }

        public Int16Expression Mod(Int16Expression value)
        {
            var expr = (Int16Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static Int16Expression operator +(Int16Expression left, Int16Expression right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator +(short left, Int16Expression right)
        {
            var expr = new Int16ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator +(Int16Expression left, short right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(right));

            return expr;
        }

        public static Int16Expression operator -(Int16Expression left, Int16Expression right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator -(short left, Int16Expression right)
        {
            var expr = new Int16ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator -(Int16Expression left, short right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(right));

            return expr;
        }

        public static Int16Expression operator *(Int16Expression left, Int16Expression right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator *(short left, Int16Expression right)
        {
            var expr = new Int16ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator *(Int16Expression left, short right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(right));

            return expr;
        }

        public static Int16Expression operator /(Int16Expression left, Int16Expression right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator /(short left, Int16Expression right)
        {
            var expr = new Int16ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator /(Int16Expression left, short right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(right));

            return expr;
        }

        public static Int16Expression operator %(Int16Expression left, Int16Expression right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator %(short left, Int16Expression right)
        {
            var expr = new Int16ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int16Expression operator %(Int16Expression left, short right)
        {
            var expr = (Int16Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int16ParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public Int16Expression Avg()
        {
            var expr = (Int16Expression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public Int16Expression Max()
        {
            var expr = (Int16Expression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public Int16Expression Min()
        {
            var expr = (Int16Expression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public Int16Expression Sum()
        {
            var expr = (Int16Expression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as Int16Column;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (Int16Column)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new Int16Column(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is short)
                        expr.Add(new Int16ParameterExpression((short)value));
                    else
                        throw new ArgumentException("Each value in values should be short type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class Int32Expression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal Int32Expression()
        {
            Sql = "0";
        }

        internal Int32Expression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Int32Expression Members

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new Int32Expression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(int value)
        {
            return new Condition(this, ExpressionOperator.Equals, new Int32ParameterExpression(value));
        }

        public Condition Equals(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(int value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new Int32ParameterExpression(value));
        }

        public Condition NotEquals(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.Equals, new Int32ParameterExpression(right));
        }

        public static Condition operator !=(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new Int32ParameterExpression(right));
        }

        public static Condition operator ==(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Bitwise

        public Int32Expression BitwiseAnd(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression BitwiseAnd(Int32Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int32Expression BitwiseOr(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression BitwiseOr(Int32Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int32Expression BitwiseXor(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression BitwiseXor(Int32Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int32Expression BitwiseNot()
        {
            var expr = (Int32Expression)Clone();

            expr.Sql = ExpressionHelper.ToString(ExpressionOperator.BitwiseNot) + "(" + expr.Sql + ")";

            return expr;
        }

        #endregion

        #region Between

        public Condition Between(int left, int right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int32ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new Int32ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int32Expression left, int right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new Int32ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(int left, Int32Expression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int32ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int32Expression left, Int32Expression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(int value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new Int32ParameterExpression(value));
        }

        public Condition GreaterThan(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(int value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new Int32ParameterExpression(value));
        }

        public Condition LessThan(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new Int32ParameterExpression(right));
        }

        public static Condition operator <(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new Int32ParameterExpression(right));
        }

        public static Condition operator >(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(int value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new Int32ParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(int value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new Int32ParameterExpression(value));
        }

        public Condition LessThanOrEquals(Int32Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(Int32Expression left, Int32Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new Int32ParameterExpression(right));
        }

        public static Condition operator <=(Int32Expression left, int right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new Int32ParameterExpression(right));
        }

        public static Condition operator >=(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(int left, Int32Expression right)
        {
            return new Condition(new Int32ParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public Int32Expression Add(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression Add(Int32Expression value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int32Expression Subtract(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression Subtract(Int32Expression value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int32Expression Multiply(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression Multiply(Int32Expression value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int32Expression Divide(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression Divide(Int32Expression value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int32Expression Mod(int value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(value));

            return expr;
        }

        public Int32Expression Mod(Int32Expression value)
        {
            var expr = (Int32Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static Int32Expression operator +(Int32Expression left, Int32Expression right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator +(int left, Int32Expression right)
        {
            var expr = new Int32ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator +(Int32Expression left, int right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(right));

            return expr;
        }

        public static Int32Expression operator -(Int32Expression left, Int32Expression right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator -(int left, Int32Expression right)
        {
            var expr = new Int32ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator -(Int32Expression left, int right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(right));

            return expr;
        }

        public static Int32Expression operator *(Int32Expression left, Int32Expression right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator *(int left, Int32Expression right)
        {
            var expr = new Int32ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator *(Int32Expression left, int right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(right));

            return expr;
        }

        public static Int32Expression operator /(Int32Expression left, Int32Expression right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator /(int left, Int32Expression right)
        {
            var expr = new Int32ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator /(Int32Expression left, int right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(right));

            return expr;
        }

        public static Int32Expression operator %(Int32Expression left, Int32Expression right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator %(int left, Int32Expression right)
        {
            var expr = new Int32ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int32Expression operator %(Int32Expression left, int right)
        {
            var expr = (Int32Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int32ParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public Int32Expression Avg()
        {
            var expr = (Int32Expression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public Int32Expression Max()
        {
            var expr = (Int32Expression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public Int32Expression Min()
        {
            var expr = (Int32Expression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public Int32Expression Sum()
        {
            var expr = (Int32Expression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as Int32Column;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (Int32Column)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new Int32Column(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is int)
                        expr.Add(new Int32ParameterExpression((int)value));
                    else
                        throw new ArgumentException("Each value in values should be int type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class Int64Expression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal Int64Expression()
        {
            Sql = "0";
        }

        internal Int64Expression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new Int64Expression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(long value)
        {
            return new Condition(this, ExpressionOperator.Equals, new Int64ParameterExpression(value));
        }

        public Condition Equals(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(long value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new Int64ParameterExpression(value));
        }

        public Condition NotEquals(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.Equals, new Int64ParameterExpression(right));
        }

        public static Condition operator !=(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new Int64ParameterExpression(right));
        }

        public static Condition operator ==(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Bitwise

        public Int64Expression BitwiseAnd(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression BitwiseAnd(Int64Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseAnd) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int64Expression BitwiseOr(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression BitwiseOr(Int64Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseOr) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int64Expression BitwiseXor(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression BitwiseXor(Int64Expression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.BitwiseXor) + " ?";
            expr.ChildExpressions.Add(value);

            return expr;
        }

        public Int64Expression BitwiseNot()
        {
            var expr = (Int64Expression)Clone();

            expr.Sql = ExpressionHelper.ToString(ExpressionOperator.BitwiseNot) + "(" + expr.Sql + ")";

            return expr;
        }

        #endregion

        #region Between

        public Condition Between(long left, long right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int64ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new Int64ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int64Expression left, long right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new Int64ParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(long left, Int64Expression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new Int64ParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(Int64Expression left, Int64Expression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(long value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new Int64ParameterExpression(value));
        }

        public Condition GreaterThan(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(long value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new Int64ParameterExpression(value));
        }

        public Condition LessThan(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new Int64ParameterExpression(right));
        }

        public static Condition operator <(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new Int64ParameterExpression(right));
        }

        public static Condition operator >(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(long value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new Int64ParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(long value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new Int64ParameterExpression(value));
        }

        public Condition LessThanOrEquals(Int64Expression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(Int64Expression left, Int64Expression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new Int64ParameterExpression(right));
        }

        public static Condition operator <=(Int64Expression left, long right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new Int64ParameterExpression(right));
        }

        public static Condition operator >=(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(long left, Int64Expression right)
        {
            return new Condition(new Int64ParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public Int64Expression Add(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression Add(Int64Expression value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int64Expression Subtract(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression Subtract(Int64Expression value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int64Expression Multiply(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression Multiply(Int64Expression value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int64Expression Divide(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression Divide(Int64Expression value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public Int64Expression Mod(long value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(value));

            return expr;
        }

        public Int64Expression Mod(Int64Expression value)
        {
            var expr = (Int64Expression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static Int64Expression operator +(Int64Expression left, Int64Expression right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator +(long left, Int64Expression right)
        {
            var expr = new Int64ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator +(Int64Expression left, long right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(right));

            return expr;
        }

        public static Int64Expression operator -(Int64Expression left, Int64Expression right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator -(long left, Int64Expression right)
        {
            var expr = new Int64ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator -(Int64Expression left, long right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(right));

            return expr;
        }

        public static Int64Expression operator *(Int64Expression left, Int64Expression right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator *(long left, Int64Expression right)
        {
            var expr = new Int64ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator *(Int64Expression left, long right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(right));

            return expr;
        }

        public static Int64Expression operator /(Int64Expression left, Int64Expression right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator /(long left, Int64Expression right)
        {
            var expr = new Int64ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator /(Int64Expression left, long right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(right));

            return expr;
        }

        public static Int64Expression operator %(Int64Expression left, Int64Expression right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator %(long left, Int64Expression right)
        {
            var expr = new Int64ParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static Int64Expression operator %(Int64Expression left, long right)
        {
            var expr = (Int64Expression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new Int64ParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public Int64Expression Avg()
        {
            var expr = (Int64Expression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public Int64Expression Max()
        {
            var expr = (Int64Expression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public Int64Expression Min()
        {
            var expr = (Int64Expression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public Int64Expression Sum()
        {
            var expr = (Int64Expression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as Int64Column;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (Int64Column)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new Int64Column(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is long)
                        expr.Add(new Int64ParameterExpression((long)value));
                    else
                        throw new ArgumentException("Each value in values should be long type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class DateTimeExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal DateTimeExpression()
        {
            Sql = "getdate()";
        }

        internal DateTimeExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Public Methods

        protected override object CreateInstance()
        {
            return new DateTimeExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(DateTime value)
        {
            return new Condition(this, ExpressionOperator.Equals, new DateTimeParameterExpression(value));
        }

        public Condition Equals(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(DateTime value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new DateTimeParameterExpression(value));
        }

        public Condition NotEquals(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.Equals, new DateTimeParameterExpression(right));
        }

        public static Condition operator !=(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new DateTimeParameterExpression(right));
        }

        public static Condition operator ==(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Between

        public Condition Between(DateTime left, DateTime right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DateTimeParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new DateTimeParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DateTimeExpression left, DateTime right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new DateTimeParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DateTime left, DateTimeExpression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DateTimeParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DateTimeExpression left, DateTimeExpression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(DateTime value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new DateTimeParameterExpression(value));
        }

        public Condition GreaterThan(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(DateTime value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new DateTimeParameterExpression(value));
        }

        public Condition LessThan(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            return ReferenceEquals(left, null)
                       ? new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right)
                       : new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new DateTimeParameterExpression(right));
        }

        public static Condition operator <(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new DateTimeParameterExpression(right));
        }

        public static Condition operator >(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(DateTime value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new DateTimeParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(DateTime value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new DateTimeParameterExpression(value));
        }

        public Condition LessThanOrEquals(DateTimeExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(DateTimeExpression left, DateTimeExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new DateTimeParameterExpression(right));
        }

        public static Condition operator <=(DateTimeExpression left, DateTime right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new DateTimeParameterExpression(right));
        }

        public static Condition operator >=(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(DateTime left, DateTimeExpression right)
        {
            return new Condition(new DateTimeParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + -

        public DateTimeExpression Add(DateTime value)
        {
            var expr = (DateTimeExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DateTimeParameterExpression(value));

            return expr;
        }

        public DateTimeExpression Add(DateTimeExpression value)
        {
            var expr = (DateTimeExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DateTimeExpression Subtract(DateTime value)
        {
            var expr = (DateTimeExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DateTimeParameterExpression(value));

            return expr;
        }

        public DateTimeExpression Subtract(DateTimeExpression value)
        {
            var expr = (DateTimeExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static DateTimeExpression operator +(DateTimeExpression left, DateTimeExpression right)
        {
            var expr = (DateTimeExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DateTimeExpression operator +(DateTime left, DateTimeExpression right)
        {
            var expr = new DateTimeParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DateTimeExpression operator +(DateTimeExpression left, DateTime right)
        {
            var expr = (DateTimeExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DateTimeParameterExpression(right));

            return expr;
        }

        public static DateTimeExpression operator -(DateTimeExpression left, DateTimeExpression right)
        {
            var expr = (DateTimeExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DateTimeExpression operator -(DateTime left, DateTimeExpression right)
        {
            var expr = new DateTimeParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DateTimeExpression operator -(DateTimeExpression left, DateTime right)
        {
            var expr = (DateTimeExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DateTimeParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public DateTimeExpression Max()
        {
            var expr = (DateTimeExpression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public DateTimeExpression Min()
        {
            var expr = (DateTimeExpression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as DateTimeColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (DateTimeColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new DateTimeColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is DateTime)
                        expr.Add(new DateTimeParameterExpression((DateTime)value));
                    else
                        throw new ArgumentException("Each value in values should be DateTime type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class StringExpression : Expression
    {
        [DataMember]
        private bool _isUnicode = true;

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal StringExpression()
        {
            Sql = "''";
        }

        internal StringExpression(bool isUnicode)
            : this()
        {
            _isUnicode = isUnicode;
        }

        internal StringExpression(bool isUnicode, string sql, IList<IExpression> childExpressions)
            : base(sql, childExpressions)
        {
            _isUnicode = isUnicode;
        }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new StringExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(string value)
        {
            if (value == null)
                return Equals((StringExpression)null);
            return new Condition(this, ExpressionOperator.Equals, new StringParameterExpression(value, IsUnicode));
        }

        public Condition Equals(StringExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(string value)
        {
            if (value == null)
                return NotEquals((StringExpression)null);
            return new Condition(this, ExpressionOperator.NotEquals, new StringParameterExpression(value, IsUnicode));
        }

        public Condition NotEquals(StringExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(StringExpression left, StringExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(right, NullExpression.Value))
            {
                return new Condition(left, ExpressionOperator.Is, right);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(StringExpression left, StringExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(right, NullExpression.Value))
            {
                return new Condition(left, ExpressionOperator.IsNot, right);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(StringExpression left, string right)
        {
            return left.Equals(right);
        }

        public static Condition operator !=(StringExpression left, string right)
        {
            return left.NotEquals(right);
        }

        public static Condition operator ==(string left, StringExpression right)
        {
            return right.Equals(left);
        }

        public static Condition operator !=(string left, StringExpression right)
        {
            return right.NotEquals(left);
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

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as StringColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (StringColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new StringColumn(this, columnName, IsUnicode);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is string)
                        expr.Add(new StringParameterExpression((string)value, IsUnicode));
                    else
                        throw new ArgumentException("Each value in values should be string type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #region String Operations

        public Condition Like(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            return new Condition(this, ExpressionOperator.Like, new StringParameterExpression(value, IsUnicode));
        }

        public Condition Like(StringExpression value)
        {
            if (ReferenceEquals(value, null))
                throw new ArgumentNullException("value");

            return new Condition(this, ExpressionOperator.Like, value);
        }

        public StringExpression ToLower()
        {
            var expr = (StringExpression)Clone();

            expr.Sql = "LOWER(" + expr.Sql + ")";

            return expr;
        }

        public StringExpression ToUpper()
        {
            var expr = (StringExpression)Clone();

            expr.Sql = "UPPER(" + expr.Sql + ")";

            return expr;
        }

        public StringExpression Trim()
        {
            var expr = (StringExpression)Clone();

            expr.Sql = "TRIM(" + expr.Sql + ")";

            return expr;
        }

        #endregion

        #endregion

        #region Properties

        public bool IsUnicode
        {
            get { return _isUnicode; }
            internal set { _isUnicode = value; }
        }

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class GuidExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal GuidExpression()
        {
            Sql = "newid()";
        }

        internal GuidExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new GuidExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(Guid value)
        {
            return new Condition(this, ExpressionOperator.Equals, new GuidParameterExpression(value));
        }

        public Condition Equals(GuidExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(Guid value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new GuidParameterExpression(value));
        }

        public Condition NotEquals(GuidExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(GuidExpression left, GuidExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(GuidExpression left, GuidExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(GuidExpression left, Guid right)
        {
            return new Condition(left, ExpressionOperator.Equals, new GuidParameterExpression(right));
        }

        public static Condition operator !=(GuidExpression left, Guid right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new GuidParameterExpression(right));
        }

        public static Condition operator ==(Guid left, GuidExpression right)
        {
            return new Condition(new GuidParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(Guid left, GuidExpression right)
        {
            return new Condition(new GuidParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as GuidColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (GuidColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new GuidColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is Guid)
                        expr.Add(new GuidParameterExpression((Guid)value));
                    else
                        throw new ArgumentException("Each value in values should be Guid type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class DoubleExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal DoubleExpression()
        {
            Sql = "0";
        }

        internal DoubleExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new DoubleExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(double value)
        {
            return new Condition(this, ExpressionOperator.Equals, new DoubleParameterExpression(value));
        }

        public Condition Equals(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(double value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new DoubleParameterExpression(value));
        }

        public Condition NotEquals(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.Equals, new DoubleParameterExpression(right));
        }

        public static Condition operator !=(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new DoubleParameterExpression(right));
        }

        public static Condition operator ==(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Between

        public Condition Between(double left, double right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DoubleParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new DoubleParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DoubleExpression left, double right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new DoubleParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(double left, DoubleExpression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DoubleParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DoubleExpression left, DoubleExpression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(double value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new DoubleParameterExpression(value));
        }

        public Condition GreaterThan(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(double value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new DoubleParameterExpression(value));
        }

        public Condition LessThan(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new DoubleParameterExpression(right));
        }

        public static Condition operator <(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new DoubleParameterExpression(right));
        }

        public static Condition operator >(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(double value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new DoubleParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(double value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new DoubleParameterExpression(value));
        }

        public Condition LessThanOrEquals(DoubleExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(DoubleExpression left, DoubleExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new DoubleParameterExpression(right));
        }

        public static Condition operator <=(DoubleExpression left, double right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new DoubleParameterExpression(right));
        }

        public static Condition operator >=(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(double left, DoubleExpression right)
        {
            return new Condition(new DoubleParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public DoubleExpression Add(double value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(value));

            return expr;
        }

        public DoubleExpression Add(DoubleExpression value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DoubleExpression Subtract(double value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(value));

            return expr;
        }

        public DoubleExpression Subtract(DoubleExpression value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DoubleExpression Multiply(double value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(value));

            return expr;
        }

        public DoubleExpression Multiply(DoubleExpression value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DoubleExpression Divide(double value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(value));

            return expr;
        }

        public DoubleExpression Divide(DoubleExpression value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DoubleExpression Mod(double value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(value));

            return expr;
        }

        public DoubleExpression Mod(DoubleExpression value)
        {
            var expr = (DoubleExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static DoubleExpression operator +(DoubleExpression left, DoubleExpression right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator +(double left, DoubleExpression right)
        {
            var expr = new DoubleParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator +(DoubleExpression left, double right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(right));

            return expr;
        }

        public static DoubleExpression operator -(DoubleExpression left, DoubleExpression right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator -(double left, DoubleExpression right)
        {
            var expr = new DoubleParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator -(DoubleExpression left, double right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(right));

            return expr;
        }

        public static DoubleExpression operator *(DoubleExpression left, DoubleExpression right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator *(double left, DoubleExpression right)
        {
            var expr = new DoubleParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator *(DoubleExpression left, double right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(right));

            return expr;
        }

        public static DoubleExpression operator /(DoubleExpression left, DoubleExpression right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator /(double left, DoubleExpression right)
        {
            var expr = new DoubleParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator /(DoubleExpression left, double right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(right));

            return expr;
        }

        public static DoubleExpression operator %(DoubleExpression left, DoubleExpression right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator %(double left, DoubleExpression right)
        {
            var expr = new DoubleParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DoubleExpression operator %(DoubleExpression left, double right)
        {
            var expr = (DoubleExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new DoubleParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public DoubleExpression Avg()
        {
            var expr = (DoubleExpression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public DoubleExpression Max()
        {
            var expr = (DoubleExpression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public DoubleExpression Min()
        {
            var expr = (DoubleExpression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public DoubleExpression Sum()
        {
            var expr = (DoubleExpression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as DoubleColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (DoubleColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new DoubleColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is double)
                        expr.Add(new DoubleParameterExpression((double)value));
                    else
                        throw new ArgumentException("Each value in values should be double type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class DecimalExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal DecimalExpression()
        {
            Sql = "0";
        }

        internal DecimalExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new DecimalExpression();
        }

        #endregion

        #region Operators

        #region Equals & NotEquals

        public Condition Equals(decimal value)
        {
            return new Condition(this, ExpressionOperator.Equals, new DecimalParameterExpression(value));
        }

        public Condition Equals(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.Equals, (IExpression)value ?? NullExpression.Value);
        }

        public Condition NotEquals(decimal value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, new DecimalParameterExpression(value));
        }

        public Condition NotEquals(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.NotEquals, (IExpression)value ?? NullExpression.Value);
        }

        public static Condition operator ==(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.Is, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.Is, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.IsNot, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(right, ExpressionOperator.IsNot, NullExpression.Value);
            }
            return new Condition(left, ExpressionOperator.NotEquals, right);
        }

        public static Condition operator ==(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.Equals, new DecimalParameterExpression(right));
        }

        public static Condition operator !=(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.NotEquals, new DecimalParameterExpression(right));
        }

        public static Condition operator ==(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.Equals, right);
        }

        public static Condition operator !=(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.NotEquals, right);
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

        #region Between

        public Condition Between(decimal left, decimal right, bool includeLeft, bool includeRight)
        {
            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DecimalParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, new DecimalParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DecimalExpression left, decimal right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, new DecimalParameterExpression(right));
            return leftCondition.And(rightCondition);
        }

        public Condition Between(decimal left, DecimalExpression right, bool includeLeft, bool includeRight)
        {
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, new DecimalParameterExpression(left));
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        public Condition Between(DecimalExpression left, DecimalExpression right, bool includeLeft, bool includeRight)
        {
            if (left == null)
                throw new ArgumentNullException("left");
            if (right == null)
                throw new ArgumentNullException("right");

            ExpressionOperator leftOp;
            ExpressionOperator rightOp;
            ExpressionHelper.GetLeftRightOperatorsForBetween(includeLeft, includeRight, out leftOp, out rightOp);

            var leftCondition = new Condition(this, leftOp, left);
            var rightCondition = new Condition(this, rightOp, right);
            return leftCondition.And(rightCondition);
        }

        #endregion

        #region GreaterThan & LessThan

        public Condition GreaterThan(decimal value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, new DecimalParameterExpression(value));
        }

        public Condition GreaterThan(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThan, value);
        }

        public Condition LessThan(decimal value)
        {
            return new Condition(this, ExpressionOperator.LessThan, new DecimalParameterExpression(value));
        }

        public Condition LessThan(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThan, value);
        }

        public static Condition operator >(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThan, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThan, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThan, right);
            }
            return new Condition(left, ExpressionOperator.LessThan, right);
        }

        public static Condition operator >(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.GreaterThan, new DecimalParameterExpression(right));
        }

        public static Condition operator <(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.LessThan, new DecimalParameterExpression(right));
        }

        public static Condition operator >(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.GreaterThan, right);
        }

        public static Condition operator <(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.LessThan, right);
        }

        #endregion

        #region GreaterThanOrEquals & LessThanOrEquals

        public Condition GreaterThanOrEquals(decimal value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, new DecimalParameterExpression(value));
        }

        public Condition GreaterThanOrEquals(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.GreaterThanOrEquals, value);
        }

        public Condition LessThanOrEquals(decimal value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, new DecimalParameterExpression(value));
        }

        public Condition LessThanOrEquals(DecimalExpression value)
        {
            return new Condition(this, ExpressionOperator.LessThanOrEquals, value);
        }

        public static Condition operator >=(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.GreaterThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.GreaterThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(DecimalExpression left, DecimalExpression right)
        {
            if (ReferenceEquals(right, null))
            {
                return new Condition(left, ExpressionOperator.LessThanOrEquals, NullExpression.Value);
            }
            if (ReferenceEquals(left, null))
            {
                return new Condition(NullExpression.Value, ExpressionOperator.LessThanOrEquals, right);
            }
            return new Condition(left, ExpressionOperator.LessThanOrEquals, right);
        }

        public static Condition operator >=(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.GreaterThanOrEquals, new DecimalParameterExpression(right));
        }

        public static Condition operator <=(DecimalExpression left, decimal right)
        {
            return new Condition(left, ExpressionOperator.LessThanOrEquals, new DecimalParameterExpression(right));
        }

        public static Condition operator >=(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.GreaterThanOrEquals, right);
        }

        public static Condition operator <=(decimal left, DecimalExpression right)
        {
            return new Condition(new DecimalParameterExpression(left), ExpressionOperator.LessThanOrEquals, right);
        }

        #endregion

        #region + - * / %

        public DecimalExpression Add(decimal value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(value));

            return expr;
        }

        public DecimalExpression Add(DecimalExpression value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DecimalExpression Subtract(decimal value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(value));

            return expr;
        }

        public DecimalExpression Subtract(DecimalExpression value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DecimalExpression Multiply(decimal value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(value));

            return expr;
        }

        public DecimalExpression Multiply(DecimalExpression value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DecimalExpression Divide(decimal value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(value));

            return expr;
        }

        public DecimalExpression Divide(DecimalExpression value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public DecimalExpression Mod(decimal value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(value));

            return expr;
        }

        public DecimalExpression Mod(DecimalExpression value)
        {
            var expr = (DecimalExpression)Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(ReferenceEquals(value, null) ? NullExpression.Value : (IExpression)value);

            return expr;
        }

        public static DecimalExpression operator +(DecimalExpression left, DecimalExpression right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator +(decimal left, DecimalExpression right)
        {
            var expr = new DecimalParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator +(DecimalExpression left, decimal right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Add) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(right));

            return expr;
        }

        public static DecimalExpression operator -(DecimalExpression left, DecimalExpression right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator -(decimal left, DecimalExpression right)
        {
            var expr = new DecimalParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator -(DecimalExpression left, decimal right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Subtract) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(right));

            return expr;
        }

        public static DecimalExpression operator *(DecimalExpression left, DecimalExpression right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator *(decimal left, DecimalExpression right)
        {
            var expr = new DecimalParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator *(DecimalExpression left, decimal right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Multiply) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(right));

            return expr;
        }

        public static DecimalExpression operator /(DecimalExpression left, DecimalExpression right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator /(decimal left, DecimalExpression right)
        {
            var expr = new DecimalParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator /(DecimalExpression left, decimal right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Divide) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(right));

            return expr;
        }

        public static DecimalExpression operator %(DecimalExpression left, DecimalExpression right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator %(decimal left, DecimalExpression right)
        {
            var expr = new DecimalParameterExpression(left);

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(right);

            return expr;
        }

        public static DecimalExpression operator %(DecimalExpression left, decimal right)
        {
            var expr = (DecimalExpression)left.Clone();

            expr.Sql += " " + ExpressionHelper.ToString(ExpressionOperator.Mod) + " ?";
            expr.ChildExpressions.Add(new DecimalParameterExpression(right));

            return expr;
        }

        #endregion

        #region Aggregation

        public DecimalExpression Avg()
        {
            var expr = (DecimalExpression)Clone();
            expr.Sql = "AVG(" + expr.Sql + ")";
            return expr;
        }

        public DecimalExpression Max()
        {
            var expr = (DecimalExpression)Clone();
            expr.Sql = "MAX(" + expr.Sql + ")";
            return expr;
        }

        public DecimalExpression Min()
        {
            var expr = (DecimalExpression)Clone();
            expr.Sql = "MIN(" + expr.Sql + ")";
            return expr;
        }

        public DecimalExpression Sum()
        {
            var expr = (DecimalExpression)Clone();
            expr.Sql = "SUM(" + expr.Sql + ")";
            return expr;
        }

        #endregion

        #region Others

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = this as DecimalColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (DecimalColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new DecimalColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            var expr = new ExpressionCollection();
            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value is decimal)
                        expr.Add(new DecimalParameterExpression((decimal)value));
                    else
                        throw new ArgumentException("Each value in values should be decimal type");
                }
            }
            if (((ICollection<IExpression>)expr).Count == 0)
                throw new ArgumentNullException("values");

            return new Condition(this, ExpressionOperator.In, expr);
        }

        #endregion

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public class BinaryExpression : Expression
    {
        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        internal BinaryExpression()
        {
            Sql = "0x0";
        }

        internal BinaryExpression(string sql, IList<IExpression> childExpressions) : base(sql, childExpressions) { }

        #endregion

        #region Protected Methods

        protected override object CreateInstance()
        {
            return new BinaryExpression();
        }

        #endregion

        #region Operators

        public IColumn As(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            var column = ((object)this) as BinaryColumn;
            if (!ReferenceEquals(column, null))
            {
                var asColumn = (BinaryColumn)column.Clone();
                asColumn._columnName = columnName;
                return asColumn;
            }

            return new BinaryColumn(this, columnName);
        }

        public override Condition In(IEnumerable values)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    [CollectionDataContract]
    [KnownType("KnownTypes")]
    internal sealed class ExpressionCollection : IExpression, ICollection<IExpression>
    {
        private readonly List<IExpression> _list = new List<IExpression>();

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region IExpression Members

        [ComVisible(false)]
        public string Sql
        {
            get
            {
                if (ChildExpressions == null || ChildExpressions.Count == 0)
                    return "NULL";

                var sb = new StringBuilder();
                sb.Append("(");
                var separate = string.Empty;
                for (var i = 0; i < _list.Count; ++i)
                {
                    sb.Append(separate);
                    sb.Append("?");

                    separate = ", ";
                }
                sb.Append(")");

                return sb.ToString();
            }
        }

        [ComVisible(false)]
        public ICollection<IExpression> ChildExpressions
        {
            get { return _list; }
        }

        [ComVisible(false)]
        public Int32Expression Count(bool distinct)
        {
            //should never be called
            throw new NotSupportedException();
        }

        [ComVisible(false)]
        public Condition In(IEnumerable values)
        {
            //should never be called
            throw new NotSupportedException();
        }

        [ComVisible(false)]
        public Condition NotIn(IEnumerable values)
        {
            //should never be called
            throw new NotSupportedException();
        }

        #endregion

        #region ICollection<IExpression> Members

        public void Add(IExpression item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(IExpression item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(IExpression[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        int ICollection<IExpression>.Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IExpression item)
        {
            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<IExpression> Members

        public IEnumerator<IExpression> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region ICloneable Members

        [ComVisible(false)]
        public object Clone()
        {
            var clone = new ExpressionCollection();
            foreach (var expr in _list)
            {
                clone._list.Add((IExpression)expr.Clone());
            }
            return clone;
        }

        #endregion
    }

    [DataContract]
    [KnownType("KnownTypes")]
    public sealed class NullExpression : IExpression
    {
        public static readonly NullExpression Value = new NullExpression();

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        private NullExpression() { }

        #endregion

        #region IExpression Members

        [ComVisible(false)]
        public string Sql
        {
            get { return "NULL"; }
        }

        [ComVisible(false)]
        public ICollection<IExpression> ChildExpressions
        {
            get { return null; }
        }

        [ComVisible(false)]
        public string ToCacheableSql()
        {
            return Sql;
        }

        [ComVisible(false)]
        public Int32Expression Count(bool distinct)
        {
            //should never be called
            throw new NotSupportedException();
        }

        [ComVisible(false)]
        public Condition In(IEnumerable values)
        {
            //should never be called
            throw new NotSupportedException();
        }

        [ComVisible(false)]
        public Condition NotIn(IEnumerable values)
        {
            //should never be called
            throw new NotSupportedException();
        }

        #endregion

        #region ICloneable Members

        [ComVisible(false)]
        public object Clone()
        {
            return this;
        }

        #endregion
    }
}
