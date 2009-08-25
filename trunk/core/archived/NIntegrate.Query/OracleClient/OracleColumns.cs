using System.Runtime.InteropServices;
using System.Runtime.Serialization;
namespace NIntegrate.Query.OracleClient
{
    [DataContract]
    public sealed class OracleBooleanColumn : BooleanColumn
    {
        public OracleBooleanColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleBooleanColumn(BooleanExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleBooleanColumn((BooleanExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleByteColumn : ByteColumn
    {
        public OracleByteColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleByteColumn(ByteExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleByteColumn((ByteExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleInt16Column : Int16Column
    {
        public OracleInt16Column(string columnName)
            : base(columnName)
        {
        }

        internal OracleInt16Column(Int16Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleInt16Column((Int16Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleInt32Column : Int32Column
    {
        public OracleInt32Column(string columnName)
            : base(columnName)
        {
        }

        internal OracleInt32Column(Int32Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        public StringExpression ToChar()
        {
            return OracleExtensionMethods.ToChar(this);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleInt32Column((Int32Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleInt64Column : Int64Column
    {
        public OracleInt64Column(string columnName)
            : base(columnName)
        {
        }

        internal OracleInt64Column(Int64Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleInt64Column((Int64Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleDateTimeColumn : DateTimeColumn
    {
        public OracleDateTimeColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleDateTimeColumn(DateTimeExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        public DateTimeExpression AddMonth(int n)
        {
            return OracleExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddMonth(Int32Expression n)
        {
            return OracleExtensionMethods.AddMonth(this, n);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleDateTimeColumn((DateTimeExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleStringColumn : StringColumn
    {
        public OracleStringColumn(string columnName, bool isUnicode)
            : base(columnName, isUnicode)
        {
        }

        internal OracleStringColumn(StringExpression expr, string columnName, bool isUnicode)
            : base(expr, columnName, isUnicode)
        {
        }

        public Condition Contains(string value)
        {
            return OracleExtensionMethods.Contains(this, value);
        }

        public Condition Contains(StringExpression value)
        {
            return OracleExtensionMethods.Contains(this, value);
        }

        public Condition EndsWith(string value)
        {
            return OracleExtensionMethods.EndsWith(this, value);
        }

        public Condition EndsWith(StringExpression value)
        {
            return OracleExtensionMethods.EndsWith(this, value);
        }

        public Condition StartsWith(string value)
        {
            return OracleExtensionMethods.StartsWith(this, value);
        }

        public Condition StartsWith(StringExpression value)
        {
            return OracleExtensionMethods.StartsWith(this, value);
        }

        public Int32Expression IndexOf(string value)
        {
            return OracleExtensionMethods.IndexOf(this, value);
        }

        public Int32Expression IndexOf(StringExpression value)
        {
            return OracleExtensionMethods.IndexOf(this, value);
        }

        public StringExpression Replace(string find, string replace)
        {
            return OracleExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, string replace)
        {
            return OracleExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(string find, StringExpression replace)
        {
            return OracleExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, StringExpression replace)
        {
            return OracleExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Substring(int begin, int length)
        {
            return OracleExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, int length)
        {
            return OracleExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(int begin, Int32Expression length)
        {
            return OracleExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, Int32Expression length)
        {
            return OracleExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression LTrim()
        {
            return OracleExtensionMethods.LTrim(this);
        }

        public StringExpression RTrim()
        {
            return OracleExtensionMethods.RTrim(this);
        }

        public Int32Expression ToAscii()
        {
            return OracleExtensionMethods.ToAscii(this);
        }

        public Int32Expression GetLength()
        {
            return OracleExtensionMethods.GetLength(this);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleStringColumn((StringExpression)base.Clone(), ColumnName, IsUnicode);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleGuidColumn : GuidColumn
    {
        public OracleGuidColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleGuidColumn(GuidExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleGuidColumn((GuidExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleDoubleColumn : DoubleColumn
    {
        public OracleDoubleColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleDoubleColumn(DoubleExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleDoubleColumn((DoubleExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class OracleDecimalColumn : DecimalColumn
    {
        public OracleDecimalColumn(string columnName)
            : base(columnName)
        {
        }

        internal OracleDecimalColumn(DecimalExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new OracleDecimalColumn((DecimalExpression)base.Clone(), ColumnName);
            return clone;
        }
    }
}
