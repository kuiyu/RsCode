using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace RsCode.AspNetCore
{
    public  static class JwtExtensions
    {
        public static  void AddJwt(this IServiceCollection services, string Url = "/UserAuthHub")
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var jwt = JwtHelper.GetJwtInfo();

                    if (jwt.Expire < 0)
                        jwt.Expire = 60*24*360 ;

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

                    ////signalr auth
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments(Url)))
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
