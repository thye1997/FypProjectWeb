using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Models.DBContext;
using FypProject.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace FypProject.Services
{
    public class ReminderService : BaseService
    {
        private readonly IServiceProvider serviceProvider;
        public ReminderService(IServiceProvider serviceProvider) :
            //base(@"0 0 * * *", TimeZoneInfo.Local)
            base(@"* * * * * *", TimeZoneInfo.Local)
        {
            this.serviceProvider = serviceProvider;
        }
       
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("This job is running");
          await base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            Debug.WriteLine("This job is running again");   
            using (var scope = serviceProvider.CreateScope())
            {
              await  Task.Run(() =>
                {
                    for (var i = 0; i < 50; i++)
                    {
                        Debug.WriteLine($"Count in the do work {i}");

                    }
                });  
               // var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
               //await notificationService.SendPushNotificationReminder();
              // await notificationService.SendSMSReminder();
            }
           // return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
