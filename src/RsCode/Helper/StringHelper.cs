using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public class StringHelper
	{
		public StringHelper()
		{
		}

		public static int GetStringLength(string s)
		{
			int num;
			num = (string.IsNullOrEmpty(s) ? 0 : Encoding.Default.GetBytes(s).Length);
			return num;
		}

		public static int IndexOf(string s, int order)
		{
			return StringHelper.IndexOf(s, '-', order);
		}

		public static int IndexOf(string s, char c, int order)
		{
			int num;
			int length = s.Length;
			int num1 = 0;
			while (true)
			{
				if (num1 < length)
				{
					if (c == s[num1])
					{
						if (order != 1)
						{
							order--;
						}
						else
						{
							num = num1;
							break;
						}
					}
					num1++;
				}
				else
				{
					num = -1;
					break;
				}
			}
			return num;
		}

		public static string[] SplitString(string sourceStr, string splitStr)
		{
			string[] strArrays;
			if (!(string.IsNullOrEmpty(sourceStr) ? false : !string.IsNullOrEmpty(splitStr)))
			{
				strArrays = new string[0];
			}
			else if (sourceStr.IndexOf(splitStr) == -1)
			{
				strArrays = new string[] { sourceStr };
			}
			else if (splitStr.Length != 1)
			{
				strArrays = Regex.Split(sourceStr, Regex.Escape(splitStr), RegexOptions.IgnoreCase);
			}
			else
			{
				char[] chrArray = new char[] { splitStr[0] };
				strArrays = sourceStr.Split(chrArray);
			}
			return strArrays;
		}

		public static string[] SplitString(string sourceStr)
		{
			return StringHelper.SplitString(sourceStr, ",");
		}

		public static string SubString(string sourceStr, int startIndex, int length)
		{
			string str;
			if (string.IsNullOrEmpty(sourceStr))
			{
				str = "";
			}
			else
			{
				str = (sourceStr.Length < startIndex + length ? sourceStr.Substring(startIndex) : sourceStr.Substring(startIndex, length));
			}
			return str;
		}

		public static string SubString(string sourceStr, int length)
		{
			return StringHelper.SubString(sourceStr, 0, length);
		}

		public static string Trim(string sourceStr, string trimStr)
		{
			return StringHelper.Trim(sourceStr, trimStr, true);
		}

		public static string Trim(string sourceStr, string trimStr, bool ignoreCase)
		{
			string empty;
			if (string.IsNullOrEmpty(sourceStr))
			{
				empty = string.Empty;
			}
			else if (!string.IsNullOrEmpty(trimStr))
			{
				if (sourceStr.StartsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
				{
					sourceStr = sourceStr.Remove(0, trimStr.Length);
				}
				if (sourceStr.EndsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture))
				{
					sourceStr = sourceStr.Substring(0, sourceStr.Length - trimStr.Length);
				}
				empty = sourceStr;
			}
			else
			{
				empty = sourceStr;
			}
			return empty;
		}

		public static string TrimEnd(string sourceStr, string trimStr)
		{
			return StringHelper.TrimEnd(sourceStr, trimStr, true);
		}

		public static string TrimEnd(string sourceStr, string trimStr, bool ignoreCase)
		{
			string empty;
			if (!string.IsNullOrEmpty(sourceStr))
			{
				empty = ((string.IsNullOrEmpty(trimStr) ? false : sourceStr.EndsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture)) ? sourceStr.Substring(0, sourceStr.Length - trimStr.Length) : sourceStr);
			}
			else
			{
				empty = string.Empty;
			}
			return empty;
		}

		public static string TrimStart(string sourceStr, string trimStr)
		{
			return StringHelper.TrimStart(sourceStr, trimStr, true);
		}

		public static string TrimStart(string sourceStr, string trimStr, bool ignoreCase)
		{
			string empty;
			if (!string.IsNullOrEmpty(sourceStr))
			{
				empty = ((string.IsNullOrEmpty(trimStr) ? false : sourceStr.StartsWith(trimStr, ignoreCase, CultureInfo.CurrentCulture)) ? sourceStr.Remove(0, trimStr.Length) : sourceStr);
			}
			else
			{
				empty = string.Empty;
			}
			return empty;
		}
	}
}