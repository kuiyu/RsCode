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