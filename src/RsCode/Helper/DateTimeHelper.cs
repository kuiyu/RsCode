/*
 * 项目：.net开发基础工具类
 * 作者：河南软商网络科技有限公司
 * * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using System.Text;

namespace System
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateTimeHelper
    {
        public static DateTime GetStartDayOfWeeks(int year, int month, int index)
        {
            DateTime minValue;
            if (!(year < 1600 ? false : year <= 9999))
            {
                minValue = DateTime.MinValue;
            }
            else if (!(month < 0 ? false : month <= 12))
            {
                minValue = DateTime.MinValue;
            }
            else if (index >= 1)
            {
                DateTime dateTime = new DateTime(year, month, 1);
                int num = 7;
                if (Convert.ToInt32(dateTime.DayOfWeek.ToString("d")) > 0)
                {
                    num = Convert.ToInt32(dateTime.DayOfWeek.ToString("d"));
                }
                DateTime dateTime1 = dateTime.AddDays(1 - num);
                DateTime dateTime2 = dateTime1.AddDays(index * 7);
                minValue = ((dateTime2 - dateTime.AddMonths(1)).Days <= 0 ? dateTime2 : DateTime.MinValue);
            }
            else
            {
                minValue = DateTime.MinValue;
            }
            return minValue;
        }

        public static string GetWeekSpanOfMonth(int year, int month)
        {
            string str;
            if (!(year < 1600 ? false : year <= 9999))
            {
                str = "";
            }
            else if ((month < 0 ? false : month <= 12))
            {
                StringBuilder stringBuilder = new StringBuilder();
                int num = 1;
                while (num < 5)
                {
                    DateTime dateTime = new DateTime(year, month, 1);
                    int num1 = 7;
                    if (Convert.ToInt32(dateTime.DayOfWeek.ToString("d")) > 0)
                    {
                        num1 = Convert.ToInt32(dateTime.DayOfWeek.ToString("d"));
                    }
                    DateTime dateTime1 = dateTime.AddDays(1 - num1);
                    DateTime dateTime2 = dateTime1.AddDays(num * 7);
                    if ((dateTime2 - dateTime.AddMonths(1)).Days <= 0)
                    {
                        stringBuilder.Append(dateTime2.ToString("yyyy-MM-dd"));
                        stringBuilder.Append(" ~ ");
                        DateTime dateTime3 = dateTime2.AddDays(6);
                        stringBuilder.Append(dateTime3.ToString("yyyy-MM-dd"));
                        stringBuilder.Append(Environment.NewLine);
                        num++;
                    }
                    else
                    {
                        str = "";
                        return str;
                    }
                }
                str = stringBuilder.ToString();
            }
            else
            {
                str = "";
            }
            return str;
        }


        /// <summary>
        /// 时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTimeByJavascript(long jsTimeStamp)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            DateTime dt = startTime.AddMilliseconds(jsTimeStamp);
            return dt;
        }

        /// <summary>
        /// 把时间转换为javascript所使用的时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ConvertDateTimeByJavascript(DateTime dt)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);// 当地时区
            long timeStamp = (long)(dt - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }

        /// <summary>
        /// 把时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long ConvertDateTimeByUnix(System.DateTime time)
        {
            
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            long timeStamp = (long)(DateTime.Now - startTime).TotalSeconds; // 相差秒数
            return timeStamp;
        }

        /// <summary>
        /// 把Unix时间戳转化为时间
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime GetTimeByUnix(long unixTimeStamp)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            DateTime dt = startTime.AddSeconds(unixTimeStamp);
            return dt;
        }

        /// <summary>
		/// 获取以0点0分0秒开始的日期
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static DateTime GetStartDateTime(DateTime d)
        {
            if (d.Hour != 0)
            {
                var year = d.Year;
                var month = d.Month;
                var day = d.Day;
                var hour = "0";
                var minute = "0";
                var second = "0";
                d = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
            }
            return d;
        }

        /// <summary>
        /// 获取以23点59分59秒结束的日期
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(DateTime d)
        {
            if (d.Hour != 23)
            {
                var year = d.Year;
                var month = d.Month;
                var day = d.Day;
                var hour = "23";
                var minute = "59";
                var second = "59";
                d = Convert.ToDateTime(string.Format("{0}-{1}-{2} {3}:{4}:{5}", year, month, day, hour, minute, second));
            }
            return d;
        }

        /// <summary>
        /// 获取指定时间的当月第一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayByMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// 获取指时间的当月最后一天
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetEndDayByMonth(DateTime dt)
        {
            var first = GetFirstDayByMonth(dt);
            return first.AddMonths(1).AddDays(-1);
        }
    }
}
