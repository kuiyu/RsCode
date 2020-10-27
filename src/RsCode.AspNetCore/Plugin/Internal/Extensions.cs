using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace RsCode.AspNetCore.Plugin.Internal
{
    internal static class Extensions
    {
        // https://github.com/simplcommerce/SimplCommerce/blob/master/src/SimplCommerce.WebHost/Extensions/ServiceCollectionExtensions.cs
        public static void AddApplicationPart(this IMvcBuilder mvcBuilder, Assembly assembly)
        {
            var partFactory = ApplicationPartFactory.GetApplicationPartFactory(assembly);
            foreach (var part in partFactory.GetApplicationParts(assembly))
                mvcBuilder.PartManager.ApplicationParts.Add(part);

            var relatedAssemblies = RelatedAssemblyAttribute.GetRelatedAssemblies(assembly, false);
            foreach (var relatedAssembly in relatedAssemblies)
            {
                partFactory = ApplicationPartFactory.GetApplicationPartFactory(relatedAssembly);
                foreach (var part in partFactory.GetApplicationParts(relatedAssembly))
                    mvcBuilder.PartManager.ApplicationParts.Add(part);
            }
        }
    }
}
