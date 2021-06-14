using System;
using System.IO;
using System.Xml.Serialization;

namespace RsCode.Helper
{
	/// <summary>
	/// xml helper
	/// </summary>
	public class XmlHelper
	{
		/// <summary>
		/// xml转对象
		/// </summary>
		/// <param name="type"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static object DeserializeFromXML(Type type, string filePath)
		{
			object obj;
			FileStream fileStream = null;
			try
			{
				try
				{
					fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
					obj = (new XmlSerializer(type)).Deserialize(fileStream);
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return obj;
		}
		/// <summary>
		/// xml序列化
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="filePath"></param>
		/// <returns></returns>
		public static bool SerializeToXml(object obj, string filePath)
		{
			bool flag = false;
			FileStream fileStream = null;
			try
			{
				try
				{
					fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
					(new XmlSerializer(obj.GetType())).Serialize(fileStream, obj);
					flag = true;
				}
				catch (Exception exception)
				{
					throw exception;
				}
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return flag;
		}
	}
}