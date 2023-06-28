RsCode是开箱即用的.net工具库，可高效完成.net项目开发
## ✨ 特性

- 🌈 MIT开源协议，完全免费使用

- 📦 开箱即用

- 💕 集成一些流行的开源框架/库。

- 🎨 集成第三方平台业务API，例：微信开发，第三方支付，第三方存储,抖音相关开发SDK等

## 💿 当前版本

- 正式发布: [![RsCode](https://img.shields.io/nuget/v/RsCode.svg?color=red&style=flat-square)](https://www.nuget.org/packages/RsCode/)

- 开源协议: [![RsCode](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](https://github.com/kuiyu/RsCode/blob/master/LICENSE)

  

## 快速使用

> 推荐使用 Visual Studio 2022 开发。

- 进入应用的项目文件夹，安装 Nuget 包引用

  ```bash
  $ dotnet add package RsCode.AspNetCore --version 2.0.0
  ```

- asp.net core项目引用:

  ```bash
  Install-Package RsCode.AspNetCore -Version 2.0.0
  ```

- 引入[log4net配置](https://rscode.cn/rscode/log.html#%E9%99%84%E5%BD%951)，

- Pragram.cs中添加

  ````csharp
  using PetaPoco;
  using RsCode;
  using RsCode.AspNetCore;
  using AspectCore.Extensions.DependencyInjection;
  
  var builder = WebApplication.CreateBuilder(args);
  //1.添加动态代理
  builder.Host.UseServiceProviderFactory(new DynamicProxyServiceProviderFactory());
  //2.添加RsCode
  builder.Services.AddRsCode();
  //3.自动注册应用接口和实现
  string[] assemblies = new string[] { "your.project.Core", "应用程序集名称" }; //todo 替换成实际业务类程序集名称
  builder.Services.AutoInject(assemblies); 
  //4.添加数据库，以MySql为例
  builder.Services.AddDatabase<MySqlDatabaseProvider>();
  builder.Services.AddUnitOfWork();
  ````
  

​      以上功能包含：API统一消息，工作单元，日志，数据库操作，接口自动注入，异常拦截，模型验证，mediator中间件，内存缓存，id生成器;需要更多的功能，可[查阅文档](https://rscode.cn/rscode/utils.html)



## 🤝 如何贡献

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://gitee.com/kuiyu/RsCode/pulls)

如果你希望参与贡献，欢迎 [Pull Request](https://gitee.com/kuiyu/RsCode/issues)，或给我们 [报告 Bug](https://gitee.com/kuiyu/RsCode/issues) 。

## ❓ 社区互助

如果您在使用的过程中碰到问题，可以通过以下途径寻求帮助，同时我们也鼓励资深用户通过下面的途径给新人提供帮助。
- [gitee](https://gitee.com/kuiyu/RsCode/issues)


- 技术人互动群(微信)  
  <img src="https://www.hnrswl.com/res/static/img/tq.png" width="300" alt="技术赚钱群">
- [![QQ群957285164](https://pub.idqqimg.com/wpa/images/group.png)](https://shang.qq.com/wpa/qunwpa?idkey=f5c24beb6bd16bf59e008df38db80e437763ccf1beb28379dd0ddcfdc94a8a46) [![QQ群244416471](https://pub.idqqimg.com/wpa/images/group.png)](https://qm.qq.com/cgi-bin/qm/qr?k=kbkmTzvTQeBYR1KIyprP5ol4tfMFyOpK&jump_from=webapi)

