using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Reflection;


namespace RsCode.AspNetCore.Plugin
{
    public interface IReferenceLoader
    {
        public void LoadStreamsIntoContext(PluginLoadContext context, string moduleFolder, Assembly assembly);
    }

    public class DefaultReferenceLoader : IReferenceLoader
    {
        private IReferenceContainer _referenceContainer = null;
        private readonly ILogger<DefaultReferenceLoader> _logger = null;

        public DefaultReferenceLoader(IReferenceContainer referenceContainer, ILogger<DefaultReferenceLoader> logger)
        {
            _referenceContainer = referenceContainer;
            _logger = logger;
        }

        public void LoadStreamsIntoContext(PluginLoadContext context, string moduleFolder, Assembly assembly)
        {
            var references = assembly.GetReferencedAssemblies();

            foreach (var item in references)
            {
                var name = item.Name;

                var version = item.Version.ToString();

                var stream = _referenceContainer.GetStream(name, version);

                if (stream != null)
                {
                    _logger.LogDebug($"Found the cached reference '{name}' v.{version}");
                    context.LoadFromStream(stream);
                }
                else
                {

                    if (IsSharedFreamwork(name))
                    {
                        continue;
                    }

                    var dllName = $"{name}.dll";
                    var filePath = $"{ Path.Combine(  moduleFolder,dllName)}";

                    if (!File.Exists(filePath))
                    {
                        _logger.LogWarning($"The package '{dllName}' is missing.");
                        continue;
                    }

                    using (var fs = new FileStream(filePath, FileMode.Open))
                    {
                        var referenceAssembly = context.LoadFromStream(fs);

                        var memoryStream = new MemoryStream();

                        fs.Position = 0;
                        fs.CopyTo(memoryStream);
                        fs.Position = 0;
                        memoryStream.Position = 0;
                        _referenceContainer.SaveStream(name, version, memoryStream);

                        LoadStreamsIntoContext(context, moduleFolder, referenceAssembly);
                    }
                }
            }
        }

        private bool IsSharedFreamwork(string name)
        {
            if(name.StartsWith("Microsoft") 
                || name.StartsWith("System")
                || name.StartsWith("api-ms-win-core")
                || name.StartsWith("clrcompression")
                || name.StartsWith("clretwrc")
                || name.StartsWith("clrjit")
                || name.StartsWith("coreclr")
                || name.StartsWith("hostpolicy")
                || name.StartsWith("mscordaccore")
                || name.StartsWith("mscor")
                || name.StartsWith("netstandard")
                || name.StartsWith("ucrtbase")
                || name.StartsWith("WindowsBase")
                || name.StartsWith("aspnetcorev2_inprocess")
                || name.StartsWith("RsCode")

                )
            {
                return true;
            }
            return false;
            
        }
    }
    public class DefaultReferenceContainer : IReferenceContainer
    {
        private static Dictionary<CachedReferenceItemKey, Stream> _cachedReferences = new Dictionary<CachedReferenceItemKey, Stream>();


        public List<CachedReferenceItemKey> GetAll()
        {
            return _cachedReferences.Keys.ToList();
        }

        public bool Exist(string name, string version)
        {
            return _cachedReferences.Keys.Any(p => p.ReferenceName == name
                && p.Version == version);
        }

        public void SaveStream(string name, string version, Stream stream)
        {
            if (Exist(name, version))
            {
                return;
            }


            _cachedReferences.Add(new CachedReferenceItemKey { ReferenceName = name, Version = version }, stream);
        }

        public Stream GetStream(string name, string version)
        {
            var key = _cachedReferences.Keys.FirstOrDefault(p => p.ReferenceName == name
                && p.Version == version);

            if (key != null)
            {
                _cachedReferences[key].Position = 0;
                return _cachedReferences[key];
            }

            return null;
        }
    }
    public interface IReferenceContainer
    {
        List<CachedReferenceItemKey> GetAll();

        bool Exist(string name, string version);

        void SaveStream(string name, string version, Stream stream);

