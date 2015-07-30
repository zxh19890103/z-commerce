using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Runtime;

namespace Nt.BLL
{
    public class CachingManager
    {

        const int _cachingTime = 2;
        private Cache _cache = null;

        #region singleton

        private CachingManager()
        {
            _cache = System.Web.HttpContext.Current.Cache;
        }

        static CachingManager _current = null;
        static readonly object padlock = new object();

        public static CachingManager Current
        {
            get
            {
                lock (padlock)
                {
                    if (_current == null)
                    {
                        _current = new CachingManager();
                    }
                    return _current;
                }
            }
        }

        #endregion

        public void Cache(string key, Object obj)
        {
            if (HasCached(key))
                Remove(key);
            _cache.Add(
                key,
                obj,
                null,
                DateTime.MaxValue,
                TimeSpan.FromMinutes(_cachingTime),
                CacheItemPriority.Normal,
                null);
        }

        public bool HasCached(string key)
        {
            return _cache.Get(key) != null;
        }

        public T Get<T>(string key)
        {
            if (_cache[key] == null)
                return default(T);
            return (T)_cache[key];
        }

        public void Remove(string key)
        {
            if (HasCached(key))
                _cache.Remove(key);
        }

    }
}
