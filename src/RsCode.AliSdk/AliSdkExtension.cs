using Microsoft.Extensions.DependencyInjection;

namespace RsCode.AliSdk
{
    public static class AliSdkExtension
    {
        public static void AddAliSdk(this IServiceCollection services)
        {
            services.AddScoped<ITranslate, Translate>();
            services.AddScoped<IOcr, Ocr>();
            services.AddScoped<IImageSegment, ImageSegment>();
            services.AddScoped<IImageAudit, ImageAudit>();
            services.AddScoped<IImageEnhance, ImageEnhance>();
            services.AddScoped<IFacebody, Facebody>();
            services.AddScoped<IVideo,Video>();
            services.AddScoped<IDoc, Doc>();
        }
    }
}
