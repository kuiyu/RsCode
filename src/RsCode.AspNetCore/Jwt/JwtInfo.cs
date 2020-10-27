using System;
using System.Collections.Generic;
using System.Text;

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
        public string SecurityKey { get; set; }
        /// <summary>
        /// accesstoken过期时间，以分钟为单位
        /// </summary>
        public int Expire { get; set; }
    }
}
