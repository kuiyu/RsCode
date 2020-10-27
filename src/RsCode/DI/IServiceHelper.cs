using System;
using System.Collections.Generic;
using System.Text;

namespace RsCode.DI
{
    public interface IServiceHelper
    {
        T GetInstance<T>(string name);
    }
}
