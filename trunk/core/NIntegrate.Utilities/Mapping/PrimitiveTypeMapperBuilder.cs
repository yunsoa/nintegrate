using System;

namespace NIntegrate.Utilities.Mapping
{
    internal sealed class PrimitiveTypeMapperBuilder : MapperBuilder
    {
        private readonly MapperCacheKey _mapperCacheKey;
        private readonly bool _isFromTypeNullable;
        private readonly Type _fromType;
        private readonly TypeCode _fromTypeCode;
        private readonly bool _isToTypeNullable;
        private readonly Type _toType;
        private readonly TypeCode _toTypeCode;

        #region Constructors

        public PrimitiveTypeMapperBuilder(Type fromType, Type toType)
        {
            if (fromType == null)
                throw new ArgumentNullException("fromType");
            if (toType == null)
                throw new ArgumentNullException("toType");

            _mapperCacheKey = new MapperCacheKey(fromType, toType);
            _isFromTypeNullable = IsNullableType(fromType);
            _fromType = _isFromTypeNullable ? fromType.GetGenericArguments()[0] : fromType;
            _fromTypeCode = Type.GetTypeCode(_fromType);
            _isToTypeNullable = IsNullableType(_mapperCacheKey.ToType);
            _toType = _isToTypeNullable ? toType.GetGenericArguments()[0] : toType;
            _toTypeCode = Type.GetTypeCode(_toType);

            if (_fromTypeCode == TypeCode.Object || _toTypeCode == TypeCode.Object)
                throw new ArgumentException("fromType and toType could only be primitive types for constructor of PrimitiveTypeMapperBuilder");
        }

        #endregion

        #region Non-Public Methods

        internal override MapperCacheKey GetCacheKey()
        {
            return _mapperCacheKey;
        }

