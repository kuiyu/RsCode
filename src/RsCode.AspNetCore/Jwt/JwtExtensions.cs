/*
 * RsCode
 * 
 * RsCode是快速开发.net应用的工具库,其丰富的功能和易用性，能够显著提高.net开发的效率和质量。
 * 协议：MIT License
 * 作者：runsoft1024
 * 微信：runsoft1024
 * 文档 https://rscode.cn/
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * github
   https://github.com/kuiyu/RsCode.git

 */

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RsCode.AspNetCore.Jwt;

namespace RsCode.AspNetCore
{
    public  static class JwtExtensions
    {
        /// <summary>
        /// 添加jwt服务，使用非对称加密
        /// </summary>
        /// <param name="services"></param>
        public static  void AddJwt(this IServiceCollection services)
        {
            JwtHelper.IsRsa = false;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var jwt = JwtHelper.GetJwtInfo();
                    var privateKey = jwt.SecurityKey;
                    if (jwt.Expire < 0)
                        jwt.Expire = 60*24*360 ;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证发行者
                        ValidateAudience = true,//是否验证接收者

                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromMinutes(jwt.Expire),
                        ValidateIssuerSigningKey = true,//是否验证安全key
                        ValidAudience = jwt.Audience,//有效的接收者
                        ValidIssuer = jwt.Issuer,//有效的发行者 
                        IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey))
                    };

                    //auth
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        { 
                            if(context.Request.Query.TryGetValue("access_token", out var accessToken))
                            {
								context.Token = accessToken;
							}
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<JwtHelper>();
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<JwtInfo>(options =>
            {
                configuration.GetSection("Jwt").Bind(options);
            });
        }
        /// <summary>
        /// 添加jwt服务 ，使用非对称加密
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwtBearer(this IServiceCollection services)
        {
            JwtHelper.IsRsa = true;
            var  publicKey = JwtHelper.GetPublicKey();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var jwt = JwtHelper.GetJwtInfo();
                     
                    if (jwt.Expire < 0)
                        jwt.Expire = 60 * 24 * 360;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证发行者
                        ValidateAudience = true,//是否验证接收者
                        ValidateLifetime = true,//是否验证失效时间
                        ValidateIssuerSigningKey = true,//是否验证安全key
                        ValidAudience = jwt.Audience,//有效的接收者
                        ValidIssuer = jwt.Issuer,//有效的发行者 
                        IssuerSigningKeys =new List<SecurityKey> { publicKey },
                        ClockSkew = TimeSpan.FromMinutes(jwt.Expire),
                        RequireSignedTokens = false,
                        // 忽略 kid 检查
                        ValidateTokenReplay = false,
                        ValidateActor = false,
                        RequireExpirationTime = true,
                        SaveSigninToken = false,
                        RoleClaimType = "role",
                        NameClaimType = "name",
                        SignatureValidator = null,
                        TokenDecryptionKey = null
                    };

                    //auth
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.TryGetValue("access_token", out var accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddScoped<JwtHelper>();
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            services.Configure<JwtInfo>(options =>
            {
                configuration.GetSection("Jwt").Bind(options);
            });
        }
    }
}
