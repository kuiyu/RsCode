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
        public static  void AddJwt(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    var jwt = JwtHelper.GetJwtInfo();

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
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecurityKey))
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
    }
}
