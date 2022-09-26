using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RsCode.Storage.Aliyun;

public static class AliyunStorageExtensions
{
    public static void AddAliyunStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAliyunOssService, AliyunOssService>();

        services.AddScoped<AliyunStorageOptions>();

        services.Configure<AliyunStorageOptions>(configuration.GetSection("aliyun:oss"));

    }
}