        Stream GetStream(string name, string version);
    }

    public class CachedReferenceItemKey
    {
        public string ReferenceName { get; set; }

        public string Version { get; set; }
    }








    public static class ServiceExtensionss
    {
        //编写WebApplication扩展方法，获取ApplicationPartManager并将自定义的IApplicationFeatureProvider添加进去
        public static void AddCustomController(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var applicationPartManager = services.GetRequiredService<ApplicationPartManager>();
                if (applicationPartManager == null)
                {
                    throw new Exception("未在容器中找到ApplicationPartManager");
                }
                applicationPartManager.FeatureProviders.Add(new RsCodeControllerFeatureProvider());
            }
        }

    }


    //public class CustomApplicationModelConvention : IApplicationModelConvention
    //{
    //    public void Apply(ApplicationModel application)
    //    {
    //        foreach (var controller in application.Controllers)
    //        {
    //            if (controller.ControllerType.IsAssignableTo(typeof(IRomteService)))
    //            {
    //                ConfigureApiExplorer(controller);
    //                ConfigureSelector(controller);
    //                ConfigureParameters(controller);
    //            }
    //        }
    //    }
    //    //直接抄abp代码
    //    private void ConfigureParameters(ControllerModel controller)
    //    {
    //        foreach (var action in controller.Actions)
    //        {
    //            foreach (var prm in action.Parameters)
    //            {
    //                if (prm.BindingInfo != null)
    //                {
    //                    continue;
    //                }

    //                if (!TypeHelper.IsPrimitiveExtended(prm.ParameterInfo.ParameterType, includeEnums: true))
    //                {
    //                    if (CanUseFormBodyBinding(action, prm))
    //                    {
    //                        prm.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    private bool CanUseFormBodyBinding(ActionModel action, ParameterModel parameter)
    //    {
    //        //如果参数名为id则默认为query param
    //        if (parameter.ParameterName == "id")
    //        {
    //            return false;
    //        }

    //        foreach (var selector in action.Selectors)
    //        {
    //            if (selector.ActionConstraints == null)
    //            {
    //                continue;
    //            }

    //            foreach (var actionConstraint in selector.ActionConstraints)
    //            {
    //                var httpMethodActionConstraint = actionConstraint as HttpMethodActionConstraint;
    //                if (httpMethodActionConstraint == null)
    //                {
    //                    continue;
    //                }

    //                if (httpMethodActionConstraint.HttpMethods.All(hm => hm.IsIn("GET", "DELETE", "TRACE", "HEAD")))
    //                {
    //                    return false;
    //                }
    //            }
    //        }
    //        return true;
    //    }

    //    private void ConfigureSelector(ControllerModel controller)
    //    {
    //        //先不使用特性，做个最简单的
    //        var controllerName = controller.ControllerName;
    //        foreach (var action in controller.Actions)
    //        {
    //            action.Selectors.Clear();
    //            var reqMethod = GetRequestMethod(action);
    //            action.Selectors.Add(new SelectorModel
    //            {
    //                AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"/{controllerName}/{action.ActionName}")),
    //                ActionConstraints = { new HttpMethodActionConstraint(new[] { reqMethod }) }
    //            });
    //        }
    //    }

    //    private string GetRequestMethod(ActionModel action)
    //    {
    //        var reqM = action.Attributes.OfType<RequestMethodAttribute>().ToList();
    //        if (reqM.Count == 0)
    //        {
    //            return RequestMethods.GET.ToString();
    //        }
    //        return reqM.Last()._method.ToString();
    //    }

    //    private void ConfigureApiExplorer(ControllerModel controller)
    //    {
    //        controller.ApiExplorer.IsVisible = true;
    //        foreach (var action in controller.Actions)
    //        {
    //            ConfigureApiExplorer(action);
    //        }
    //    }

    //    private void ConfigureApiExplorer(ActionModel action)
    //    {
    //        action.ApiExplorer.IsVisible = true;
    //    }
    //}




}
