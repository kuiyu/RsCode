using System.Threading.Channels;
using System.Threading.Tasks;

namespace RsCode
{
    /// <summary>
    /// 线程通道容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ChannelContainer<T>
    {
        static Channel<T> container;

        public  static Channel<T> GetChannel()
        {
            if (container == null)
            {
                container = Channel.CreateUnbounded<T>();
            }
            return container;
        }

        public static async Task WriteAsync(T t)
        {
            GetChannel();
            await container.Writer.WriteAsync(t);
        }

        public static T Read()
        {
            T t = default(T);
            container.Reader.TryRead(out t);
            return t;
        }
        public static async Task<T> ReadAsync()
        {
            T t = default(T);
            t = await container.Reader.ReadAsync();
            return t;
        }
    }
}
