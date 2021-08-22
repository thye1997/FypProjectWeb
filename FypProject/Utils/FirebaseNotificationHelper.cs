using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using FypProject.Models;
using FypProject.ViewModel;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.DependencyInjection;
using Notification = FypProject.Models.Notification;
using FypProject.Repository;


namespace FypProject.Utils
{
    public class FirebaseNotificationHelper
    {
        private readonly FirebaseMessaging messaging;
        private FirebaseApp app;
        private readonly IServiceProvider serviceProvider;
        public FirebaseNotificationHelper(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            app = FirebaseApp.Create(             
                new AppOptions() { Credential = GoogleCredential.FromFile("serviceKey.json") });
            //app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("serviceKey.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            messaging = FirebaseMessaging.GetMessaging(app);            
        }
        private Message CreateNotification(string token, Notification obj)
        {       
            return new Message()
            {
                Token = token,
                Data = new Dictionary<string, string>()
                {
                    { "title", obj.Title },
                    { "body", obj.Content },
                },
            };
        }
        public async Task SendNotifcation(IGenericRepository<Account> genericRepository, Notification obj)
        {
            var tokenList = genericRepository.List().Where(c => c.PushNotificationEnabled).ToList();
            foreach(var n in tokenList)
            {
                if (!string.IsNullOrEmpty(n.FirebaseToken))
                {
                    try
                    {
                        var result = await messaging.SendAsync(CreateNotification(n.FirebaseToken, obj));
                    }
                    catch(Exception e)
                    {   //catch token not registered anymore (probably uninstalled by users)
                        using (var scoped = serviceProvider.CreateScope())
                        {    
                            var accountRepository = scoped.ServiceProvider.GetRequiredService<IGenericRepository<Account>>();
                            var account= accountRepository.List().Where(c => c.Id == n.Id).FirstOrDefault();
                            account.FirebaseToken = "";
                            accountRepository.SaveChanges();
                        };
                    }
                }
            }
        }
        private Message CreateRescheduleNotification(Account viewModel, string Body)
        {
            return new Message()
            {
                Token = viewModel.FirebaseToken,
                Data = new Dictionary<string, string>()
                {
                    { "title", "Appointment Reschedule Notice" },
                    { "body", Body },
                },
            };
        }

        public async Task SendRescheduleNotification(Account viewModel,string content)
        {            
                if (!string.IsNullOrEmpty(viewModel.FirebaseToken))
                {
                    try
                    {
                        var result = await messaging.SendAsync(CreateRescheduleNotification(viewModel, content));
                    }
                    catch (Exception e)
                    {   //catch token not registered anymore (probably uninstalled by users)
                        using (var scoped = serviceProvider.CreateScope())
                        {
                            var accountRepository = scoped.ServiceProvider.GetRequiredService<IGenericRepository<Account>>();
                            var account = accountRepository.List().Where(c => c.Id == viewModel.Id).FirstOrDefault();
                            account.FirebaseToken = "";
                            accountRepository.SaveChanges();
                        };
                    }
                }       
        }

        private Message CreatePushReminder(ReminderPushNotificationViewModel viewModel)
        {
            string content = $"Your scheduled appointment will be on tomorrow {viewModel.slot}";
            using(var scoped = serviceProvider.CreateScope())
            {
                var reminderRepository = scoped.ServiceProvider.GetRequiredService<IGenericRepository<Reminder>>();
                reminderRepository.Add(new Reminder
                {
                    userId = viewModel.userId,
                    Content = content,
                });
            }
   
            return new Message()
            {
                Token = viewModel.FirebaseToken,
                Data = new Dictionary<string, string>()
                {
                    { "title", viewModel.title },
                    { "body", content },
                },
            };
        }
        public async Task SendPushReminder(List<ReminderPushNotificationViewModel> viewModel)
        {
            foreach(var n in viewModel)
            {
                try
                {
                    Debug.WriteLine("Push notification sent");
                    var result = await messaging.SendAsync(CreatePushReminder(n));
                }
                catch (Exception e)
                {   //catch token not registered anymore (probably uninstalled by users)
                    using (var scoped = serviceProvider.CreateScope())
                    {
                        var accountRepository = scoped.ServiceProvider.GetRequiredService<IGenericRepository<Account>>();
                        var account = accountRepository.List().Where(c => c.Id == n.Id).FirstOrDefault();
                        account.FirebaseToken = "";
                        accountRepository.SaveChanges();
                    };
                }
            }
        }
    }
}
