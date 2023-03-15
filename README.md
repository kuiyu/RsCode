# RsCode
一款开箱即用的.net工具库，助力.net开发。


## ✨ 特性

- 🌈 基于最新的.net技术。
- 📦 开箱即用的工具库。
- 💕 集成一些流行的开源框架/库。
- 🎨 统一的异常处理
- 🛡 高效的第三方业务库,例：微信开发，微信支付 ，第三方存储等
- ⚙️ 基于 .NET Standard 2.1/.NET 6，可直接引用丰富的 .NET 类库。
- 🎁 可与已有的 ASP.NET Core MVC、Razor Pages 项目无缝集成。


## 🌈 源码托管

- [Gitee](https://github.com/kuiyu/RsCode/)
- [GitHub](https://gitee.com/kuiyu/RsCode/)
- 
## 🖥 支持环境

- .NET Core 3.1以上


## 💿 当前版本

- 正式发布: [![RsCode](https://img.shields.io/nuget/v/RsCode.svg?color=red&style=flat-square)](https://www.nuget.org/packages/RsCode/)



## 📦 安装

- 先安装 [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1?WT.mc_id=DT-MVP-5003987) 3.1.300 以上版本


### 在已有项目中引入 RsCode

- 进入应用的项目文件夹，安装 Nuget 包引用

  ```bash
  $ dotnet add package RsCode --version 1.6.0
  ```

- asp.net core项目引用:

  ```bash
  Install-Package RsCode.AspNetCore -Version 1.6.0
  ```

  > 推荐使用 Visual Studio 2022 开发。



## ⚙️使用

使用RsCode时，必要的项目配置 di,mediatR,异常日志

```csharp
using RsCode;
using RsCode.AspNetCore;
using AspectCore.Configuration;
using AspectCore.Extensions.Hosting;
using AspectCore.Extensions.DataAnnotations;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
//添加di
builder.Host.UseServiceContext(o =>
{
    var p = Predicates.ForService("Rswl.*"); //your project namespace
    o.AddDataAnnotations(p);
});
//添加日志
builder.Host.ConfigureLogging(logging =>
{
    logging.AddLog4Net();
});
//添加mediatR
builder.Services.AddMediatR(typeof(Program).Assembly);
//记录日志
builder.Services.AddExceptionLogging();

//自动注册接口和实现
builder.Services.AutoRegister("myproject.Core"); //your project name
builder.Services.AutoRegister("myproject.Application");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
//异常处理
app.UseErrorHandler();
app.MapControllers();

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


- 技术人互动群(微信)  
  <img src="https://www.hnrswl.com/res/static/img/tq.png" width="300" alt="技术赚钱群">
- [![QQ群957285164](https://pub.idqqimg.com/wpa/images/group.png)](https://shang.qq.com/wpa/qunwpa?idkey=f5c24beb6bd16bf59e008df38db80e437763ccf1beb28379dd0ddcfdc94a8a46) [![QQ群244416471](https://pub.idqqimg.com/wpa/images/group.png)](https://qm.qq.com/cgi-bin/qm/qr?k=kbkmTzvTQeBYR1KIyprP5ol4tfMFyOpK&jump_from=webapi)




## ☀️ 授权协议

[![RsCode](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/kuiyu/RsCode/blob/master/LICENSE)

## 友情链接

[稀缺资源下载](https://pan.rs888.net)   [网络工具箱](https://u.rscode.cn)