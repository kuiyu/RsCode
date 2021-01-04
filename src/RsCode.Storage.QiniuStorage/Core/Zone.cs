namespace RsCode.Storage.QiniuStorage
{
    /// <summary>
    /// 区域
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


        public string UcHost { get; set; }

        public string RsHost { get; set; }




    }
}