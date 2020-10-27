using AspectCore.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace RsCode.DI
{

    public interface IServiceBase
    {
          string ClassName { get; set; }
    }
    public interface IServiceHelper<T> where T:IServiceBase
    {
        T GetInstance(string name);
    }

    public  class ServiceHelper<T>:IServiceHelper<T> where T : IServiceBase
    {
        [FromServiceContext]
        protected IEnumerable<T> services { get; set; } 

        public T GetInstance(string name)
        { 
            var instance = services.FirstOrDefault(o => o.ClassName == name);
            return instance;
        }
    }


   

}
