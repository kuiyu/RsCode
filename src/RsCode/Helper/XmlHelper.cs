using System;
using System.IO;
using System.Xml.Serialization;

namespace RsCode.Helper
{
	public class XmlHelper
	{
		public XmlHelper()
		{
		}

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