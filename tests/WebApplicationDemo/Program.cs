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
builder.Services.AddJsonLocalization(options => options.ResourcesPath = "i18n"); //i18n���Զ������Դ�ļ�������
builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

builder.Services.AddRsCode();

//ע����
builder.Services.AddPlugins();


var app = builder.Build();
app.UseRequestLocalization("zh-CN", "en-US");
// Configure the HTTP request pipeline.

app.UseStaticFiles();
app.UseRouting();
////mvcģʽ�±���ָ���������ת��ҳ��
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
