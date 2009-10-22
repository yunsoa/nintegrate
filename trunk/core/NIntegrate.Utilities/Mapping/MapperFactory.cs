using System;
using System.Collections.Generic;

namespace NIntegrate.Utilities.Mapping
{
    public delegate TTo Mapper<TFrom, TTo>(TFrom from);

    internal delegate void InternalMapper<TFrom, TTo>(TFrom from, TTo to);

    public class MapperFactory
    {
        private readonly Dictionary<MapperCacheKey, MapperBuilder> _mapperCache
            = new Dictionary<MapperCacheKey, MapperBuilder>();
        private readonly object _mapperCacheLock = new object();
        private bool _initialized;

        #region Public Methods

        public Mapper<TFrom, TTo> GetMapper<TFrom, TTo>()
        {
            if (!_initialized)
            {
                lock (_mapperCacheLock)
                {
                    if (!_initialized)
                    {
                        Initialize();
                        _initialized = true;
                    }
                }
            }

            var cacheKey = new MapperCacheKey(typeof(TFrom),typeof(TTo));
            MapperBuilder builder;
            if (!_mapperCache.TryGetValue(cacheKey, out builder))
            {
                lock (_mapperCacheLock)
                {
                    if (!_mapperCache.TryGetValue(cacheKey, out builder))
                    {
                        builder = ConfigureMapper<TFrom, TTo>(true, true, true);
                    }
                }
            }

            var internalMapper = (InternalMapper<TFrom, TTo>)builder.BuildMapper();
            return from =>
                       {
                           var to = Create<TTo>();
                           internalMapper(from, to);
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
                _mapperCache[builder.GetCacheKey()] = builder;
            }
            return builder;
        }

        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Override this method in derived class to integrate with 3rd party object factory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual T Create<T>()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Override this method in derived class to customize the initialization.
        /// The default implementation predefines mappers for all primitive types.
        /// </summary>
        protected virtual void Initialize()
        {
            //...
        }

        #endregion
    }
}
