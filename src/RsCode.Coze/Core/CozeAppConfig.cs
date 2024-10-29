/*
 * 项目:扣子SDK封装RsCode.Coze
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */

namespace RsCode.Coze.Core
{
    /// <summary>
    /// 扣子应用配置
    /// </summary>
    public class CozeAppConfig
    {
        /// <summary>
        /// 创建的应用id
        /// </summary>
     
        public string AppId { get; set; }
        /// <summary>
        /// 应用公钥指纹
        /// </summary>
      
        public string PublicKey { get; set; }
        /// <summary>
        /// 私钥pem文件相对路径
        /// </summary>
       
        public string PrivateKeyPath { get; set; }
    }
}
