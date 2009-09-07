using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    [DataContract]
    public sealed class BooleanParameterExpression : BooleanExpression, IParameterExpression
    {
        #region Constructors

        public BooleanParameterExpression(string id, bool? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public BooleanParameterExpression(bool? value)
        {
            Value = value;
            Sql = "?";
        }

        public BooleanParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public bool? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (bool?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new BooleanParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new BooleanParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(bool value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new BooleanParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(bool? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(bool value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new BooleanParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(bool? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(BooleanParameterExpression left, bool? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(BooleanParameterExpression left, bool? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(bool? left, BooleanParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(bool? left, BooleanParameterExpression right)
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
    }

    [DataContract]
    public sealed class ByteParameterExpression : ByteExpression, IParameterExpression
    {
        #region Constructors

        public ByteParameterExpression(string id, byte? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public ByteParameterExpression(byte? value)
        {
            Value = value;
            Sql = "?";
        }

        public ByteParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public byte? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (byte?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new ByteParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new ByteParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(byte value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new ByteParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(byte? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(byte value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new ByteParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(byte? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(ByteParameterExpression left, byte? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(ByteParameterExpression left, byte? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(byte? left, ByteParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(byte? left, ByteParameterExpression right)
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
    }

    [DataContract]
    public sealed class Int16ParameterExpression : Int16Expression, IParameterExpression
    {
        #region Constructors

        public Int16ParameterExpression(string id, short? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public Int16ParameterExpression(short? value)
        {
            Value = value;
            Sql = "?";
        }

        public Int16ParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public short? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (short?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new Int16ParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new Int16ParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(short value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new Int16ParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(short? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(short value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new Int16ParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(short? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(Int16ParameterExpression left, short? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(Int16ParameterExpression left, short? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(short? left, Int16ParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(short? left, Int16ParameterExpression right)
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
    }

    [DataContract]
    public sealed class Int32ParameterExpression : Int32Expression, IParameterExpression
    {
        #region Constructors

        public Int32ParameterExpression(string id, int? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public Int32ParameterExpression(int? value)
        {
            Value = value;
            Sql = "?";
        }

        public Int32ParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public int? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (int?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new Int32ParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new Int32ParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(int value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new Int32ParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(int? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(int value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new Int32ParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(int? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(Int32ParameterExpression left, int? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(Int32ParameterExpression left, int? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(int? left, Int32ParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(int? left, Int32ParameterExpression right)
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
    }

    [DataContract]
    public sealed class Int64ParameterExpression : Int64Expression, IParameterExpression
    {
        #region Constructors

        public Int64ParameterExpression(string id, long? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public Int64ParameterExpression(long? value)
        {
            Value = value;
            Sql = "?";
        }

        public Int64ParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public long? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (long?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new Int64ParameterExpression(Value) {Sql = Sql}
                       :
                           new Int64ParameterExpression(ID, Value) {Sql = Sql};
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(long value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new Int64ParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(long? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(long value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new Int64ParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(long? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(Int64ParameterExpression left, long? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(Int64ParameterExpression left, long? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(long? left, Int64ParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(long? left, Int64ParameterExpression right)
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
    }

    [DataContract]
    public sealed class DateTimeParameterExpression : DateTimeExpression, IParameterExpression
    {
        #region Constructors

        public DateTimeParameterExpression(string id, DateTime? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public DateTimeParameterExpression(DateTime? value)
        {
            Value = value;
            Sql = "?";
        }

        public DateTimeParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public DateTime? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (DateTime?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new DateTimeParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new DateTimeParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(DateTime value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new DateTimeParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(DateTime? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(DateTime value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new DateTimeParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(DateTime? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(DateTimeParameterExpression left, DateTime? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(DateTimeParameterExpression left, DateTime? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(DateTime? left, DateTimeParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(DateTime? left, DateTimeParameterExpression right)
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
    }

    [DataContract]
    public sealed class StringParameterExpression : StringExpression, IParameterExpression
    {
        #region Constructors

        public StringParameterExpression(string id, string value, bool isUnicode) : base(isUnicode)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public StringParameterExpression(string value, bool isUnicode) : base(isUnicode)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Value = value;
            Sql = "?";
        }

        public StringParameterExpression(string sprocParamName, SprocParameterDirection direction, bool IsUnicode)
            : base(IsUnicode)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public string Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (string)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new StringParameterExpression(Value, IsUnicode) { Sql = Sql, Direction = Direction }
                       :
                           new StringParameterExpression(ID, Value, IsUnicode) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(string value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new StringParameterExpression(value, IsUnicode));
        }

        public new ParameterEqualsCondition NotEquals(string value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new StringParameterExpression(value, IsUnicode));
        }

        public static ParameterEqualsCondition operator ==(StringParameterExpression left, string right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(StringParameterExpression left, string right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(string left, StringParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(string left, StringParameterExpression right)
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
    }

    [DataContract]
    public sealed class GuidParameterExpression : GuidExpression, IParameterExpression
    {
        #region Constructors

        public GuidParameterExpression(string id, Guid? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public GuidParameterExpression(Guid? value)
        {
            Value = value;
            Sql = "?";
        }

        public GuidParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public Guid? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (Guid?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new GuidParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new GuidParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(Guid value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new GuidParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(Guid? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(Guid value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new GuidParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(Guid? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(GuidParameterExpression left, Guid? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(GuidParameterExpression left, Guid? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(Guid? left, GuidParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(Guid? left, GuidParameterExpression right)
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
    }

    [DataContract]
    public sealed class DoubleParameterExpression : DoubleExpression, IParameterExpression
    {
        #region Constructors

        public DoubleParameterExpression(string id, double? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public DoubleParameterExpression(double? value)
        {
            Value = value;
            Sql = "?";
        }

        public DoubleParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public double? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (double?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new DoubleParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new DoubleParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(double value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new DoubleParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(double? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(double value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new DoubleParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(double? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(DoubleParameterExpression left, double? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(DoubleParameterExpression left, double? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(double? left, DoubleParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(double? left, DoubleParameterExpression right)
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
    }

    [DataContract]
    public sealed class DecimalParameterExpression : DecimalExpression, IParameterExpression
    {
        #region Constructors

        public DecimalParameterExpression(string id, decimal? value)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            ID = id;
            Value = value;
            Sql = "?";
        }

        public DecimalParameterExpression(decimal? value)
        {
            Value = value;
            Sql = "?";
        }

        public DecimalParameterExpression(string sprocParamName, SprocParameterDirection direction)
        {
            if (string.IsNullOrEmpty(sprocParamName))
                throw new ArgumentNullException("sprocParamName");

            ID = sprocParamName;
            Sql = "?";
            Direction = direction;
        }

        #endregion

        #region Public Properties

        [DataMember]
        public string ID { get; internal set; }

        [DataMember]
        public decimal? Value { get; set; }

        object IParameterExpression.Value
        {
            get { return Value; }
            set { Value = (decimal?)value; }
        }

        [DataMember]
        public SprocParameterDirection? Direction { get; internal set; }

        #endregion

        #region Public Methods

        public override object Clone()
        {
            return string.IsNullOrEmpty(ID)
                       ?
                           new DecimalParameterExpression(Value) { Sql = Sql, Direction = Direction }
                       :
                           new DecimalParameterExpression(ID, Value) { Sql = Sql, Direction = Direction };
        }

        #endregion

        #region Equals & NotEquals

        public new ParameterEqualsCondition Equals(decimal value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.Equals, new DecimalParameterExpression(value));
        }

        public ParameterEqualsCondition Equals(decimal? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.Is, NullExpression.Value);

            return Equals(value.Value);
        }

        public new ParameterEqualsCondition NotEquals(decimal value)
        {
            return new ParameterEqualsCondition(this, ExpressionOperator.NotEquals, new DecimalParameterExpression(value));
        }

        public ParameterEqualsCondition NotEquals(decimal? value)
        {
            if (value == null)
                return new ParameterEqualsCondition(this, ExpressionOperator.IsNot, NullExpression.Value);

            return NotEquals(value.Value);
        }

        public static ParameterEqualsCondition operator ==(DecimalParameterExpression left, decimal? right)
        {
            return left.Equals(right);
        }

        public static ParameterEqualsCondition operator !=(DecimalParameterExpression left, decimal? right)
        {
            return left.NotEquals(right);
        }

        public static ParameterEqualsCondition operator ==(decimal? left, DecimalParameterExpression right)
        {
            return right.Equals(left);
        }

        public static ParameterEqualsCondition operator !=(decimal? left, DecimalParameterExpression right)
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
    }
}
