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

using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace RsCode
{
    /// <summary>
    /// 线程通道容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class ChannelHelper<T>
    {
        static Channel<T> currentChannel=null;
        /// <summary>
        /// 获取通道
        /// </summary>
        /// <param name="capacity">通道可存储的最大条目数。</param>
        /// <returns></returns>
        public static Channel<T> GetChannel(int capacity = 0)
        {
            var channel = CallContext<Channel<T>>.GetData("rscode.channel");
            if (channel == null)
            {
                if (capacity <= 0)
                {
                    channel = Channel.CreateUnbounded<T>();
                }
                else
                {
                    channel = Channel.CreateBounded<T>(capacity);
                }
                CallContext<Channel<T>>.SetData("rscode.channel", channel);
            }
            currentChannel = channel;
            return channel;
        }

        public static List<T>ReadAllData()
        {
            if(currentChannel==null)
            {
                throw new Exception("please call GetChannel()");
            }
            List<T> result = new List<T>();
            T t = default(T);
            while (currentChannel.Reader.TryRead(out t))
            {
                result.Add(t);
            }
            return result;
        }

        public static bool TryRead()
        {
            if (currentChannel == null)
            {
                throw new Exception("please call GetChannel()");
            }
            T t = default(T);
            return currentChannel.Reader.TryRead(out t);
          
        }

        public static  bool TryWrite(T t)
        {
            if (currentChannel == null)
            {
                throw new Exception("please call GetChannel()");
            }
            return currentChannel.Writer.TryWrite(t);
           
        }

        
       
    }
}
