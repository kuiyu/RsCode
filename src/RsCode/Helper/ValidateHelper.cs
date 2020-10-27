using System;
using System.Text.RegularExpressions;

namespace RsCode.Helper
{
	public class ValidateHelper
	{
		private static Regex _emailregex;

		private static Regex _mobileregex;

		private static Regex _phoneregex;

		private static Regex _ipregex;

		private static Regex _dateregex;

		private static Regex _numericregex;

		private static Regex _zipcoderegex;

		static ValidateHelper()
		{
			ValidateHelper._emailregex = new Regex("^([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\\.][a-z]{2,3}([\\.][a-z]{2})?$", RegexOptions.IgnoreCase);
			ValidateHelper._mobileregex = new Regex("^(13|15|18|14)[0-9]{9}$");
			ValidateHelper._phoneregex = new Regex("^(\\d{3,4}-?)?\\d{7,8}$");
			ValidateHelper._ipregex = new Regex("^(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d{1,2}|1\\d\\d|2[0-4]\\d|25[0-5])$");
			ValidateHelper._dateregex = new Regex("(\\d{4})-(\\d{1,2})-(\\d{1,2})");
			ValidateHelper._numericregex = new Regex("^[-]?[0-9]+(\\.[0-9]+)?$");
			ValidateHelper._zipcoderegex = new Regex("^\\d{6}$");
		}

		public ValidateHelper()
		{
		}

		public static bool BetweenPeriod(string[] periodList, out string liePeriod)
		{
			bool flag;
			if ((periodList == null ? false : periodList.Length > 0))
			{
				DateTime now = DateTime.Now;
				DateTime date = now.Date;
				string[] strArrays = periodList;
				for (int i = 0; i < strArrays.Length; i++)
				{
					string str = strArrays[i];
					int num = str.IndexOf("-");
					DateTime dateTime = TypeHelper.StringToDateTime(str.Substring(0, num));
					DateTime dateTime1 = TypeHelper.StringToDateTime(str.Substring(num + 1));
					if (dateTime < dateTime1)
					{
						if ((now <= dateTime ? false : now < dateTime1))
						{
							liePeriod = str;
							flag = true;
							return flag;
						}
					}
					else if ((!(now > dateTime) || !(now < date.AddDays(1)) ? now < dateTime1 : true))
					{
						liePeriod = str;
						flag = true;
						return flag;
					}
				}
			}
			liePeriod = string.Empty;
			flag = false;
			return flag;
		}

		public static bool BetweenPeriod(string periodStr, out string liePeriod)
		{
			string[] strArrays = StringHelper.SplitString(periodStr, "\n");
			return ValidateHelper.BetweenPeriod(strArrays, out liePeriod);
		}

		public static bool BetweenPeriod(string periodList)
		{
			string empty = string.Empty;
			return ValidateHelper.BetweenPeriod(periodList, out empty);
		}

