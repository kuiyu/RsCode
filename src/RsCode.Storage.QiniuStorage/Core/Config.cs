using AspectCore.DependencyInjection;
using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 配置信息，主要包括Zone配置(另请参阅Zone模块)
    /// 目前已支持的机房包括：
    /// 华东(CN_East), 华北(CN_North), 华南(CN_South), 北美(US_North)
    /// 默认设置为华东机房(CN_East) 
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 默认高级资源管理域名
        /// </summary>
        public static string DefaultRsHost = "http://rs.qiniu.com";
        /// <summary>
        /// 默认数据处理域名
        /// </summary>
        public static string DefaultApiHost = "http://api.qiniu.com";
        /// <summary>
        /// 空间所在的区域(Zone)
        /// </summary>
        public Zone Zone = null;
        /// <summary>
        /// 是否采用https域名
        /// </summary>
        public bool UseHttps = false;
        /// <summary>
        /// 是否采用CDN加速域名，对上传有效
        /// </summary>
        public bool UseCdnDomains = false;
        /// <summary>
        /// 分片上传时，片的大小，默认为4MB，以提高上传效率
        /// </summary>
        public ChunkUnit ChunkSize = ChunkUnit.U4096K;
        /// <summary>
        /// 分片上传的阈值，超过该大小采用分片上传的方式
        /// </summary>
        public int PutThreshold = ResumeChunk.GetChunkSize(ChunkUnit.U1024K) * 10;
        /// <summary>
        /// 重试请求次数
        /// </summary>
        public int MaxRetryTimes = 3;

        
    }
}
