RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量

## ✨ 特性

- 🌈 MIT开源协议，完全免费使用

- 📦 开箱即用

- 💕 集成一些流行的开源框架/库。

- 🎨 集成第三方平台业务API，例：微信开发，第三方支付，第三方存储,抖音相关开发SDK等

  

## 🌈 源码托管

- [Gitee](https://github.com/kuiyu/RsCode/)
- [GitHub](https://gitee.com/kuiyu/RsCode/)

## 🖥 支持环境

- .NET Core 6.0以上


## 💿 当前版本

- 正式发布: [![RsCode](https://img.shields.io/nuget/v/RsCode.svg?color=red&style=flat-square)](https://www.nuget.org/packages/RsCode/)



## 📦 使用RsCode

- 先安装 [.NET Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 6.0 以上版本

### 在已有项目中引入 RsCode

- 进入应用的项目文件夹，安装 Nuget 包引用

  ```bash
  $ dotnet add package RsCode --version 2.2.3
  ```

- asp.net core项目引用:

  ```bash
  Install-Package RsCode.AspNetCore -Version 2.2.3
  ```

  > 推荐使用 Visual Studio 2022 开发

在Program.cs中

```csharp
using RsCode;
using RsCode.AspNetCore;
using AspectCore.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());

builder.Services.AddControllers().AddControllersAsServices();

//添加RsCode
builder.Services.AddRsCode();
//自动注册应用接口和实现
string[] assemblies = new string[] { "your.project.Core", "应用程序集名称" }; //todo 替换成实际业务类程序集名称
builder.Services.AutoInject(assemblies); 
//添加数据库，以MySql为例
builder.Services.AddDatabase(FreeSql.DataType.MySql, "DefaultConnection");
//添加unitofwork
builder.Services.AddUnitOfWork();
//添加插件支持
builder.Services.AddPlugins();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
	//启用swaggerui api文档
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

//异常处理
app.UseErrorHandler();

//添加插件支持
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
```

## 🔗 文档链接

- [文档主页](https://rscode.cn)
- [微软官方教程](https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-6.0)



## 🤝 如何贡献

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://gitee.com/kuiyu/RsCode/pulls)

如果你希望参与贡献，欢迎 [Pull Request](https://gitee.com/kuiyu/RsCode/issues)，或给我们 [报告 Bug](https://gitee.com/kuiyu/RsCode/issues) 。

## ❓ 社区互助

如果您在使用的过程中碰到问题，可以通过以下途径寻求帮助，同时我们也鼓励资深用户通过下面的途径给新人提供帮助。
- [gitee](https://gitee.com/kuiyu/RsCode/issues)


- 作者微信：runsoft1024



## ☀️ 授权协议

[![RsCode](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/kuiyu/RsCode/blob/master/LICENSE)

