using System;
using System.Runtime.Serialization;

namespace NIntegrate.Query
{
    [DataContract]
    public sealed class BooleanParameterExpression : BooleanExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private bool _value;

        #endregion

        #region Constructors

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
            return new BooleanParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class ByteParameterExpression : ByteExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private byte _value;

        #endregion

        #region Constructors

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
            return new ByteParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class Int16ParameterExpression : Int16Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private short _value;

        #endregion

        #region Constructors

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
            return new Int16ParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class Int32ParameterExpression : Int32Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private int _value;

        #endregion

        #region Constructors

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
            return new Int32ParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class Int64ParameterExpression : Int64Expression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private long _value;

        #endregion

        #region Constructors

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
            return new Int64ParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class DateTimeParameterExpression : DateTimeExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private readonly DateTime _value;

        #endregion

        #region Constructors

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
            return new DateTimeParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class StringParameterExpression : StringExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        internal string _value;

        #endregion

        #region Constructors

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
            return new StringParameterExpression(_value, _isUnicode);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class GuidParameterExpression : GuidExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private Guid _value;

        #endregion

        #region Constructors

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
            return new GuidParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class DoubleParameterExpression : DoubleExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private double _value;

        #endregion

        #region Constructors

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
            return new DoubleParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }

    [DataContract]
    public sealed class DecimalParameterExpression : DecimalExpression, IParameterExpression
    {
        #region Private Fields

        [DataMember]
        private decimal _value;

        #endregion

        #region Constructors

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
            return new DecimalParameterExpression(_value);
        }

        #endregion

        #region IParameterExpression Members

        object IParameterExpression.Value
        {
            get { return _value; }
        }

        #endregion
    }
}
