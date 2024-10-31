using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RsCode.Coze.Core;

namespace RsCode.Coze
{
    public static class CozeServiceExtensions
    {
        public static void AddCoze(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var Configuration = provider.GetService<IConfiguration>();
            services.Configure<List<CozeAppConfig>>(options =>Configuration.GetSection("ByteDance:Coze").Bind(options));

            services.AddMemoryCache();
            services.AddSingleton<CozeManager>();
            services.AddSingleton<BotService>();
            services.AddSingleton<ChatService>();
            services.AddSingleton<ConversationService>();
            services.AddSingleton<FilesService>();
            services.AddSingleton<KnowledgeService>();
            services.AddSingleton<MessageService>();
           
        }
    }

 
}
