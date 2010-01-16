using System;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace NIntegrate.Data
{
    [DataContract]
    public sealed class BooleanColumn : BooleanExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public BooleanColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal BooleanColumn(BooleanExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            var clone = new BooleanColumn((BooleanExpression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(BooleanExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(bool value)
        {
            return new Assignment(this, new BooleanParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class ByteColumn : ByteExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public ByteColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal ByteColumn(ByteExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new ByteColumn((ByteExpression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(ByteExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(byte value)
        {
            return new Assignment(this, new ByteParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class Int16Column : Int16Expression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public Int16Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal Int16Column(Int16Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new Int16Column((Int16Expression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(Int16Expression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(short value)
        {
            return new Assignment(this, new Int16ParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class Int32Column : Int32Expression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public Int32Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal Int32Column(Int32Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new Int32Column((Int32Expression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(Int32Expression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(int value)
        {
            return new Assignment(this, new Int32ParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class Int64Column : Int64Expression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public Int64Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal Int64Column(Int64Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new Int64Column((Int64Expression)base.Clone(), ColumnName)
                            {
                                _columnName = _columnName,
                            };
            return clone;
        }

        public Assignment Set(Int64Expression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(long value)
        {
            return new Assignment(this, new Int64ParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class DateTimeColumn : DateTimeExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public DateTimeColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal DateTimeColumn(DateTimeExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new DateTimeColumn((DateTimeExpression)base.Clone(), ColumnName)
                            {
                                _columnName = _columnName,
                            };
            return clone;
        }

        public Assignment Set(DateTimeExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(DateTime value)
        {
            return new Assignment(this, new DateTimeParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class StringColumn : StringExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public StringColumn(string columnName, bool isUnicode)
            : base(isUnicode)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal StringColumn(StringExpression expr, string columnName, bool isUnicode)
            : this(columnName, isUnicode)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new StringColumn((StringExpression)base.Clone(), ColumnName, IsUnicode);
            return clone;
        }

        public Assignment Set(StringExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(string value)
        {
            if (value == null)
                return new Assignment(this, NullExpression.Value);

            return new Assignment(this, new StringParameterExpression(value, IsUnicode));
        }

        #endregion
    }

    [DataContract]
    public sealed class GuidColumn : GuidExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public GuidColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal GuidColumn(GuidExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new GuidColumn((GuidExpression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(GuidExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(Guid value)
        {
            return new Assignment(this, new GuidParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class DoubleColumn : DoubleExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public DoubleColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal DoubleColumn(DoubleExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new DoubleColumn((DoubleExpression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(DoubleExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(double value)
        {
            return new Assignment(this, new DoubleParameterExpression(value));
        }

        #endregion
    }

    [DataContract]
    public sealed class DecimalColumn : DecimalExpression, IColumn
    {
        [DataMember]
        internal string _columnName;

        #region Constructors

        public DecimalColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            Sql = columnName.ToDatabaseObjectName();
        }

        internal DecimalColumn(DecimalExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            Sql = expr.Sql;
            if (expr.ChildExpressions != null)
                ChildExpressions.AddRange(expr.ChildExpressions);
        }

        #endregion

        #region IColumn Members

        public string ColumnName
        {
            get { return _columnName; }
        }

        #endregion

        #region Public Methods

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new DecimalColumn((DecimalExpression)base.Clone(), ColumnName);
            return clone;
        }

        public Assignment Set(DecimalExpression value)
        {
            return new Assignment(this, ((object)value) == null ? (IExpression)NullExpression.Value : value);
        }

        public Assignment Set(decimal value)
        {
            return new Assignment(this, new DecimalParameterExpression(value));
        }

        #endregion
    }
}
