using System;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace NIntegrate.Query
{
    [DataContract]
    public class BooleanColumn : BooleanExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal BooleanColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal BooleanColumn(BooleanExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class ByteColumn : ByteExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal ByteColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal ByteColumn(ByteExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class Int16Column : Int16Expression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal Int16Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal Int16Column(Int16Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class Int32Column : Int32Expression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal Int32Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal Int32Column(Int32Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class Int64Column : Int64Expression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal Int64Column(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal Int64Column(Int64Expression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class DateTimeColumn : DateTimeExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal DateTimeColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal DateTimeColumn(DateTimeExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class StringColumn : StringExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal StringColumn(string columnName, bool isUnicode)
            : base(isUnicode)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal StringColumn(StringExpression expr, string columnName, bool isUnicode)
            : this(columnName, isUnicode)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class GuidColumn : GuidExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal GuidColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal GuidColumn(GuidExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class DoubleColumn : DoubleExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal DoubleColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal DoubleColumn(DoubleExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }

    [DataContract]
    public class DecimalColumn : DecimalExpression, IColumn
    {
        #region Private Fields

        [DataMember]
        internal string _columnName;

        #endregion

        #region Constructors

        internal DecimalColumn(string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("columnName");

            _columnName = columnName;
            _sql = columnName.ToDatabaseObjectName();
        }

        internal DecimalColumn(DecimalExpression expr, string columnName)
            : this(columnName)
        {
            if (ReferenceEquals(expr, null))
                throw new ArgumentNullException("expr");

            _sql = expr.Sql;
            if (expr.ChildExpressions != null)
                _childExpressions.AddRange(expr.ChildExpressions);
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

        #endregion
    }
}
