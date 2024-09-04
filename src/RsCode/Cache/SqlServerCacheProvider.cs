/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */
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
