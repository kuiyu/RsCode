using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rs.DI
{
    public interface IIocHelper:IDisposable
    {
        IContainer Container { get; }
    }


  
    

}
