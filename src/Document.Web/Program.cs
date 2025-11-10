var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// 使用FileServer中间件（包含静态文件+默认文件+目录浏览）
app.UseFileServer(new FileServerOptions
{
    EnableDefaultFiles = true,
    DefaultFilesOptions = { DefaultFileNames = new[] { "index.html" } }
});

app.Run();
