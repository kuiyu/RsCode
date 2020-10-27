using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace RsCode.Helper
{
    public static class HtmlContentHelper
	{
		private static IEnumerable<string> GetHtmlImageUrlList(string htmlText)
		{
			MatchCollection matchCollections = (new Regex("<img\\b[^<>]*?\\bsrc[\\s\\t\\r\\n]*=[\\s\\t\\r\\n]*[\"']?[\\s\\t\\r\\n]*(?<imgUrl>[^\\s\\t\\r\\n\"'<>]*)[^<>]*?/?[\\s\\t\\r\\n]*>", RegexOptions.IgnoreCase)).Matches(htmlText);
			int num = 0;
			string[] value = new string[matchCollections.Count];
			foreach (Match match in matchCollections)
			{
				int num1 = num;
				num = num1 + 1;
				value[num1] = match.Groups["imgUrl"].Value;
			}
			return value;
		}

		public static string RemoveScriptsAndStyles(string htmlText)
		{
			htmlText = Regex.Replace(htmlText, "<\\s*script[^>]*?>.*?<\\s*/\\s*script\\s*>", "", RegexOptions.IgnoreCase);
			htmlText = Regex.Replace(htmlText, "<\\s*style[^>]*?>.*?<\\s*/\\s*style\\s*>", "", RegexOptions.IgnoreCase);
			return htmlText;
		}

		public static string TransferToLocalImage(string htmlText, string relativeRootPath, string desDir, string imgSrcPreText = "")
		{
			if (!relativeRootPath.EndsWith("/"))
			{
				relativeRootPath = string.Concat(relativeRootPath, "/");
			}
			if (!Directory.Exists(desDir))
			{
				Directory.CreateDirectory(desDir);
			}
			int num = 0;
			List<string> strs = HtmlContentHelper.GetHtmlImageUrlList(htmlText).ToList().FindAll((string imgurl) => !imgurl.StartsWith("data:"));
			WebClient webClient = new WebClient();
			foreach (string str in strs)
			{
				string[] strArrays = str.Split(new char[] { '.' });
				string fileExt = strArrays[strArrays.Length - 1];
                string filename = strArrays[0];
				Guid guid = Guid.NewGuid();
                string str2 = string.Concat(guid.ToString("N"), ".", fileExt);
               // string str2 = string.Concat(filename, ".", fileExt);
				string str3 = string.Concat(desDir, "/", str2);
				try
				{
					if (!str.StartsWith("http://"))
					{
						File.Copy(string.Concat(relativeRootPath, str), str3, true);
					}
					else
					{
						webClient.DownloadFile(str, str3);
					}
					htmlText = htmlText.Replace(str, string.Concat(imgSrcPreText, str2));
				}
				catch
				{
				}
				num++;
			}
			htmlText = htmlText.Replace("<IMG", "<img").Replace("</IMG", "</img");
			return htmlText;
		}
	}
}