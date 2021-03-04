using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RsCode
{
	public class SecureHelper
	{
		private readonly static byte[] _aeskeys;

		private static Regex _base64regex;

		private static Regex _sqlkeywordregex;

		static SecureHelper()
		{
			SecureHelper._aeskeys = new byte[] { 18, 52, 86, 120, 144, 171, 205, 239, 18, 52, 86, 120, 144, 171, 205, 239 };
			SecureHelper._base64regex = new Regex("[A-Za-z0-9\\=\\/\\+]");
			SecureHelper._sqlkeywordregex = new Regex("(select|insert|delete|from|count\\(|drop|table|update|truncate|asc\\(|mid\\(|char\\(|xp_cmdshell|exec|master|net|local|group|administrators|user|or|and|-|;|,|\\(|\\)|\\[|\\]|\\{|\\}|%|@|\\*|!|\\')", RegexOptions.IgnoreCase);
		}

		public SecureHelper()
		{
		}

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decryptStr">要解密的字符串</param>
        /// <param name="decryptKey">解密使用的key</param>
        /// <returns></returns>
		public static string AESDecrypt(string decryptStr, string decryptKey)
		{
			string empty;
			if (!string.IsNullOrWhiteSpace(decryptStr))
			{
				decryptKey = StringHelper.SubString(decryptKey, 32);
				decryptKey = decryptKey.PadRight(32, ' ');
				byte[] numArray = Convert.FromBase64String(decryptStr);
				SymmetricAlgorithm bytes = Rijndael.Create();
				bytes.Key = Encoding.UTF8.GetBytes(decryptKey);
				bytes.IV = SecureHelper._aeskeys;
				byte[] numArray1 = new byte[numArray.Length];
				MemoryStream memoryStream = new MemoryStream(numArray);
				try
				{
					CryptoStream cryptoStream = new CryptoStream(memoryStream, bytes.CreateDecryptor(), CryptoStreamMode.Read);
					try
					{
						cryptoStream.Read(numArray1, 0, numArray1.Length);
						cryptoStream.Close();
						memoryStream.Close();
					}
					finally
					{
						if (cryptoStream != null)
						{
							((IDisposable)cryptoStream).Dispose();
						}
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						((IDisposable)memoryStream).Dispose();
					}
				}
				empty = Encoding.UTF8.GetString(numArray1).Replace("\0", "");
			}
			else
			{
				empty = string.Empty;
			}
			return empty;
		}

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encryptStr">要加密的字符串</param>
        /// <param name="encryptKey">加密使用的key</param>
        /// <returns></returns>
		public static string AESEncrypt(string encryptStr, string encryptKey)
		{
			string base64String;
			if (!string.IsNullOrWhiteSpace(encryptStr))
			{
				encryptKey = StringHelper.SubString(encryptKey, 32);
				encryptKey = encryptKey.PadRight(32, ' ');
				SymmetricAlgorithm bytes = Rijndael.Create();
				byte[] numArray = Encoding.UTF8.GetBytes(encryptStr);
				bytes.Key = Encoding.UTF8.GetBytes(encryptKey);
				bytes.IV = SecureHelper._aeskeys;
				byte[] array = null;
				MemoryStream memoryStream = new MemoryStream();
				try
				{
					CryptoStream cryptoStream = new CryptoStream(memoryStream, bytes.CreateEncryptor(), CryptoStreamMode.Write);
					try
					{
						cryptoStream.Write(numArray, 0, numArray.Length);
						cryptoStream.FlushFinalBlock();
						array = memoryStream.ToArray();
						cryptoStream.Close();
						memoryStream.Close();
					}
					finally
					{
						if (cryptoStream != null)
						{
							((IDisposable)cryptoStream).Dispose();
						}
					}
				}
				finally
				{
					if (memoryStream != null)
					{
						((IDisposable)memoryStream).Dispose();
					}
				}
				base64String = Convert.ToBase64String(array);
			}
			else
			{
				base64String = string.Empty;
			}
			return base64String;
		}

		public static string DecodeBase64(Encoding encode, string result)
		{
			string str = "";
			byte[] numArray = Convert.FromBase64String(result);
			try
			{
				str = encode.GetString(numArray);
			}
			catch
			{
				str = result;
			}
			return str;
		}

		public static string DecodeBase64(string result)
		{
			return SecureHelper.DecodeBase64(Encoding.UTF8, result);
		}

		public static string EncodeBase64(Encoding encode, string source)
		{
			return Convert.ToBase64String(encode.GetBytes(source));
		}

		public static string EncodeBase64(string source)
		{
			return SecureHelper.EncodeBase64(Encoding.UTF8, source);
		}

		public static bool IsBase64String(string str)
		{
			bool flag;
			flag = (str == null ? true : SecureHelper._base64regex.IsMatch(str));
			return flag;
		}

		public static bool IsSafeSqlString(string s)
		{
			return ((s == null ? true : !SecureHelper._sqlkeywordregex.IsMatch(s)) ? true : false);
		}

		public static string MD5(string inputStr)
		{
			MD5 mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] numArray = mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(inputStr));
			StringBuilder stringBuilder = new StringBuilder();
			byte[] numArray1 = numArray;
			for (int i = 0; i < numArray1.Length; i++)
			{
				byte num = numArray1[i];
				stringBuilder.Append(num.ToString("x").PadLeft(2, '0'));
			}
			return stringBuilder.ToString();
		}
	}
}