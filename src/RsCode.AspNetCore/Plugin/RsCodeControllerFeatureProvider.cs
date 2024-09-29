using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace RsCode.AspNetCore.Plugin
{
    public class RsCodeControllerFeatureProvider:ControllerFeatureProvider
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var assembly = Assembly.GetEntryAssembly();
            var assemblies = assembly.GetReferencedAssemblies();
            assemblies.Append<AssemblyName>(assembly.GetName());
            assemblies.ToList().ForEach(assembly =>
            {
                var assTemp = Assembly.Load(assembly);
                assTemp.GetTypes().ToList().ForEach(type =>
                {
                    //如果实现了IRomteService则认为是一个控制器
                    if (!IsController(type.GetTypeInfo()))
                    {
                        return;
                    }
                    feature.Controllers.Add(type.GetTypeInfo());
                });
            });
        }


        protected override bool IsController(TypeInfo typeInfo)
        {
            // 自定义控制器判断逻辑，例如检查类型是否继承自特定的基类等
            if (typeInfo.IsClass && !typeInfo.IsAbstract && typeInfo.IsSubclassOf(typeof(ControllerBase)))
            {
                return true;
            }
            return false;
        }
    }
}
