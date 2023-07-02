/*
 * RsCode
 * 
 * RsCode is .net core platform rapid development framework
 * Apache License 2.0
 * 
 * 作者：lrj
 * 
 * 项目己托管于
 * gitee
 * https://gitee.com/rswl/RsCode.git
 * 
 * github
   https://github.com/kuiyu/RsCode.git
 */
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

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
      
        }
    }
}
