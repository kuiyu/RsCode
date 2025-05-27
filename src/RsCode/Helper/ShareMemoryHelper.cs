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
using System.IO.MemoryMappedFiles;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RsCode.Helper
{
    public sealed class ShareMemoryHelper : IDisposable
    {
        private readonly MemoryMappedFile _mmf;
        private readonly MemoryMappedViewAccessor _accessor;
        private readonly Mutex _mutex;
        private bool _disposed;

        public long Capacity { get; }

        private ShareMemoryHelper(MemoryMappedFile mmf, Mutex mutex, long capacity)
        {
            _mmf = mmf;
            _mutex = mutex;
            Capacity = capacity;
            _accessor = _mmf.CreateViewAccessor(0, capacity);
        }

        public static ShareMemoryHelper CreateOrOpen(string mapName, long capacity)
        {
            var mmf = MemoryMappedFile.CreateOrOpen(mapName, capacity);
            var mutex = new Mutex(initiallyOwned: false, name: $"{mapName}_Mutex");
            return new ShareMemoryHelper(mmf, mutex, capacity);
        }

        public void Write<T>(T data, long position)where T:unmanaged
        {
            int structSize = Unsafe.SizeOf<T>();
            if (position + structSize > Capacity)
                throw new ArgumentException("Data exceeds available space");

            try
            {
                _mutex.WaitOne();
                _accessor.Write(position, ref data);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
        public void WriteArray<T>(T[] data, long position) where T : unmanaged
        {
            int elementSize = Unsafe.SizeOf<T>();
            if (position + elementSize * data.Length > Capacity)
                throw new ArgumentException("Data exceeds available space");

            try
            {
                _mutex.WaitOne();
                _accessor.WriteArray(position, data, 0, data.Length);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public T ReadStruct<T>(long position) where T : unmanaged
        {
            int structSize = Unsafe.SizeOf<T>();
            if (position + structSize > Capacity)
                throw new ArgumentException("Invalid read range");

            try
            {
                _mutex.WaitOne();
                _accessor.Read(position, out T result);
                return result;
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _accessor.Dispose();
            _mmf.Dispose();
            _mutex.Dispose();
            _disposed = true;
        }
    }
}
