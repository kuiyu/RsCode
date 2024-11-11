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
        /// AES �㷨����(ECBģʽ) �����ļ��ܣ����ܺ����base64���룬��������
        /// </summary>
        /// <param name="EncryptStr">����</param>
        /// <param name="Key">��Կ</param>
        /// <returns>���ܺ�base64���������</returns>
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
        /// AES �㷨����(ECBģʽ) ������base64������н��ܣ���������
        /// </summary>
        /// <param name="DecryptStr">����</param>
        /// <param name="Key">��Կ</param>
        /// <returns>����</returns>
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
        ///AES �㷨����(ECBģʽ) �����ļ��ܣ����ܺ����Hex���룬��������
        /// </summary>
        /// <param name="str">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>���ܺ�Hex���������</returns>
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
        ///AES �㷨����(ECBģʽ) ������Hex�������н��ܣ���������
        /// </summary>
        /// <param name="str">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
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
        /// byte����Hex����
        /// </summary>
        /// <param name="bytes">��Ҫ���б����byte[]</param>
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
        /// �ַ�������Hex����(Hex.decodeHex())
        /// </summary> 
        /// <param name="hexString">��Ҫ���н�����ַ���</param> 
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


        private static  byte[] IV = Encoding.UTF8.GetBytes("1234567812345678");  // 16�ֽڳ�ʼ������

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