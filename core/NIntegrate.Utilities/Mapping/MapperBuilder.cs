using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIntegrate.Utilities.Mapping
{
    public delegate TValue MappingFrom<TFrom, TValue>(TFrom from);
    public delegate void MappingTo<TTo, TValue>(TTo to, TValue value);
    public delegate void MappingTo2<TTo, TValue1, TValue2>(TTo to, TValue1 value1, TValue2 value2);
    public delegate void MappingTo3<TTo, TValue1, TValue2, TValue3>(TTo to, TValue1 value1, TValue2 value2, TValue3 value3);

    public abstract class MapperBuilder
    {
        internal abstract MapperCacheKey GetCacheKey();

        internal abstract Delegate BuildMapper();
    }

    public class MapperBuilder<TFrom, TTo> : MapperBuilder
    {
        #region Constructors

        internal MapperBuilder(bool autoMap, bool ignoreCase, bool ignoreUnderscore)
        {
            
        }

        #endregion

        #region Public Methods

        public MapperBuilder<TFrom, TTo> From<TValue>(MappingFrom<TFrom, TValue> from)
        {
            throw new NotImplementedException();
        }

        public MapperBuilder<TFrom, TTo> To<TValue>(MappingTo<TTo, TValue> to)
        {
            throw new NotImplementedException();
        }

        public MapperBuilder<TFrom, TTo> To<TValue1, TValue2>(MappingTo2<TTo, TValue1, TValue2> to)
        {
            throw new NotImplementedException();
        }

        public MapperBuilder<TFrom, TTo> To<TValue1, TValue2, TValue3>(MappingTo3<TTo, TValue1, TValue2, TValue3> to)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Non-Public Methods

        internal sealed override MapperCacheKey GetCacheKey()
        {
            return new MapperCacheKey(typeof(TFrom), typeof(TTo));
        }

        internal sealed override Delegate BuildMapper()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