		private static bool CheckIDCard15(string Id)
		{
			bool flag;
			long num = 0;
			if (!(!long.TryParse(Id, out num) ? false : num >= Math.Pow(10, 14)))
			{
				flag = false;
			}
			else if ("11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91".IndexOf(Id.Remove(2)) != -1)
			{
				string str = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
				DateTime dateTime = new DateTime();
				flag = (DateTime.TryParse(str, out dateTime) ? true : false);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		private static bool CheckIDCard18(string Id)
		{
			bool flag;
			long num = 0;
			if (!(!long.TryParse(Id.Remove(17), out num) || num < Math.Pow(10, 16) ? false : long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out num)))
			{
				flag = false;
			}
			else if ("11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91".IndexOf(Id.Remove(2)) != -1)
			{
				string str = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
				DateTime dateTime = new DateTime();
				if (DateTime.TryParse(str, out dateTime))
				{
					char[] chrArray = new char[] { ',' };
					string[] strArrays = "1,0,x,9,8,7,6,5,4,3,2".Split(chrArray);
					chrArray = new char[] { ',' };
					string[] strArrays1 = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(chrArray);
					char[] charArray = Id.Remove(17).ToCharArray();
					int num1 = 0;
					for (int i = 0; i < 17; i++)
					{
						num1 = num1 + int.Parse(strArrays1[i]) * int.Parse(charArray[i].ToString());
					}
					int num2 = -1;
					Math.DivRem(num1, 11, out num2);
					flag = (!(strArrays[num2] != Id.Substring(17, 1).ToLower()) ? true : false);
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static bool InIP(string sourceIP, string targetIP)
		{
			bool flag;
			if ((string.IsNullOrEmpty(sourceIP) ? false : !string.IsNullOrEmpty(targetIP)))
			{
				string[] strArrays = StringHelper.SplitString(sourceIP, ".");
				string[] strArrays1 = StringHelper.SplitString(targetIP, ".");
				int length = strArrays.Length;
				int num = 0;
				while (num < length)
				{
					if (strArrays1[num] == "*")
					{
						flag = true;
						return flag;
					}
					else if (strArrays[num] != strArrays1[num])
					{
						flag = false;
						return flag;
					}
					else if (num != 3)
					{
						num++;
					}
					else
					{
						flag = true;
						return flag;
					}
				}
				flag = false;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static bool InIPList(string sourceIP, string[] targetIPList)
		{
			bool flag;
			if ((targetIPList == null ? false : targetIPList.Length > 0))
			{
				string[] strArrays = targetIPList;
				int num = 0;
				while (num < strArrays.Length)
				{
					if (!ValidateHelper.InIP(sourceIP, strArrays[num]))
					{
						num++;
					}
					else
					{
						flag = true;
						return flag;
					}
				}
			}
			flag = false;
			return flag;
		}

		public static bool InIPList(string sourceIP, string targetIPStr)
		{
			string[] strArrays = StringHelper.SplitString(targetIPStr, "\n");
			return ValidateHelper.InIPList(sourceIP, strArrays);
		}

		public static bool IsDate(string s)
		{
			return ValidateHelper._dateregex.IsMatch(s);
		}

		public static bool IsEmail(string s)
		{
			bool flag;
			flag = (!string.IsNullOrEmpty(s) ? ValidateHelper._emailregex.IsMatch(s) : true);
			return flag;
		}

		public static bool IsIdCard(string id)
		{
			bool flag;
			if (string.IsNullOrEmpty(id))
			{
				flag = true;
			}
			else if (id.Length != 18)
			{
				flag = (id.Length != 15 ? false : ValidateHelper.CheckIDCard15(id));
			}
			else
			{
				flag = ValidateHelper.CheckIDCard18(id);
			}
			return flag;
		}

		public static bool IsImgFileName(string fileName)
		{
			bool flag;
			if (fileName.IndexOf(".") != -1)
			{
				string lower = fileName.Trim().ToLower();
				string str = lower.Substring(lower.LastIndexOf("."));
				flag = (str == ".png" || str == ".bmp" || str == ".jpg" || str == ".jpeg" ? true : str == ".gif");
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public static bool IsIP(string s)
		{
			return ValidateHelper._ipregex.IsMatch(s);
		}

		public static bool IsMobile(string s)
		{
			bool flag;
			flag = (!string.IsNullOrEmpty(s) ? ValidateHelper._mobileregex.IsMatch(s) : true);
			return flag;
		}

		public static bool IsNumeric(string numericStr)
		{
			return ValidateHelper._numericregex.IsMatch(numericStr);
		}

		public static bool IsNumericArray(string[] numericStrList)
		{
			bool flag;
			if ((numericStrList == null ? true : numericStrList.Length <= 0))
			{
				flag = false;
			}
			else
			{
				string[] strArrays = numericStrList;
				int num = 0;
				while (num < strArrays.Length)
				{
					if (ValidateHelper.IsNumeric(strArrays[num]))
					{
						num++;
					}
					else
					{
						flag = false;
						return flag;
					}
				}
				flag = true;
			}
			return flag;
		}

		public static bool IsNumericRule(string numericRuleStr, string splitChar)
		{
			return ValidateHelper.IsNumericArray(StringHelper.SplitString(numericRuleStr, splitChar));
		}

		public static bool IsNumericRule(string numericRuleStr)
		{
			return ValidateHelper.IsNumericRule(numericRuleStr, ",");
		}

		public static bool IsPhone(string s)
		{
			bool flag;
			flag = (!string.IsNullOrEmpty(s) ? ValidateHelper._phoneregex.IsMatch(s) : true);
			return flag;
		}

		public static bool IsZipCode(string s)
		{
			bool flag;
			flag = (!string.IsNullOrEmpty(s) ? ValidateHelper._zipcoderegex.IsMatch(s) : true);
			return flag;
		}
	}
}