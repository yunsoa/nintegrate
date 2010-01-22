using System;
using System.Collections.Generic;

namespace NIntegrate.Mapping
{
    /// <summary>
    /// The MapperFactory class is a Emit & delegate based object mapper delegate factory. It dynamically emit dynamic method, cache and return the delegate of the mapper.
    /// </summary>
    public class MapperFactory
    {
        private readonly Dictionary<MapperCacheKey, MapperBuilder> _mapperCache
            = new Dictionary<MapperCacheKey, MapperBuilder>();
        private readonly object _mapperCacheLock = new object();

        #region Non-Public Properties

        internal IDictionary<MapperCacheKey, MapperBuilder> MapperCache
        {
            get { return _mapperCache; }
        }

        #endregion

        #region Public Methods

        public Mapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            Initialize();

            var cacheKey = new MapperCacheKey(typeof(TFrom),typeof(TTo));
            MapperBuilder builder;
            if (!_mapperCache.TryGetValue(cacheKey, out builder))
            {
                lock (_mapperCacheLock)
                {
                    if (!_mapperCache.TryGetValue(cacheKey, out builder))
                    {
                        builder = ConfigureMapper<TFrom, TTo>();
                    }
                }
            }

            var internalMapper = (InternalMapper<TFrom, TTo>)builder.BuildMapper();

            if (internalMapper == null)
                return null;

            return from =>
                       {
                           var to = default(TTo);
                           var type = typeof(TTo);
                           if (!MappingHelper.IsNullableType(type)
                               && !type.IsArray
                               && Type.GetTypeCode(type) == TypeCode.Object)
                           {
                               to = Create<TTo>();
                           }
                           internalMapper(this, from, ref to);
                           return to;
                       };
        }

        public MapperBuilder<TFrom, TTo> ConfigureMapper<TFrom, TTo>()
        {
            return ConfigureMapper<TFrom, TTo>(true, true, true);
        }

        public MapperBuilder<TFrom, TTo> ConfigureMapper<TFrom, TTo>(
            bool autoMap, bool ignoreCase, bool IgnoreUnderscore)
        {
            var builder = new MapperBuilder<TFrom, TTo>(autoMap, ignoreCase, IgnoreUnderscore);
            lock (_mapperCacheLock)
            {
                _mapperCache[builder.CacheKey] = builder;
            }
            return builder;
        }

        #endregion

        #region Non-Public Methods

        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Override this method in derived class to integrate with 3rd party object factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T Create<T>()
        {
            return Activator.CreateInstance<T>();
        }

        #endregion
    }
}
