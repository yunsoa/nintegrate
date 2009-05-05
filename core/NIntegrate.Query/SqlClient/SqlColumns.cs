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
    }

    [DataContract]
    public sealed class SqlByteColumn : ByteColumn
    {
        public SqlByteColumn(string columnName)
            : base(columnName)
        {
        }
    }

    [DataContract]
    public sealed class SqlInt16Column : Int16Column
    {
        public SqlInt16Column(string columnName)
            : base(columnName)
        {
        }
    }

    [DataContract]
    public sealed class SqlInt32Column : Int32Column
    {
        public SqlInt32Column(string columnName)
            : base(columnName)
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
    }

    [DataContract]
    public sealed class SqlInt64Column : Int64Column
    {
        public SqlInt64Column(string columnName)
            : base(columnName)
        {
        }
    }

    [DataContract]
    public sealed class SqlDateTimeColumn : DateTimeColumn
    {
        public SqlDateTimeColumn(string columnName)
            : base(columnName)
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
    }

    [DataContract]
    public sealed class SqlStringColumn : StringColumn
    {
        public SqlStringColumn(string columnName, bool isUnicode)
            : base(columnName, isUnicode)
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
    }

    [DataContract]
    public sealed class SqlGuidColumn : GuidColumn
    {
        public SqlGuidColumn(string columnName)
            : base(columnName)
        {
        }
    }

    [DataContract]
    public sealed class SqlDoubleColumn : DoubleColumn
    {
        public SqlDoubleColumn(string columnName)
            : base(columnName)
        {
        }
    }

    [DataContract]
    public sealed class SqlDecimalColumn : DecimalColumn
    {
        public SqlDecimalColumn(string columnName)
            : base(columnName)
        {
        }
    }
}
