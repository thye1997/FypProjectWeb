
using FypProject.Hangfire.Contract;
using FypProject.Hangfire.Services;
using Hangfire;
using System.Configuration;

namespace FypProject.Hangfire
{
    public class Program
    {


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<RandomJobService>();
            builder.Services.AddTransient<RandomJobService>();
            builder.Services.AddScoped<Logging>();
            builder.Services.AddTransient<LoggingService>();

            ConfigureServices(builder, builder.Services);
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.MapControllers();

            app.UseHangfireDashboard("/hangfire");

            app.Run();
        }


        public static void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {   

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Add framework services.
            services.AddMvc();
        }
    }
}
