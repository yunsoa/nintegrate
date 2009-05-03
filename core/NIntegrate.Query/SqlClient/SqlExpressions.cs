using System;
using System.Runtime.Serialization;
namespace NIntegrate.Query.SqlClient
{
    #region Int32

    [DataContract]
    public sealed class SqlInt32Expression : Int32Expression
    {
        public StringExpression ToChar()
        {
            return ExtensionMethods.ToChar(this);
        }

        public StringExpression ToNChar()
        {
            return ExtensionMethods.ToNChar(this);
        }
    }

    [DataContract]
    public sealed class SqlInt32ParameterExpression : Int32ParameterExpression
    {
        public SqlInt32ParameterExpression(int value)
            : base(value)
        {
        }

        public SqlInt32ParameterExpression(string id, int value)
            : base(id, value)
        {
        }

        public StringExpression ToChar()
        {
            return ExtensionMethods.ToChar(this);
        }

        public StringExpression ToNChar()
        {
            return ExtensionMethods.ToNChar(this);
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
            return ExtensionMethods.ToChar(this);
        }

        public StringExpression ToNChar()
        {
            return ExtensionMethods.ToNChar(this);
        }
    }

    #endregion

    #region DateTime

    [DataContract]
    public sealed class SqlDateTimeExpression : DateTimeExpression
    {
        public DateTimeExpression AddDay(int n)
        {
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddDay(Int32Expression n)
        {
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddMonth(int n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddMonth(Int32Expression n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddYear(int n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddYear(Int32Expression n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddHour(int n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddHour(Int32Expression n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddMinute(int n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddMinute(Int32Expression n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddSecond(int n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public DateTimeExpression AddSecond(Int32Expression n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public Int32Expression GetDay()
        {
            return ExtensionMethods.GetDay(this);
        }

        public Int32Expression GetMonth()
        {
            return ExtensionMethods.GetMonth(this);
        }

        public Int32Expression GetYear()
        {
            return ExtensionMethods.GetYear(this);
        }


        public Int32Expression GetHour()
        {
            return ExtensionMethods.GetHour(this);
        }


        public Int32Expression GetMinute()
        {
            return ExtensionMethods.GetMinute(this);
        }


        public Int32Expression GetSecond()
        {
            return ExtensionMethods.GetSecond(this);
        }
    }

    [DataContract]
    public sealed class SqlDateTimeParameterExpression : DateTimeParameterExpression
    {
        public SqlDateTimeParameterExpression(DateTime value)
            : base(value)
        {
        }

        public SqlDateTimeParameterExpression(string id, DateTime value)
            : base(id, value)
        {
        }

        public DateTimeExpression AddDay(int n)
        {
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddDay(Int32Expression n)
        {
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddMonth(int n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddMonth(Int32Expression n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddYear(int n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddYear(Int32Expression n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddHour(int n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddHour(Int32Expression n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddMinute(int n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddMinute(Int32Expression n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddSecond(int n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public DateTimeExpression AddSecond(Int32Expression n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public Int32Expression GetDay()
        {
            return ExtensionMethods.GetDay(this);
        }

        public Int32Expression GetMonth()
        {
            return ExtensionMethods.GetMonth(this);
        }

        public Int32Expression GetYear()
        {
            return ExtensionMethods.GetYear(this);
        }


        public Int32Expression GetHour()
        {
            return ExtensionMethods.GetHour(this);
        }


        public Int32Expression GetMinute()
        {
            return ExtensionMethods.GetMinute(this);
        }


        public Int32Expression GetSecond()
        {
            return ExtensionMethods.GetSecond(this);
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
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddDay(Int32Expression n)
        {
            return ExtensionMethods.AddDay(this, n);
        }

        public DateTimeExpression AddMonth(int n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddMonth(Int32Expression n)
        {
            return ExtensionMethods.AddMonth(this, n);
        }

        public DateTimeExpression AddYear(int n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddYear(Int32Expression n)
        {
            return ExtensionMethods.AddYear(this, n);
        }

        public DateTimeExpression AddHour(int n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddHour(Int32Expression n)
        {
            return ExtensionMethods.AddHour(this, n);
        }

        public DateTimeExpression AddMinute(int n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddMinute(Int32Expression n)
        {
            return ExtensionMethods.AddMinute(this, n);
        }

        public DateTimeExpression AddSecond(int n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public DateTimeExpression AddSecond(Int32Expression n)
        {
            return ExtensionMethods.AddSecond(this, n);
        }

        public Int32Expression GetDay()
        {
            return ExtensionMethods.GetDay(this);
        }

        public Int32Expression GetMonth()
        {
            return ExtensionMethods.GetMonth(this);
        }

        public Int32Expression GetYear()
        {
            return ExtensionMethods.GetYear(this);
        }


        public Int32Expression GetHour()
        {
            return ExtensionMethods.GetHour(this);
        }


        public Int32Expression GetMinute()
        {
            return ExtensionMethods.GetMinute(this);
        }


        public Int32Expression GetSecond()
        {
            return ExtensionMethods.GetSecond(this);
        }
    }

    #endregion

    #region String

    [DataContract]
    public sealed class SqlStringExpression : StringExpression
    {
        public Condition Contains(string value)
        {
            return ExtensionMethods.Contains(this, value);
        }

        public Condition Contains(StringExpression value)
        {
            return ExtensionMethods.Contains(this, value);
        }

        public Condition EndsWith(string value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition EndsWith(StringExpression value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition StartsWith(string value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Condition StartsWith(StringExpression value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Int32Expression IndexOf(string value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public Int32Expression IndexOf(StringExpression value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public StringExpression Replace(string find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(string find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Substring(int begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(int begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Left(int length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Left(Int32Expression length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Right(int length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression Right(Int32Expression length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression LTrim()
        {
            return ExtensionMethods.LTrim(this);
        }

        public StringExpression RTrim()
        {
            return ExtensionMethods.RTrim(this);
        }

        public Int32Expression ToAscii()
        {
            return ExtensionMethods.ToAscii(this);
        }

        public Int32Expression ToUnicode()
        {
            return ExtensionMethods.ToUnicode(this);
        }

        public Int32Expression GetLength()
        {
            return ExtensionMethods.GetLength(this);
        }
    }

    [DataContract]
    public sealed class SqlStringParameterExpression : StringParameterExpression
    {
        public SqlStringParameterExpression(string value, bool isUnicode)
            : base(value, isUnicode)
        {
        }

        public SqlStringParameterExpression(string id, string value, bool isUnicode)
            : base(id, value, isUnicode)
        {
        }

        public Condition Contains(string value)
        {
            return ExtensionMethods.Contains(this, value);
        }

        public Condition Contains(StringExpression value)
        {
            return ExtensionMethods.Contains(this, value);
        }

        public Condition EndsWith(string value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition EndsWith(StringExpression value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition StartsWith(string value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Condition StartsWith(StringExpression value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Int32Expression IndexOf(string value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public Int32Expression IndexOf(StringExpression value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public StringExpression Replace(string find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(string find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Substring(int begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(int begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Left(int length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Left(Int32Expression length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Right(int length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression Right(Int32Expression length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression LTrim()
        {
            return ExtensionMethods.LTrim(this);
        }

        public StringExpression RTrim()
        {
            return ExtensionMethods.RTrim(this);
        }

        public Int32Expression ToAscii()
        {
            return ExtensionMethods.ToAscii(this);
        }

        public Int32Expression ToUnicode()
        {
            return ExtensionMethods.ToUnicode(this);
        }

        public Int32Expression GetLength()
        {
            return ExtensionMethods.GetLength(this);
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
            return ExtensionMethods.Contains(this, value);
        }

        public Condition Contains(StringExpression value)
        {
            return ExtensionMethods.Contains(this, value);
        }

        public Condition EndsWith(string value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition EndsWith(StringExpression value)
        {
            return ExtensionMethods.EndsWith(this, value);
        }

        public Condition StartsWith(string value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Condition StartsWith(StringExpression value)
        {
            return ExtensionMethods.StartsWith(this, value);
        }

        public Int32Expression IndexOf(string value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public Int32Expression IndexOf(StringExpression value)
        {
            return ExtensionMethods.IndexOf(this, value);
        }

        public StringExpression Replace(string find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, string replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(string find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Replace(StringExpression find, StringExpression replace)
        {
            return ExtensionMethods.Replace(this, find, replace);
        }

        public StringExpression Substring(int begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, int length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(int begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Substring(Int32Expression begin, Int32Expression length)
        {
            return ExtensionMethods.Substring(this, begin, length);
        }

        public StringExpression Left(int length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Left(Int32Expression length)
        {
            return ExtensionMethods.Left(this, length);
        }

        public StringExpression Right(int length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression Right(Int32Expression length)
        {
            return ExtensionMethods.Right(this, length);
        }

        public StringExpression LTrim()
        {
            return ExtensionMethods.LTrim(this);
        }

        public StringExpression RTrim()
        {
            return ExtensionMethods.RTrim(this);
        }

        public Int32Expression ToAscii()
        {
            return ExtensionMethods.ToAscii(this);
        }

        public Int32Expression ToUnicode()
        {
            return ExtensionMethods.ToUnicode(this);
        }

        public Int32Expression GetLength()
        {
            return ExtensionMethods.GetLength(this);
        }
    }

    #endregion
}
