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

        /// <summary>
        /// 华东
        /// </summary>
        public static Zone ZONE_CN_East = new Zone()
        {
            ServerUploadDomain = "up.qiniu.com",
            RsfHost = "rsf.qiniu.com",
            ApiHost = "api.qiniu.com",
            IovipHost = "iovip.qbox.me",
            SrcUpHosts = new string[] { "up.qiniup.com","upload.qiniup.com"
                 },
            CdnUpHosts = new string[] { "upload.qiniup.com",
                "upload-nb.qiniup.com", "upload-xs.qiniup.com" }
        };

        /// <summary>
        /// 华北
        /// </summary>
        public static Zone ZONE_CN_North = new Zone()
        {
            ServerUploadDomain = "up-z1.qiniu.com",
            RsfHost = "rsf-z1.qiniu.com",
            ApiHost = "api-z1.qiniu.com",
            IovipHost = "iovip-z1.qbox.me",
            SrcUpHosts = new string[] { "up-z1.qiniup.com", "upload-z1.qiniup.com" },
            CdnUpHosts = new string[] { "upload-z1.qiniup.com" }
        };

        /// <summary>
        /// 华南
        /// </summary>
        public static Zone ZONE_CN_South = new Zone()
        {
            ServerUploadDomain = "up-z2.qiniu.com",
            RsfHost = "rsf-z2.qiniu.com",
            ApiHost = "api-z2.qiniu.com",
            IovipHost = "iovip-z2.qbox.me",
            SrcUpHosts = new string[] { "up-z2.qiniup.com",
                "upload-z2.qiniup.com" },
            CdnUpHosts = new string[] { "upload-z2.qiniup.com",
                "upload-gz.qiniup.com", "upload-fs.qiniup.com" }
        };

        /// <summary>
        /// 北美
        /// </summary>
        public static Zone ZONE_US_North = new Zone()
        {
            ServerUploadDomain = "up-na0.qiniu.com",
            RsfHost = "rsf-na0.qiniu.com",
            ApiHost = "api-na0.qiniu.com",
            IovipHost = "iovip-na0.qbox.me",
            SrcUpHosts = new string[] { "up-na0.qiniup.com", "upload-na0.qiniup.com" },
            CdnUpHosts = new string[] { "upload-na0.qiniup.com" }
        };
    }
}