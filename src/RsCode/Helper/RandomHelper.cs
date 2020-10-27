using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsCode.Helper
{
    public class RandomHelper
    {
        /// <summary>
        /// 正态随机分配额度
        /// </summary>
        /// <param name="totalAmount">要分配的额度</param>
        /// <param name="count">总份数</param>
        /// <returns></returns>
        public static List<double> RandAmount(double totalAmount, int count)
        {
            //人均最小额度  
            double min = 0.1;
            if (totalAmount < count * min)
                return null;

            int num = count;
            List<double> array = new List<double>();
            Random r = new Random();
            for (int i = 0; i < num; i++)
            {
                if (count == 1)
                {
                    count--;
                    array.Add(Convert.ToDouble(totalAmount.ToString("0.00")));
                    // Console.WriteLine(string.Format("第{0}个红包：{1}元", i + 1, Convert.ToDouble(remainMoney.ToString("0.00"))));
                }
                else
                {
                    //(remainMoney - (remainCount - 1) * min)：保存剩余金额可以足够的去分配剩余的红包数量  
                    double max = (totalAmount - (count - 1) * min) / count * 2;
                    double money = r.NextDouble() * max;
                    money = Convert.ToDouble((money <= min ? min : money).ToString("0.00"));
                    count--;
                    totalAmount -= money;
                    array.Add(money);
                    // Console.WriteLine(string.Format("第{0}个红包：{1}元", i + 1, money));
                }
            }
            //再次随机  
            return array.OrderBy(o => Guid.NewGuid()).ToList();
        }

        static readonly Random Random = new Random(~unchecked((int)DateTime.Now.Ticks));

        /// <summary>
        /// Generates the check code with unique number.
        /// </summary>
        /// <returns>The check code number.</returns>
        /// <param name="codeCount">Code count. Max 10</param>
        public string GenerateCheckCodeNum(int codeCount)
        {
            codeCount = codeCount > 10 ? 10 : codeCount;   // unable to return unique number list longer than 10

            int[] arrInt = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            arrInt = arrInt.OrderBy(c => Guid.NewGuid()).ToArray<int>();// make the array in random order

            string str = string.Empty;

            for (int i = 0; i < codeCount; i++)
            {
                str += arrInt[i];
            }
            return str;
        }

         
        /// <summary>
        /// 随机生成字符串（数字和字母混和）
        /// </summary>
        /// <returns>The check code.</returns>
        /// <param name="CodeCount">Code lenght.</param>
        public string GenerateCheckCode(int CodeCount)
        {
            char[] MixedList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; //remove I & O
            return GetRandomCode(MixedList, CodeCount);
        }

        #region
        /// <summary>
        /// Gets the random code.
        /// </summary>
        /// <returns>The random code.</returns>
        /// <param name="CharList">All char want to generate.</param>
        /// <param name="CodeLength">Code lenght.</param>
        private string GetRandomCode(char[] CharList, int CodeLength)
        {
            string result = string.Empty;
            for (int i = 0; i < CodeLength; i++)
            {
                int rnd = Random.Next(0, CharList.Length);
                result += CharList[rnd];
            }
            return result;
        }
        void test()
        {
            
        }
        #endregion
    }
}