        internal override Delegate BuildMapper()
        {
            Delegate result = null;

            switch (_fromTypeCode)
            {
                #region case Boolean

                case TypeCode.Boolean:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Boolean:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<bool, bool>(delegate (bool from, ref bool to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<bool?, bool?>(delegate(bool? from, ref bool? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<bool?, bool>(delegate(bool? from, ref bool to) { to = from.HasValue ? from.Value : default(bool); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<bool, bool?>(delegate(bool from, ref bool? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Byte

                case TypeCode.Byte:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Byte:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, byte>(delegate(byte from, ref byte to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, byte?>(delegate(byte? from, ref byte? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, byte>(delegate(byte? from, ref byte to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, byte?>(delegate(byte from, ref byte? to) { to = from; });
                            break;
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, decimal>(delegate(byte from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, decimal?>(delegate(byte? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, decimal>(delegate(byte? from, ref decimal to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, decimal?>(delegate(byte from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, double>(delegate(byte from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, double?>(delegate(byte? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, double>(delegate(byte? from, ref double to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, double?>(delegate(byte from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int16:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, short>(delegate(byte from, ref short to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, short?>(delegate(byte? from, ref short? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, short>(delegate(byte? from, ref short to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, short?>(delegate(byte from, ref short? to) { to = from; });
                            break;
                        case TypeCode.UInt16:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, ushort>(delegate(byte from, ref ushort to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, ushort?>(delegate(byte? from, ref ushort? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, ushort>(delegate(byte? from, ref ushort to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, ushort?>(delegate(byte from, ref ushort? to) { to = from; });
                            break;
                        case TypeCode.Int32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, int>(delegate(byte from, ref int to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, int?>(delegate(byte? from, ref int? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, int>(delegate(byte? from, ref int to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, int?>(delegate(byte from, ref int? to) { to = from; });
                            break;
                        case TypeCode.UInt32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, uint>(delegate(byte from, ref uint to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, uint?>(delegate(byte? from, ref uint? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, uint>(delegate(byte? from, ref uint to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, uint?>(delegate(byte from, ref uint? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, long>(delegate(byte from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, long?>(delegate(byte? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, long>(delegate(byte? from, ref long to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, long?>(delegate(byte from, ref long? to) { to = from; });
                            break;
                        case TypeCode.UInt64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, ulong>(delegate(byte from, ref ulong to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, ulong?>(delegate(byte? from, ref ulong? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, ulong>(delegate(byte? from, ref ulong to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, ulong?>(delegate(byte from, ref ulong? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte, float>(delegate(byte from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte?, float?>(delegate(byte? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<byte?, float>(delegate(byte? from, ref float to) { to = from.HasValue ? from.Value : default(byte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<byte, float?>(delegate(byte from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Char

                case TypeCode.Char:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Char:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<char, char>(delegate(char from, ref char to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<char?, char?>(delegate(char? from, ref char? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<char?, char>(delegate(char? from, ref char to) { to = from.HasValue ? from.Value : default(char); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<char, char?>(delegate(char from, ref char? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case DateTime

                case TypeCode.DateTime:
                    switch (_toTypeCode)
                    {
                        case TypeCode.DateTime:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<DateTime, DateTime>(delegate(DateTime from, ref DateTime to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<DateTime?, DateTime?>(delegate(DateTime? from, ref DateTime? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<DateTime?, DateTime>(delegate(DateTime? from, ref DateTime to) { to = from.HasValue ? from.Value : default(DateTime); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<DateTime, DateTime?>(delegate(DateTime from, ref DateTime? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Decimal

                case TypeCode.Decimal:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<decimal, decimal>(delegate(decimal from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<decimal?, decimal?>(delegate(decimal? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<decimal?, decimal>(delegate(decimal? from, ref decimal to) { to = from.HasValue ? from.Value : default(decimal); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<decimal, decimal?>(delegate(decimal from, ref decimal? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Double

                case TypeCode.Double:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<double, double>(delegate(double from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<double?, double?>(delegate(double? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<double?, double>(delegate(double? from, ref double to) { to = from.HasValue ? from.Value : default(double); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<double, double?>(delegate(double from, ref double? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Int16

                case TypeCode.Int16:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, decimal>(delegate(short from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, decimal?>(delegate(short? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, decimal>(delegate(short? from, ref decimal to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, decimal?>(delegate(short from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, double>(delegate(short from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, double?>(delegate(short? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, double>(delegate(short? from, ref double to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, double?>(delegate(short from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int16:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, short>(delegate(short from, ref short to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, short?>(delegate(short? from, ref short? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, short>(delegate(short? from, ref short to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, short?>(delegate(short from, ref short? to) { to = from; });
                            break;
                        case TypeCode.Int32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, int>(delegate(short from, ref int to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, int?>(delegate(short? from, ref int? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, int>(delegate(short? from, ref int to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, int?>(delegate(short from, ref int? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, long>(delegate(short from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, long?>(delegate(short? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, long>(delegate(short? from, ref long to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, long?>(delegate(short from, ref long? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short, float>(delegate(short from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short?, float?>(delegate(short? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<short?, float>(delegate(short? from, ref float to) { to = from.HasValue ? from.Value : default(short); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<short, float?>(delegate(short from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Int32

                case TypeCode.Int32:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int, decimal>(delegate(int from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int?, decimal?>(delegate(int? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int?, decimal>(delegate(int? from, ref decimal to) { to = from.HasValue ? from.Value : default(int); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int, decimal?>(delegate(int from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int, double>(delegate(int from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int?, double?>(delegate(int? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int?, double>(delegate(int? from, ref double to) { to = from.HasValue ? from.Value : default(int); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int, double?>(delegate(int from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int, int>(delegate(int from, ref int to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int?, int?>(delegate(int? from, ref int? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int?, int>(delegate(int? from, ref int to) { to = from.HasValue ? from.Value : default(int); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int, int?>(delegate(int from, ref int? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int, long>(delegate(int from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int?, long?>(delegate(int? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int?, long>(delegate(int? from, ref long to) { to = from.HasValue ? from.Value : default(int); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int, long?>(delegate(int from, ref long? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int, float>(delegate(int from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int?, float?>(delegate(int? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<int?, float>(delegate(int? from, ref float to) { to = from.HasValue ? from.Value : default(int); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<int, float?>(delegate(int from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Int64

                case TypeCode.Int64:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long, decimal>(delegate(long from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long?, decimal?>(delegate(long? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long?, decimal>(delegate(long? from, ref decimal to) { to = from.HasValue ? from.Value : default(long); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long, decimal?>(delegate(long from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long, double>(delegate(long from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long?, double?>(delegate(long? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long?, double>(delegate(long? from, ref double to) { to = from.HasValue ? from.Value : default(long); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long, double?>(delegate(long from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long, long>(delegate(long from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long?, long?>(delegate(long? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long?, long>(delegate(long? from, ref long to) { to = from.HasValue ? from.Value : default(long); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long, long?>(delegate(long from, ref long? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long, float>(delegate(long from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long?, float?>(delegate(long? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<long?, float>(delegate(long? from, ref float to) { to = from.HasValue ? from.Value : default(long); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<long, float?>(delegate(long from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case SByte

                case TypeCode.SByte:
                    switch (_toTypeCode)
                    {
                        case TypeCode.SByte:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, sbyte>(delegate(sbyte from, ref sbyte to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, sbyte?>(delegate(sbyte? from, ref sbyte? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, sbyte>(delegate(sbyte? from, ref sbyte to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, sbyte?>(delegate(sbyte from, ref sbyte? to) { to = from; });
                            break;
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, decimal>(delegate(sbyte from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, decimal?>(delegate(sbyte? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, decimal>(delegate(sbyte? from, ref decimal to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, decimal?>(delegate(sbyte from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, double>(delegate(sbyte from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, double?>(delegate(sbyte? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, double>(delegate(sbyte? from, ref double to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, double?>(delegate(sbyte from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int16:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, short>(delegate(sbyte from, ref short to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, short?>(delegate(sbyte? from, ref short? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, short>(delegate(sbyte? from, ref short to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, short?>(delegate(sbyte from, ref short? to) { to = from; });
                            break;
                        case TypeCode.Int32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, int>(delegate(sbyte from, ref int to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, int?>(delegate(sbyte? from, ref int? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, int>(delegate(sbyte? from, ref int to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, int?>(delegate(sbyte from, ref int? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, long>(delegate(sbyte from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, long?>(delegate(sbyte? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, long>(delegate(sbyte? from, ref long to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, long?>(delegate(sbyte from, ref long? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte, float>(delegate(sbyte from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte?, float?>(delegate(sbyte? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<sbyte?, float>(delegate(sbyte? from, ref float to) { to = from.HasValue ? from.Value : default(sbyte); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<sbyte, float?>(delegate(sbyte from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case Single

                case TypeCode.Single:
                    switch (_toTypeCode)
                    {
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<float, double>(delegate(float from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<float?, double?>(delegate(float? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<float?, double>(delegate(float? from, ref double to) { to = from.HasValue ? from.Value : default(float); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<float, double?>(delegate(float from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<float, float>(delegate(float from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<float?, float?>(delegate(float? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<float?, float>(delegate(float? from, ref float to) { to = from.HasValue ? from.Value : default(float); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<float, float?>(delegate(float from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case String

                case TypeCode.String:
                    switch (_toTypeCode)
                    {
                        case TypeCode.String:
                            result = new InternalMapper<string, string>(delegate(string from, ref string to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case UInt16

                case TypeCode.UInt16:
                    switch (_toTypeCode)
                    {
                        case TypeCode.UInt16:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, ushort>(delegate(ushort from, ref ushort to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, ushort?>(delegate(ushort? from, ref ushort? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, ushort>(delegate(ushort? from, ref ushort to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, ushort?>(delegate(ushort from, ref ushort? to) { to = from; });
                            break;
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, decimal>(delegate(ushort from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, decimal?>(delegate(ushort? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, decimal>(delegate(ushort? from, ref decimal to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, decimal?>(delegate(ushort from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, double>(delegate(ushort from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, double?>(delegate(ushort? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, double>(delegate(ushort? from, ref double to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, double?>(delegate(ushort from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, int>(delegate(ushort from, ref int to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, int?>(delegate(ushort? from, ref int? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, int>(delegate(ushort? from, ref int to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, int?>(delegate(ushort from, ref int? to) { to = from; });
                            break;
                        case TypeCode.UInt32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, uint>(delegate(ushort from, ref uint to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, uint?>(delegate(ushort? from, ref uint? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, uint>(delegate(ushort? from, ref uint to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, uint?>(delegate(ushort from, ref uint? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, long>(delegate(ushort from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, long?>(delegate(ushort? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, long>(delegate(ushort? from, ref long to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, long?>(delegate(ushort from, ref long? to) { to = from; });
                            break;
                        case TypeCode.UInt64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, ulong>(delegate(ushort from, ref ulong to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, ulong?>(delegate(ushort? from, ref ulong? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, ulong>(delegate(ushort? from, ref ulong to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, ulong?>(delegate(ushort from, ref ulong? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort, float>(delegate(ushort from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort?, float?>(delegate(ushort? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ushort?, float>(delegate(ushort? from, ref float to) { to = from.HasValue ? from.Value : default(ushort); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ushort, float?>(delegate(ushort from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case UInt32

                case TypeCode.UInt32:
                    switch (_toTypeCode)
                    {
                        case TypeCode.UInt32:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, uint>(delegate(uint from, ref uint to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, uint?>(delegate(uint? from, ref uint? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, uint>(delegate(uint? from, ref uint to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, uint?>(delegate(uint from, ref uint? to) { to = from; });
                            break;
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, decimal>(delegate(uint from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, decimal?>(delegate(uint? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, decimal>(delegate(uint? from, ref decimal to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, decimal?>(delegate(uint from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, double>(delegate(uint from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, double?>(delegate(uint? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, double>(delegate(uint? from, ref double to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, double?>(delegate(uint from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Int64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, long>(delegate(uint from, ref long to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, long?>(delegate(uint? from, ref long? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, long>(delegate(uint? from, ref long to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, long?>(delegate(uint from, ref long? to) { to = from; });
                            break;
                        case TypeCode.UInt64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, ulong>(delegate(uint from, ref ulong to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, ulong?>(delegate(uint? from, ref ulong? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, ulong>(delegate(uint? from, ref ulong to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, ulong?>(delegate(uint from, ref ulong? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint, float>(delegate(uint from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint?, float?>(delegate(uint? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<uint?, float>(delegate(uint? from, ref float to) { to = from.HasValue ? from.Value : default(uint); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<uint, float?>(delegate(uint from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion

                #region case UInt64

                case TypeCode.UInt64:
                    switch (_toTypeCode)
                    {
                        case TypeCode.UInt64:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong, ulong>(delegate(ulong from, ref ulong to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong?, ulong?>(delegate(ulong? from, ref ulong? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong?, ulong>(delegate(ulong? from, ref ulong to) { to = from.HasValue ? from.Value : default(ulong); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong, ulong?>(delegate(ulong from, ref ulong? to) { to = from; });
                            break;
                        case TypeCode.Decimal:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong, decimal>(delegate(ulong from, ref decimal to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong?, decimal?>(delegate(ulong? from, ref decimal? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong?, decimal>(delegate(ulong? from, ref decimal to) { to = from.HasValue ? from.Value : default(ulong); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong, decimal?>(delegate(ulong from, ref decimal? to) { to = from; });
                            break;
                        case TypeCode.Double:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong, double>(delegate(ulong from, ref double to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong?, double?>(delegate(ulong? from, ref double? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong?, double>(delegate(ulong? from, ref double to) { to = from.HasValue ? from.Value : default(ulong); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong, double?>(delegate(ulong from, ref double? to) { to = from; });
                            break;
                        case TypeCode.Single:
                            if (!_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong, float>(delegate(ulong from, ref float to) { to = from; });
                            else if (_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong?, float?>(delegate(ulong? from, ref float? to) { to = from; });
                            else if (_isFromTypeNullable && !_isToTypeNullable)
                                result = new InternalMapper<ulong?, float>(delegate(ulong? from, ref float to) { to = from.HasValue ? from.Value : default(ulong); });
                            else if (!_isFromTypeNullable && _isToTypeNullable)
                                result = new InternalMapper<ulong, float?>(delegate(ulong from, ref float? to) { to = from; });
                            break;
                    }
                    break;

                #endregion
            }

            return result;
        }

        internal static bool IsNullableType(Type type)
        {
            return type.IsGenericType 
                && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        #endregion
    }
}
