using RsCode.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddRsCode();

//ע����
builder.Services.AddPlugins();


var app = builder.Build();

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
