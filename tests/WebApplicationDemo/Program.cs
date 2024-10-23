using AspectCore.Extensions.DependencyInjection;
using AspectCore.Extensions.Hosting;
using RsCode.AspNetCore;
using RsCode;
using WebApplicationDemo.Models;
using Microsoft.AspNetCore.Mvc.Razor;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

builder.Services.AddControllers().AddControllersAsServices();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserTestService, UsertTestService>();

builder.Services.AddDatabase(FreeSql.DataType.MySql, "DefaultConnection");
builder.Services.AddDatabase(FreeSql.DataType.Sqlite, "DefaultConnection2", true);
builder.Services.AddUnitOfWork();
builder.Services.AddJsonLocalization(options => options.ResourcesPath = "i18n"); //i18n是自定义的资源文件夹名称
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddRsCode();

//注册插件
builder.Services.AddPlugins();


var app = builder.Build();
app.UseRequestLocalization("zh-CN", "en-US");
// Configure the HTTP request pipeline.

app.UseStaticFiles();
app.UseRouting();
////mvc模式下报错，指定出错后跳转的页面
//app.UseExceptionHandler("/home/error");

app.UseAuthorization();

app.UsePlugins(builder.Environment);
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
