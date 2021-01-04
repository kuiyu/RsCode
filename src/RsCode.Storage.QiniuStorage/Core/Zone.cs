using RsCode.Storage.QiniuStorage.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 目前已支持的区域：华东/华北/华南/北美
    /// </summary>
    public class Zone
    {
        
         
        /// <summary>
        /// 服务器端上传
        /// </summary>
        public string ServerUploadDomain { set; get; }

        /// <summary>
        /// 源列表
        /// </summary>
        public string RsfHost { set; get; }

        /// <summary>
        /// 数据处理
        /// </summary>
        public string ApiHost { set; get; }

        /// <summary>
        /// 镜像刷新、资源抓取
        /// </summary>
        public string IovipHost { set; get; }

        /// <summary>
        /// 资源上传
        /// </summary>
        public string[] SrcUpHosts { set; get; }

        /// <summary>
        /// CDN加速
        /// </summary>
        public string[] CdnUpHosts { set; get; }
 
     
        

         
        
        

       
    }
}