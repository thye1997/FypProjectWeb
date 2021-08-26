using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CookieAuthConfig
    {

        public static IServiceCollection CookiesAuthConfig(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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
