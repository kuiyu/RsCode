using RsCode.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddRsCode();

//注册插件
builder.Services.AddPlugins();


var app = builder.Build();

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
