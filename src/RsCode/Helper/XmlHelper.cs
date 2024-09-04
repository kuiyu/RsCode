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
using System;
using System.Globalization;
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
		/// ��ָ��xml�ļ�������ת����
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
		/// ��ָ��xml�ļ����������л�
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

		/// <summary>
		/// ��xml�ַ���ת������
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <param name="xmlRoot"></param>
		/// <returns></returns>
        public static T Deserializer<T>(string xml, string xmlRoot = "")
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(xmlRoot));
            using (StringReader sr = new StringReader(xml))
            {
                var obj = serializer.Deserialize(sr);
                return obj != null ? (T)obj : default(T);
            }
            return default(T);
        }
		/// <summary>
		/// ������ת��xml�ַ���
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="xsd"></param>
		/// <param name="xsi"></param>
		/// <param name="xmlRoot"></param>
		/// <returns></returns>
        public static string Serializer<T>(T obj, string xsd = "", string xsi = "", string xmlRoot = "")
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            if (!string.IsNullOrWhiteSpace(xsd))
            {
                ns.Add("xsd", xsd);
            }
            if (!string.IsNullOrWhiteSpace(xsi))
            {
                ns.Add("xsi", xsi);
            }

            XmlRootAttribute xra = new XmlRootAttribute(xmlRoot);
            XmlSerializer serializer = new XmlSerializer(typeof(T), xra);
            using (StringWriter writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                serializer.Serialize(writer, obj, ns);
                string xml = writer.ToString();
                return xml;
            }
        }
    }
}