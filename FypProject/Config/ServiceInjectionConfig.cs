using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Models;
using FypProject.Repository;
using FypProject.Services;
using FypProject.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtension
    {
        public static IServiceCollection DependencyInjection(this IServiceCollection services, Action<string> message =null)
        {
            message("-----dependency injection beginning...............");
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<UserService>();
            services.AddScoped<AccountService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<DashboardService>();
            services.AddScoped<MedicalHistoryService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //services.AddScoped<ReminderService>();
            services.AddHostedService<ReminderService>();
            services.AddSingleton<FirebaseNotificationHelper>();
            services.AddSingleton<TwilioHelper>();
            message("dependecy injection completed.");
            return services;
        }


    }
}
