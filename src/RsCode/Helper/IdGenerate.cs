﻿using System;
using System.Diagnostics;
using System.Threading;

namespace RsCode.Helper
{

    /// <summary>
    /// 类似 Twitter Snowflake(41 + 10 + 12) 算法的 Id 生成器。
    /// 格式：{32 位时间戳, 0-10 位机器码, 0-12 位递增系列号}。
    /// 注意：程序启动前请确保系统时间正确。
    /// </summary>
    public  class IdGenerate
    {
        private readonly long _machineId = 0;
        private readonly byte _machineIdBits = 0;
        private readonly byte _sequenceBits = 0;
        private readonly long _maxSequence = 0;

        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private readonly object _lockObject = new object();

        private long _sequence = 0;
        private long _lastTimestamp = 0;

        /// <summary>
        /// The constructor of <see cref="IdGen"/>.
        /// </summary>
        /// <param name="machineId">当前机器码</param>
        /// <param name="machineIdBits">机器码位数（0-10之间）</param>
        /// <param name="sequenceBits">
        /// 序列号位数（0-20之间）
        /// 注意：
        /// 1. 并发量越大，此值也要越大，例如：10 可以 1 秒内生成 2^10=1024 个 ID。
        /// 2. 每台机器此参数务必相同。
        /// </param>
        public IdGenerate(byte machineId = 0, byte machineIdBits = 0, byte sequenceBits = 10)
        {
            if (sequenceBits > 20)
                throw new ArgumentOutOfRangeException(nameof(sequenceBits), "序列号不能超过 20 位。");

            if (machineIdBits > 10)
                throw new ArgumentOutOfRangeException(nameof(machineIdBits), "机器码不能超过 10 位。");

            _machineIdBits = machineIdBits;
            _sequenceBits = sequenceBits;
            _maxSequence = GetMaxOfBits(_sequenceBits);

            if (machineId > 0)
            {
                var maxMachineId = GetMaxOfBits(machineId);
                if (machineId > maxMachineId)
                    throw new ArgumentOutOfRangeException(nameof(machineId), $"机器码不能大于 {maxMachineId}。");
                _machineId = machineId;
            }
        }

        private long GetTimestampNow()
        {
            // 10000000 = TimeSpan.FromSeconds(1).Ticks
            return (OffsetTicks + _stopwatch.Elapsed.Ticks) / 10000000L;
        }

        private long GetNextTimestamp()
        {
            long timestamp = GetTimestampNow();
            if (timestamp < _lastTimestamp)
                throw new Exception("新的时间戳比旧的小，请检查系统时间。");

            while (timestamp == _lastTimestamp)
            {
                if (_sequence < _maxSequence)
                {
                    _sequence++;
                    return timestamp;
                }
                Thread.Sleep(0); // 降低CPU消耗
                timestamp = GetTimestampNow();
            }
            _sequence = 0;

            return timestamp;
        }

        private  readonly long OffsetTicks =
           DateTime.UtcNow.Ticks - new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

  
 

        /// <summary>
        /// Generate a new sequence id.
        /// </summary>
        /// <returns>The generated id.</returns>
        public  long NewId()
        {
            return NewSequenceId();
        }

        // 获取指定长度二进制的最大整型数。例如：5 返回 000..011111。
        private  long GetMaxOfBits(byte bits)
        {
            return (1L << bits) - 1; // 或 -1 ^ -1 << bits
        }

        // 获取数字的二进制位长度。例如 5 的位长度是 3。
        private  int GetBitsLength(long number)
        {
            return (int)Math.Log(number, 2) + 1;
        }



        /// <summary>
        /// 生成新的ID
        /// </summary>
        /// <returns>ID</returns>
        private long NewSequenceId()
        {
            lock (_lockObject)
            {
                _lastTimestamp = GetNextTimestamp();

                int timestampShift = _machineIdBits + _sequenceBits;
                int machineIdShift = _sequenceBits;
                return (_lastTimestamp << timestampShift) | (_machineId << machineIdShift) | _sequence;
            }
        }
    }



     
}
