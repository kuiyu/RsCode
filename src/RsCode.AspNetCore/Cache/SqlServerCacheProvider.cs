using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RsCode.Cache
{
    public class SqlServerCacheProvider : ICacheProvider
    {
        public string CacheName { get;  } = "sqlserver";
        public CacheType CacheType { get;  } = CacheType.SqlServer;
        public bool Exists(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object obj, int seconds = 300)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object obj, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, object obj, int seconds = 300)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync(string key, object obj, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
