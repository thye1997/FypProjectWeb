using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FypProject.Models;
using FypProject.Models.DBContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Identity;
namespace FypProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public Action<string> serviceMessage { set; get; }

        private void consoleMessage(string message)
        {
            Debug.WriteLine($"{message} {Environment.NewLine}");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            serviceMessage = consoleMessage;
            services.DependencyInjection(serviceMessage);
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("FypProject")));
            //services.AddIdentity<SystemUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();            
            services.CookiesAuthConfig();
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.Configure<List<Login>>(Configuration.GetSection("Users"));
            services.AddHttpClient();          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
               // app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //var list = new List<string> { "/css", "/js", "/lib", "/favicon.ico" };
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            /*app.UseWhen( //just testing tho
                context => list.Any(s => context.Request.Path.StartsWithSegments(s)),
                appbuilder =>
                appbuilder.UseStaticFiles()
                );*/

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}"); // id with ? mean it is optional
            });
        }
    }
}
