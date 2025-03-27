using Microsoft.Extensions.DependencyInjection;


namespace RsCode.AliSdk
{
    public static class AliSdkExtension
    {
        public static void AddAliSdk(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddSingleton<ITranslate, Translate>();
            services.AddSingleton<IOcr, Ocr>();
            services.AddSingleton<IImageSegment, ImageSegment>();
            services.AddSingleton<IImageAudit, ImageAudit>();
            services.AddSingleton<IImageEnhance, ImageEnhance>();
            services.AddSingleton<IFacebody, Facebody>();
            services.AddSingleton<IVideo,Video>();
            services.AddSingleton<IDoc, Doc>();
        }
    }
}
