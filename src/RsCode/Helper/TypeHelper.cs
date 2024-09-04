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

namespace System
{
	public static class TypeHelper
	{
	 
		public static bool ObjectToBool(object o, bool defaultValue)
		{
			bool flag;
			flag = (o == null ? defaultValue : TypeHelper.StringToBool(o.ToString(), defaultValue));
			return flag;
		}

		public static bool ObjectToBool(object o)
		{
			return TypeHelper.ObjectToBool(o, false);
		}

		public static DateTime ObjectToDateTime(object o, DateTime defaultValue)
		{
			DateTime dateTime;
			dateTime = (o == null ? defaultValue : TypeHelper.StringToDateTime(o.ToString(), defaultValue));
			return dateTime;
		}

		public static DateTime ObjectToDateTime(object o)
		{
			return TypeHelper.ObjectToDateTime(o, DateTime.Now);
		}

		public static decimal ObjectToDecimal(object o, decimal defaultValue)
		{
			decimal num;
			num = (o == null ? defaultValue : TypeHelper.StringToDecimal(o.ToString(), defaultValue));
			return num;
		}

		public static decimal ObjectToDecimal(object o)
		{
			return TypeHelper.ObjectToDecimal(o, new decimal(0));
		}

		public static int ObjectToInt(object o, int defaultValue)
		{
			int num;
			num = (o == null ? defaultValue : TypeHelper.StringToInt(o.ToString(), defaultValue));
			return num;
		}

		public static int ObjectToInt(object o)
		{
			return TypeHelper.ObjectToInt(o, 0);
		}

		public static bool StringToBool(string s, bool defaultValue)
		{
			bool flag;
			if (!(s == "false"))
			{
				flag = (!(s == "true") ? defaultValue : true);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static DateTime StringToDateTime(string s, DateTime defaultValue)
		{
			DateTime dateTime;
			DateTime dateTime1;
			if (!string.IsNullOrWhiteSpace(s))
			{
				if (DateTime.TryParse(s, out dateTime))
				{
					dateTime1 = dateTime;
					return dateTime1;
				}
			}
			dateTime1 = defaultValue;
			return dateTime1;
		}

		public static DateTime StringToDateTime(string s)
		{
			return TypeHelper.StringToDateTime(s, DateTime.Now);
		}

		public static decimal StringToDecimal(string s, decimal defaultValue)
		{
			decimal num;
			decimal num1;
			if (!string.IsNullOrWhiteSpace(s))
			{
				if (decimal.TryParse(s, out num))
				{
					num1 = num;
					return num1;
				}
			}
			num1 = defaultValue;
			return num1;
		}

		public static decimal StringToDecimal(string s)
		{
			return TypeHelper.StringToDecimal(s, new decimal(0));
		}

		public static int StringToInt(string s, int defaultValue)
		{
			int num;
			int num1;
			if (!string.IsNullOrWhiteSpace(s))
			{
				if (int.TryParse(s, out num))
				{
					num1 = num;
					return num1;
				}
			}
			num1 = defaultValue;
			return num1;
		}

		public static int StringToInt(string s)
		{
			return TypeHelper.StringToInt(s, 0);
		}

		public static bool ToBool(string s)
		{
			return TypeHelper.StringToBool(s, false);
		}
	}
}