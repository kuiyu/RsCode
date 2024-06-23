using AspectCore.Extensions.DependencyInjection;
using RsCode.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddControllersAsServices();

builder.Services.AddRsCode();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
////mvc模式下报错，指定出错后跳转的页面
app.UseExceptionHandler("/home/error");

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
