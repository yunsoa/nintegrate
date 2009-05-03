using System;
using System.Runtime.Serialization;

namespace NIntegrate.Query
{
    [DataContract]
    public sealed class BooleanParameterExpression : BooleanExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private bool _value;

        #endregion

        #region Constructors

        public BooleanParameterExpression(string id, bool value) : this(value)
        {
            _id = id;
        }

        public BooleanParameterExpression(bool value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public bool Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new BooleanParameterExpression(_id, _value) {_sql = _sql};
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (bool)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class ByteParameterExpression : ByteExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private byte _value;

        #endregion

        #region Constructors

        public ByteParameterExpression(string id, byte value) : this(value)
        {
            _id = id;
        }

        public ByteParameterExpression(byte value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public byte Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new ByteParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (byte)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class Int16ParameterExpression : Int16Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private short _value;

        #endregion

        #region Constructors

        public Int16ParameterExpression(string id, short value) : this(value)
        {
            _id = id;
        }

        public Int16ParameterExpression(short value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public short Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new Int16ParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (short)value; }
        }

        #endregion
    }

    [DataContract]
    public class Int32ParameterExpression : Int32Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private int _value;

        #endregion

        #region Constructors

        public Int32ParameterExpression(string id, int value) : this(value)
        {
            _id = id;
        }

        public Int32ParameterExpression(int value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public int Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new Int32ParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (int)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class Int64ParameterExpression : Int64Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private long _value;

        #endregion

        #region Constructors

        public Int64ParameterExpression(string id, long value) : this(value)
        {
            _id = id;
        }

        public Int64ParameterExpression(long value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public long Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new Int64ParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (long)value; }
        }

        #endregion
    }

    [DataContract]
    public class DateTimeParameterExpression : DateTimeExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private DateTime _value;

        #endregion

        #region Constructors

        public DateTimeParameterExpression(string id, DateTime value) : this(value)
        {
            _id = id;
        }

        public DateTimeParameterExpression(DateTime value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public DateTime Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new DateTimeParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (DateTime)value; }
        }

        #endregion
    }

    [DataContract]
    public class StringParameterExpression : StringExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        internal string _value;

        #endregion

        #region Constructors

        public StringParameterExpression(string id, string value, bool isUnicode) : this(value, isUnicode)
        {
            _id = id;
        }

        public StringParameterExpression(string value, bool isUnicode) : base(isUnicode)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public string Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new StringParameterExpression(_id, _value, _isUnicode) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (string)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class GuidParameterExpression : GuidExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private Guid _value;

        #endregion

        #region Constructors

        public GuidParameterExpression(string id, Guid value) : this(value)
        {
            _id = id;
        }

        public GuidParameterExpression(Guid value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public Guid Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new GuidParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (Guid)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class DoubleParameterExpression : DoubleExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private double _value;

        #endregion

        #region Constructors

        public DoubleParameterExpression(string id, double value) : this(value)
        {
            _id = id;
        }

        public DoubleParameterExpression(double value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public double Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new DoubleParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (double)value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class DecimalParameterExpression : DecimalExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly string _id;
        [DataMember]
        private decimal _value;

        #endregion

        #region Constructors

        public DecimalParameterExpression(string id, decimal value) : this(value)
        {
            _id = id;
        }

        public DecimalParameterExpression(decimal value)
        {
            _value = value;
            _sql = "?";
        }

        #endregion

        #region Public Methods

        public decimal Value
        {
            get { return _value; }
        }

        public override object Clone()
        {
            return new DecimalParameterExpression(_id, _value) { _sql = _sql };
        }

        #endregion

        #region IParameterExpression Members

        public string ID
        {
            get { return _id; }
        }

        object IParameterExpression.Value
        {
            get { return _value; }
            set { _value = (decimal)value; }
        }

        #endregion
    }
}
