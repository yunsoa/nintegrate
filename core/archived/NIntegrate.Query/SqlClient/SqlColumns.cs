﻿using System.Runtime.InteropServices;
using System.Runtime.Serialization;
namespace NIntegrate.Query.SqlClient
{
    [DataContract]
    public sealed class SqlBooleanColumn : BooleanColumn
    {
        public SqlBooleanColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlBooleanColumn(BooleanExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlBooleanColumn((BooleanExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlByteColumn : ByteColumn
    {
        public SqlByteColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlByteColumn(ByteExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlByteColumn((ByteExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlInt16Column : Int16Column
    {
        public SqlInt16Column(string columnName)
            : base(columnName)
        {
        }

        internal SqlInt16Column(Int16Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlInt16Column((Int16Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlInt32Column : Int32Column
    {
        public SqlInt32Column(string columnName)
            : base(columnName)
        {
        }

        internal SqlInt32Column(Int32Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        public StringExpression ToChar()
        {
            return SqlExtensionMethods.ToChar(this);
        }

        public StringExpression ToNChar()
        {
            return SqlExtensionMethods.ToNChar(this);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlInt32Column((Int32Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlInt64Column : Int64Column
    {
        public SqlInt64Column(string columnName)
            : base(columnName)
        {
        }

        internal SqlInt64Column(Int64Expression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlInt64Column((Int64Expression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlDateTimeColumn : DateTimeColumn
    {
        public SqlDateTimeColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlDateTimeColumn(DateTimeExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        public DateTimeExpression AddDay(int n)
        {
            return SqlExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddDay(Int32Expression n)
        {
            return SqlExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddMonth(int n)
        {
            return SqlExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddMonth(Int32Expression n)
        {
            return SqlExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddYear(int n)
        {
            return SqlExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddYear(Int32Expression n)
        {
            return SqlExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddHour(int n)
        {
            return SqlExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddHour(Int32Expression n)
        {
            return SqlExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddMinute(int n)
        {
            return SqlExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddMinute(Int32Expression n)
        {
            return SqlExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddSecond(int n)
        {
            return SqlExtensionMethods.AddSecond(this, n);
        }

        public DateTimeExpression AddSecond(Int32Expression n)
        {
            return SqlExtensionMethods.AddSecond(this, n);
        }

        public Int32Expression GetDay()
        {
            return SqlExtensionMethods.GetDay(this);
        }

        public Int32Expression GetMonth()
        {
            return SqlExtensionMethods.GetMonth(this);
        }

        public Int32Expression GetYear()
        {
            return SqlExtensionMethods.GetYear(this);
        }


        public Int32Expression GetHour()
        {
            return SqlExtensionMethods.GetHour(this);
        }


        public Int32Expression GetMinute()
        {
            return SqlExtensionMethods.GetMinute(this);
        }


        public Int32Expression GetSecond()
        {
            return SqlExtensionMethods.GetSecond(this);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlDateTimeColumn((DateTimeExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlStringColumn : StringColumn
    {
        public SqlStringColumn(string columnName, bool isUnicode)
            : base(columnName, isUnicode)
        {
        }

        internal SqlStringColumn(StringExpression expr, string columnName, bool isUnicode)
            : base(expr, columnName, isUnicode)
        {
        }

        public Condition Contains(string value)
        {
            return SqlExtensionMethods.Contains(this, value);
        }

        public Condition Contains(StringExpression value)
        {
            return SqlExtensionMethods.Contains(this, value);
        }

        public Condition EndsWith(string value)
        {
            return SqlExtensionMethods.EndsWith(this, value);
        }

        public Condition EndsWith(StringExpression value)
        {
            return SqlExtensionMethods.EndsWith(this, value);
        }

        public Condition StartsWith(string value)
        {
            return SqlExtensionMethods.StartsWith(this, value);
        }

        public Condition StartsWith(StringExpression value)
        {
            return SqlExtensionMethods.StartsWith(this, value);
        }

        public Int32Expression IndexOf(string value)
        {
            return SqlExtensionMethods.IndexOf(this, value);
        }

        public Int32Expression IndexOf(StringExpression value)
        {
            return SqlExtensionMethods.IndexOf(this, value);
        }

        public StringExpression Replace(string find, string replace)
        {
            return SqlExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, string replace)
        {
            return SqlExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(string find, StringExpression replace)
        {
            return SqlExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, StringExpression replace)
        {
            return SqlExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Substring(int begin, int length)
        {
            return SqlExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, int length)
        {
            return SqlExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(int begin, Int32Expression length)
        {
            return SqlExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, Int32Expression length)
        {
            return SqlExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Left(int length)
        {
            return SqlExtensionMethods.Left(this, length);
        }

        public StringExpression Left(Int32Expression length)
        {
            return SqlExtensionMethods.Left(this, length);
        }

        public StringExpression Right(int length)
        {
            return SqlExtensionMethods.Right(this, length);
        }

        public StringExpression Right(Int32Expression length)
        {
            return SqlExtensionMethods.Right(this, length);
        }

        public StringExpression LTrim()
        {
            return SqlExtensionMethods.LTrim(this);
        }

        public StringExpression RTrim()
        {
            return SqlExtensionMethods.RTrim(this);
        }

        public Int32Expression ToAscii()
        {
            return SqlExtensionMethods.ToAscii(this);
        }

        public Int32Expression ToUnicode()
        {
            return SqlExtensionMethods.ToUnicode(this);
        }

        public Int32Expression GetLength()
        {
            return SqlExtensionMethods.GetLength(this);
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlStringColumn((StringExpression)base.Clone(), ColumnName, IsUnicode);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlGuidColumn : GuidColumn
    {
        public SqlGuidColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlGuidColumn(GuidExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlGuidColumn((GuidExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlDoubleColumn : DoubleColumn
    {
        public SqlDoubleColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlDoubleColumn(DoubleExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlDoubleColumn((DoubleExpression)base.Clone(), ColumnName);
            return clone;
        }
    }

    [DataContract]
    public sealed class SqlDecimalColumn : DecimalColumn
    {
        public SqlDecimalColumn(string columnName)
            : base(columnName)
        {
        }

        internal SqlDecimalColumn(DecimalExpression expr, string columnName)
            : base(expr, columnName)
        {
        }

        [ComVisible(false)]
        public override object Clone()
        {
            var clone = new SqlDecimalColumn((DecimalExpression)base.Clone(), ColumnName);
            return clone;
        }
    }
}
