﻿/*
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
using SkiaSharp;
using System;
using System.Threading.Tasks;

namespace RsCode
{
    public sealed class VerifyCodeHelper
    {
        #region 单例模式
        //创建私有化静态obj锁  
        private static readonly object _ObjLock = new object();
        //创建私有静态字段，接收类的实例化对象  
        private static VerifyCodeHelper _VerifyCodeHelper = null;
        //构造函数私有化  
        private VerifyCodeHelper() { }
        //创建单利对象资源并返回  
        public static VerifyCodeHelper GetSingleObj()
        {
            if (_VerifyCodeHelper == null)
            {
                lock (_ObjLock)
                {
                    if (_VerifyCodeHelper == null)
                        _VerifyCodeHelper = new VerifyCodeHelper();
                }
            }
            return _VerifyCodeHelper;
        }
        #endregion

        #region 生产验证码
        public enum VerifyCodeType { NumberVerifyCode, AbcVerifyCode, MixVerifyCode };

        /// <summary>
        /// 1.数字验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string CreateNumberVerifyCode(int length)
        {
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值  
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字  
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字  
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码  
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 2.字母验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private string CreateAbcVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>
        /// 3.混合验证码
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        private string CreateMixVerifyCode(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
            };
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                verification[i] = dictionary[random.Next(dictionary.Length - 1)];
            }
            return new string(verification);
        }

        /// <summary>
        /// 产生验证码（随机产生4-6位）
        /// </summary>
        /// <param name="type">验证码类型：数字，字符，符合</param>
        /// <returns></returns>
        public async Task<string> CreateVerifyCodeAsync(VerifyCodeType type)
        {
            string verifyCode = string.Empty;
            Random random = new Random();
            int length = random.Next(4, 6);
            switch (type)
            {
                case VerifyCodeType.NumberVerifyCode:
                    verifyCode =await Task.Run(()=> GetSingleObj().CreateNumberVerifyCode(length));
                    break;
                case VerifyCodeType.AbcVerifyCode:
                    verifyCode =await Task.Run(()=> GetSingleObj().CreateAbcVerifyCode(length));
                    break;
                case VerifyCodeType.MixVerifyCode:
                    verifyCode =await Task.Run(()=> GetSingleObj().CreateMixVerifyCode(length));
                    break;
            }
            return verifyCode;
        }
        #endregion

        #region 验证码图片
        ///// <summary>
        ///// 验证码图片 => Bitmap
        ///// </summary>
        ///// <param name="verifyCode">验证码</param>
        ///// <param name="width">宽</param>
        ///// <param name="height">高</param>
        ///// <returns>Bitmap</returns>
        //public Bitmap CreateBitmapByImgVerifyCode(string verifyCode, int width, int height)
        //{ 

        //        Font font = new Font("Arial", 14, (FontStyle.Bold | FontStyle.Italic));
        //    Brush brush;
        //    Bitmap bitmap = new Bitmap(width, height);
        //    Graphics g = Graphics.FromImage(bitmap);
        //    SizeF totalSizeF = g.MeasureString(verifyCode, font);
        //    SizeF curCharSizeF;
        //    PointF startPointF = new PointF(0, (height - totalSizeF.Height) / 2);
        //    Random random = new Random(); //随机数产生器
        //    g.Clear(Color.White); //清空图片背景色  
        //    for (int i = 0; i < verifyCode.Length; i++)
        //    {
        //        brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
        //        g.DrawString(verifyCode[i].ToString(), font, brush, startPointF);
        //        curCharSizeF = g.MeasureString(verifyCode[i].ToString(), font);
        //        startPointF.X += curCharSizeF.Width;
        //    }

        //    //画图片的干扰线  
        //    for (int i = 0; i < 10; i++)
        //    {
        //        int x1 = random.Next(bitmap.Width);
        //        int x2 = random.Next(bitmap.Width);
        //        int y1 = random.Next(bitmap.Height);
        //        int y2 = random.Next(bitmap.Height);
        //        g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        //    }

        //    //画图片的前景干扰点  
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = random.Next(bitmap.Width);
        //        int y = random.Next(bitmap.Height);
        //        bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
        //    }

        //    g.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1); //画图片的边框线  
        //    g.Dispose();
        //    return bitmap;
        //}

        /// <summary>
        /// 验证码图片 => byte[]
        /// </summary>
        /// <param name="verifyCode">验证码</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <returns>byte[]</returns>
        public byte[] CreateByteByImgVerifyCode(string verifyCode, int width, int height)
        { 
            using (SKBitmap bitmap = new SKBitmap(width, height))
            {
                using(SKCanvas canvas = new SKCanvas(bitmap))
                {
                    //设置背景色
                    canvas.Clear(SKColors.White);
                    Random random = new Random();
                    //设置画笔
                    using (SKPaint textPaint=new SKPaint())
                    {
                        // 在画布上绘制文本
                        textPaint.TextSize = 40;
                        textPaint.Color = SKColors.Blue;
                        textPaint.FakeBoldText = true;
                        textPaint.IsAntialias = true;
                        textPaint.StrokeWidth = 2;
                        textPaint.TextAlign = SKTextAlign.Center;
                        canvas.DrawText(verifyCode, width / 2, height / 2+15, textPaint);
                    }
                    //添加干扰线
                    for (int i = 0; i < 5; i++)
                    {
                        using (SKPaint linePaint = new SKPaint())
                        {
                            linePaint.Color = GetRandomColor(random);
                            linePaint.StrokeWidth = 2;
                            canvas.DrawLine(random.Next(width), random.Next(height), random.Next(width), random.Next(height), linePaint);
                        }
                    }

                    // 添加干扰点
                    for (int i = 0; i < 50; i++)
                    {
                        using (SKPaint pointPaint = new SKPaint())
                        {
                            pointPaint.Color = GetRandomColor(random);
                            canvas.DrawPoint(random.Next(width), random.Next(height), pointPaint);
                        }
                    }
                }

                // 将 SKBitmap 保存为图片文件
                using (SKImage image = SKImage.FromBitmap(bitmap))
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    //System.IO.File.WriteAllBytes("output.png", data.ToArray());
                    return data.ToArray();
                }
            }

        }

        private static SKColor GetRandomColor(Random random)
        {
            return new SKColor((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));
        }
        #endregion
    }

}
