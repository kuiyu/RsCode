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

namespace RsCode.AspNetCore.Jwt
{
    public class JwtInfo
    {
        /// <summary>
        /// 发行方
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 接收方
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 安全码
        /// </summary>
        public string SecurityKey { get; set; } = "";
        /// <summary>
        /// accesstoken过期时间，以分钟为单位
        /// </summary>
        public int Expire { get; set; }

        public string PublicKey { get; set; } = "";
        public string PublicKeyPath { get; set; } = "";

        public string PrivateKey { get; set; } = "";

        public string PrivateKeyPath { get; set; } = "";
    }
}
