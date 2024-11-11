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


using System;
using System.Collections.Generic;
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

		
        /// <summary>
        /// AES 算法加密(ECB模式) 将明文加密，加密后进行base64编码，返回密文
        /// </summary>
        /// <param name="EncryptStr">明文</param>
        /// <param name="Key">密钥</param>
        /// <returns>加密后base64编码的密文</returns>
        public static string AesEncryptorBase64(string EncryptStr, string Key)
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(Key);
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(EncryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// AES 算法解密(ECB模式) 将密文base64解码进行解密，返回明文
        /// </summary>
        /// <param name="DecryptStr">密文</param>
        /// <param name="Key">密钥</param>
        /// <returns>明文</returns>
        public static string AesDecryptorBase64(string DecryptStr, string Key)
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(Key);
                byte[] toEncryptArray = Convert.FromBase64String(DecryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Encoding.UTF8.GetString(resultArray);//  UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///AES 算法加密(ECB模式) 将明文加密，加密后进行Hex编码，返回密文
        /// </summary>
        /// <param name="str">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>加密后Hex编码的密文</returns>
        public static string AesEncryptor_Hex(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = StrToHexByte(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return ToHexString(resultArray);
        }

        /// <summary>
        ///AES 算法解密(ECB模式) 将密文Hex解码后进行解密，返回明文
        /// </summary>
        /// <param name="str">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string AesDecryptor_Hex(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = StrToHexByte(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = StrToHexByte(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        /// byte数组Hex编码
        /// </summary>
        /// <param name="bytes">需要进行编码的byte[]</param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }
        /// <summary> 
        /// 字符串进行Hex解码(Hex.decodeHex())
        /// </summary> 
        /// <param name="hexString">需要进行解码的字符串</param> 
        /// <returns></returns> 
        public static byte[] StrToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        private static  byte[] IV = Encoding.UTF8.GetBytes("1234567812345678");  // 16字节初始化向量

        [Obsolete]
        public static string AESEncrypt(string plainText,string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var encryptKey = StringHelper.SubString(key, 32);
                encryptKey = encryptKey.PadRight(32, ' ');
                aesAlg.Key = Encoding.UTF8.GetBytes(encryptKey);
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
						var mt = msEncrypt.ToArray();
						return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }
        [Obsolete]
        public static string AESDecrypt(string cipherText,string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                var decryptKey = StringHelper.SubString(key, 32);
                decryptKey = decryptKey.PadRight(32, ' ');
                aesAlg.Key = Encoding.UTF8.GetBytes(decryptKey);
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] bytes = Convert.FromBase64String(cipherText);
				 
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        { 
							return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
        [Obsolete]
        public static void SetIV(string iv)
		{
            var _iv = StringHelper.SubString(iv, 32);
            IV = Encoding.UTF8.GetBytes(_iv.PadRight(32, ' '));
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