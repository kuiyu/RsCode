/*
 * 项目:抖音开放平台SDK RsCode.Douyin 
 * 协议:MIT License 2.0 
 * 作者:河南软商网络科技有限公司 
 * 项目己托管于  
 * github
   https://github.com/kuiyu/RsCode.git
 */


namespace RsCode.Douyin
{
    public abstract class DouyinRequest
    {
        public virtual string RequestMethod()
        {
            return "POST";
        }
        public virtual  string GetApiUrl()
        {
            throw new Exception("请配置接口地址");
        }
        /// <summary>
        /// 担保支付请求签名算法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public string CreateParamSign(DouyinOptions options)
        {
            
            List<string> paramList = new List<string>();
            //对属性进行ascii码排序 
            var props = this.GetType().GetProperties();
            foreach (var prop in props)
            {
                //排除不需要参数
                if (prop.Name == "AppId" || prop.Name == "ThirdPartyId" || prop.Name == "Sign" || prop.Name == "OtherSettleParams")
                { continue;
                }

                var value = prop.GetValue(this);
                if (value == null)
                {
                    //paramList.Add("<nil>");
                }
                else
                {
                     paramList.Add(value.ToString().Trim());
                }
            }
            paramList.Add(options.Salt);

            var list = paramList.ToArray();
            Array.Sort(list, string.CompareOrdinal);

            string s = "";
            foreach (var obj in list)
            {
                if(obj==null||string.IsNullOrWhiteSpace(obj))
                {
                    continue;
                }
                string value = obj.ToString();

                value = value.Trim();
                if(value.Equals("")||value.Equals("null"))
                {
                    continue;
                }

                if (string.IsNullOrEmpty(s))
                {
                    s=obj.Trim();
                }else
                {
                    s += $"&{obj}";
                }
            }
            
            var result = DouyinTool.MD5Sign(s);
            
            return result;
        }


        
    }
}
