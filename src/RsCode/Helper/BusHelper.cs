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
using System.Collections.Concurrent;
using System.Linq;

namespace RsCode
{
    /// <summary>
    /// 发布与订阅服务
    /// </summary>
    public static class BusHelper
    {
        /// <summary>
        /// 通知总想清单
        /// </summary>
        static ConcurrentDictionary<PublishedInterface, PublishedInterface> PCaches = new ConcurrentDictionary<PublishedInterface, PublishedInterface>();

        public static void Initialize()
        {

        }

        /// <summary>
        /// 订阅消息：订阅者监听数据提供方的信息
        /// </summary>
        /// <param name="subscriber">订阅者</param>
        /// <param name="pi">发布的接口</param>
        /// <param name="callback">回调函数，能快速执行完毕，如果不能一定要使用异步。如果要操作UI，需要使用委托this.Invoke(new MethodInvoker(() =>{}));</param>
        public static void Subscription<T>(IModule subscriber, PublishedInterface<T> pi, Action<T> callback)
        {
            PublishedInterface<T> piChache = (PublishedInterface<T>)PCaches.GetOrAdd(pi, key =>
            {
                return pi;
            });

            piChache.Subscription(subscriber, callback);
        }

        /// <summary>
        /// 移除某个订阅
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="pi"></param>
        public static void SubscriptionRemove<T>(IModule subscriber, PublishedInterface pi)
        {
            PublishedInterface<T> piChache = (PublishedInterface<T>)PCaches.GetOrAdd(pi, key =>
            {
                return pi;
            });
            piChache.SubscriptionRemove(subscriber);
        }

        /// <summary>
        /// 发布消息：数据提供放向订阅者推送数据
        /// </summary>
        /// <param name="pi">发布的接口</param>
        /// <param name="obj">参数</param>
        public static void Publishing<T>(PublishedInterface<T> pi, T obj)
        {
            PublishedInterface<T> piChache = (PublishedInterface<T>)PCaches.GetOrAdd(pi, key =>
            {
                return pi;
            });

            piChache.Publishing(obj);
        }

    }

 
    public abstract class PublishedInterface
    {

    }

    /// <summary>
    /// 发布接口
    /// </summary>
    public class PublishedInterface<T> : PublishedInterface
    {
        ConcurrentDictionary<IModule, Lazy<SubscriberInfo<T>>> SubCaches = new ConcurrentDictionary<IModule, Lazy<SubscriberInfo<T>>>();

        /// <summary>
        /// 订阅消息：订阅者监听数据提供方的信息
        /// </summary>
        /// <param name="subscriber">订阅者</param>
        /// <param name="pi">发布的接口</param>
        /// <param name="callback">回调函数，能快速执行完毕，如果不能一定要使用异步。如果要操作UI，需要使用委托this.Invoke(new MethodInvoker(() =>{}));</param>
        public void Subscription(IModule subscriber, Action<T> callback)
        {
            SubCaches.AddOrUpdate(subscriber, key =>
            {
                return new Lazy<SubscriberInfo<T>>(() =>
                {
                    return new SubscriberInfo<T>() { Subscriber = subscriber, Callback = callback };
                });
            }, (key, value) => { return value; });

            Console.WriteLine($"Add\t{this.ToString()}\t{SubCaches.Count}\t{subscriber.Name}");
        }


        /// <summary>
        /// 移除某个订阅
        /// </summary>
        /// <param name="subscriber"></param>
        /// <param name="pi"></param>
        public void SubscriptionRemove(IModule subscriber)
        {
            Lazy<SubscriberInfo<T>> remove = null;
            SubCaches.TryRemove(subscriber, out remove);

            Console.WriteLine($"Remove\t{this.ToString()}\t{SubCaches.Count}\t{remove?.Value?.Subscriber?.Name}");
        }

        /// <summary>
        /// 发布消息：数据提供放向订阅者推送数据
        /// </summary>
        /// <param name="pi">发布的接口</param>
        /// <param name="obj">参数</param>
        public void Publishing(T obj)
        {
            var subs = SubCaches.ToList();
            for (int i = subs.Count - 1; i >= 0; i--)
            {
                var item = subs[i];
                if (item.Value.Value == null || item.Value.Value.Subscriber.IsDisposed == true)
                {//如果订阅方已经卸载，那么删除此订阅
                    Lazy<SubscriberInfo<T>> remove = null;
                    SubCaches.TryRemove(item.Key, out remove);
                    continue;
                }
                item.Value.Value.Callback(obj);//调用订阅
            }

        }

    }


    /// <summary>
    /// 订阅者的信息及回调函数
    /// </summary>
    public class SubscriberInfo<T>
    {
        /// <summary>
        /// 订阅模块
        /// </summary>
        public IModule Subscriber { get; set; }

        /// <summary>
        /// 回调函数
        /// </summary>
        public Action<T> Callback { get; set; }
    }

    public interface IModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 模块标题
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 模块是否已经卸载
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 模块在面板关闭后是否被消灭
        /// </summary>
        bool DontDestroy { get; set; }

        T ExecCmd<T>(string cmd, params object[] args);

    }
}

