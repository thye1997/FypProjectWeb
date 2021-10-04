using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    public class CookieValidateMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieValidateMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var contextUrl = context.Request.Path;
            if (contextUrl.StartsWithSegments("/Account/Login")) // do not let user go back to login page if authenticated already
            {
                    if (context.Request.Cookies.ContainsKey(SystemData.Cookie.FypProj))
                    {
                    //Debug.WriteLine($"Url path {contextUrl}");
                    //TODO: validate if user cookie is validate here https://stackoverflow.com/questions/46274422/user-doesnt-seem-authenticated-in-the-pipline-insideuse-in-dotnetcore-2-0
                    context.Response.Redirect("/");
                    }
                    else
                    {
                    await _next(context);
                    }           
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCookieValidate(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CookieValidateMiddleware>();
        }
    }
}
