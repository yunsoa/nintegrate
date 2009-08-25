using System.Collections;
using EnterpriseSharedServiceContracts;
using System;

namespace EnterpriseSharedServiceImpls
{
    public sealed class CachingServiceImpl : ICachingService
    {
        private sealed class CacheItem
        {
            public byte[] Data;
            public DateTime ExpireTime;
        }

        private static readonly Hashtable _cache = Hashtable.Synchronized(new Hashtable());

        #region ICachingService Members

        public byte[] GetCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;

            var item = _cache[key] as CacheItem;

            if (item != null)
            {
                if (item.ExpireTime <= DateTime.Now)
                {
                    _cache.Remove(key);
                    return null;
                }
                return item.Data;
            }

            return null;
        }

        public void SetCache(string key, byte[] data, TimeSpan expireTimeSpan)
        {
            if (string.IsNullOrEmpty(key) || expireTimeSpan == TimeSpan.Zero)
                return;

            lock (_cache)
            {
                var item = _cache[key] as CacheItem;
                if (item != null)
                {
                    item.Data = data;
                    item.ExpireTime = DateTime.Now.Add(expireTimeSpan);
                    _cache[key] = item;
                }
                else
                {
                    _cache.Add(key, new CacheItem { Data = data, ExpireTime = DateTime.Now.Add(expireTimeSpan)});
                }
            }
        }

        #endregion
    }
}
