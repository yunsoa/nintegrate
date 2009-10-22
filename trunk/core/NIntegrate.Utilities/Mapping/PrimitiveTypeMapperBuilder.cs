using System;

namespace NIntegrate.Utilities.Mapping
{
    internal sealed class PrimitiveTypeMapperBuilder : MapperBuilder
    {
        private readonly MapperCacheKey _mapperCacheKey;

        #region Constructors

        public PrimitiveTypeMapperBuilder(Type fromType, Type toType)
        {
            if (fromType == null)
                throw new ArgumentNullException("fromType");
            if (toType == null)
                throw new ArgumentNullException("toType");

            _mapperCacheKey = new MapperCacheKey(fromType, toType);
        }

        #endregion

        #region Non-Public Methods

        internal override MapperCacheKey GetCacheKey()
        {
            return _mapperCacheKey;
        }

        internal override Delegate BuildMapper()
        {
            //Type.GetTypeCode()
            throw new NotImplementedException();
        }

        #endregion
    }
}
