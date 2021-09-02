using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CookieAuthConfig
    {

        public static IServiceCollection CookiesAuthConfig(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddCookie(options =>
                {
                    options.Events = new CookieAuthenticationEvents()
                    {
                        OnSigningIn = context =>
                        {
                            //context.Properties.IsPersistent = true;
                            Debug.WriteLine("hehe user signning in");                       
                            return Task.CompletedTask;
                        },
                        OnSignedIn = context =>
                        {
                            //var header =
                            var header = context.Response.Headers.AsEnumerable();
                           context.Response.Headers.Add("My-Custom-Response-Headers", "Yeet");
                            foreach(var head in header)
                            {
                                Debug.WriteLine($"header value of request => {head} {Environment.NewLine}");
                            }
                            return Task.CompletedTask;
                        },
                        OnSigningOut = context =>
                        {
                            Debug.WriteLine("Yeet, user is signning out");
                            return Task.CompletedTask;

                        }



                    };
                    options.Cookie.Name = "FypProj";
                    options.LoginPath = "/Account/Login";
                    //options.ExpireTimeSpan = new TimeSpan(0,0,5);
                });
            /*services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "FypProj";
                options.LoginPath = "/Account/Login";
                //options.ExpireTimeSpan = new TimeSpan(0,0,5);
            });
            /* .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options => {
                 options.RequireHttpsMetadata = false;
             });*/

            return services;
        }
    }
}
