﻿using System;
using System.Threading;

namespace RsCode
{
    public class DisposeAction : IDisposable
    {
        public static readonly DisposeAction Empty = new DisposeAction(null);

        private Action _action;

        public DisposeAction(Action action)
        {
            _action = action;
        }
        public void Dispose()
        {
            var action = Interlocked.Exchange(ref _action, null);
            action?.Invoke();
        }
    }
}
