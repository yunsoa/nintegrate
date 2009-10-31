using System;
using System.Collections.Generic;

namespace NIntegrate.Utilities.Mapping
{
    public class MapperFactory
    {
        private readonly Dictionary<MapperCacheKey, MapperBuilder> _mapperCache
            = new Dictionary<MapperCacheKey, MapperBuilder>();
        private readonly object _mapperCacheLock = new object();
        private bool _initialized;

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
                           if (!PrimitiveTypeMapperBuilder.IsNullableType(type)
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
            if (!_initialized)
            {
                lock (_mapperCacheLock)
                {
                    if (!_initialized)
                    {
                        InitializePredefinedPrimitiveTypeMappers();
                        _initialized = true;
                    }
                }
            }
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

        private void InitializePredefinedPrimitiveTypeMappers()
        {
            PrimitiveTypeMapperBuilder builder;

            #region bool mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(bool), typeof(bool));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(bool?), typeof(bool?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(bool?), typeof(bool));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(bool), typeof(bool?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region byte mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(byte));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(byte?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(byte));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(byte?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(ushort));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(ushort?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(ushort));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(ushort?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(byte), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region char mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(char), typeof(char));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(char?), typeof(char?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(char?), typeof(char));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(char), typeof(char?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region DateTime mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(DateTime), typeof(DateTime));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(DateTime?), typeof(DateTime?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(DateTime?), typeof(DateTime));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(DateTime), typeof(DateTime?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region decimal mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(decimal), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(decimal?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(decimal?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(decimal), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region double mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(double), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(double?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(double?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(double), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region short mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(short), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region int mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(int), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region long mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(long), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region sbyte mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(sbyte));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(sbyte?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(sbyte));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(sbyte?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(short));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(short?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(sbyte), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region float mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(float), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(float), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region string mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(string), typeof(string));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region ushort mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(ushort));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(ushort?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(ushort));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(ushort?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(int));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(int?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ushort), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region uint mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(uint));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(uint?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(long));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(long?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(uint), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion

            #region ulong mappers

            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(ulong));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(ulong?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(decimal));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(decimal?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(double));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(double?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong?), typeof(float));
            _mapperCache[builder.CacheKey] = builder;
            builder = new PrimitiveTypeMapperBuilder(typeof(ulong), typeof(float?));
            _mapperCache[builder.CacheKey] = builder;

            #endregion
        }

        #endregion
    }
}
