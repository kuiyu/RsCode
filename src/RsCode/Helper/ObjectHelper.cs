/*
 * RsCode
 * 
 * RsCode�ǿ��ٿ���.netӦ�õĹ��߿�,��ḻ�Ĺ��ܺ������ԣ��ܹ��������.net������Ч�ʺ�������
 * Э�飺MIT License
 * ���ߣ�runsoft1024
 * ΢�ţ�runsoft1024
 * �ĵ� https://rscode.cn/
 * 
 * ��Ŀ���й���
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */
using Newtonsoft.Json;
 

namespace System
{
	public static class ObjectHelper
	{
		public static object DeepColne(object obj)
		{
			return JsonConvert.DeserializeObject(JsonConvert.SerializeObject(obj));
		}

		public static T DeepColne<T>(T t)
		{
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(t));
		}
	}
}