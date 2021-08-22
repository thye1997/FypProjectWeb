using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;
using FypProject.ApiViewModel;
using FypProject.Repository;
using static FypProject.Config.SystemData;
using FypProject.Utils;
using FypProject.ViewModel;
using FypProject.Models.DBContext;

namespace FypProject.Services
{
    public class NotificationService
    {
        private readonly IGenericRepository<Notification> notificationRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IGenericRepository<AccountProfile> accProfileRepository;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Account> accRepository;
        private readonly FirebaseNotificationHelper firebase;
        private readonly TwilioHelper twilio;
        private readonly IGenericRepository<Reminder> reminderRepository;

        public NotificationService(IGenericRepository<Notification> notificationRepository,
            IAppointmentRepository appointmentRepository,
            IGenericRepository<AccountProfile> accProfileRepository,
            IGenericRepository<Account> accRepository,
            IGenericRepository<User> userRepository,
            FirebaseNotificationHelper firebase,
            TwilioHelper twilio,
            IGenericRepository<Reminder> reminderRepository
            )
        {
            this.notificationRepository = notificationRepository;
            this.appointmentRepository = appointmentRepository;
            this.accProfileRepository = accProfileRepository;
            this.userRepository = userRepository;
            this.accRepository = accRepository;
            this.firebase = firebase;
            this.twilio = twilio;
            this.reminderRepository = reminderRepository;
        }
        public List<NotificationListResponse> GetNotificationList()
        {
            List<NotificationListResponse> list = new List<NotificationListResponse>();
            var result = notificationRepository.List().ToList();
            if (result != null)
            {
                foreach(var n in result)
                {
                    list.Add(new NotificationListResponse
                    {   Id = n.Id,
                        Title = n.Title,
                        Body = n.Content,
                        Date = n.createdOn
                    });
                }
                return list;
            }
            else
            {
                return list;
            }
        }
        public List<ReminderListViewModel> GetReminderList(int accId)
        {
            var accProfile = accProfileRepository.List().Where(c => c.accountId == accId).ToList();
            List<ReminderListViewModel> reminderLists = new List<ReminderListViewModel>();
            List<int> userIdList = new List<int>();
            foreach(var n in accProfile)
            {
                userIdList.Add(n.userId);
            }
            if (accProfile.Count > 0)
            {
                var reminderList = reminderRepository.List().Where(c => userIdList.Contains(c.userId)).ToList();
                foreach(var n in reminderList)
                {
                    reminderLists.Add(new ReminderListViewModel
                    {
                        content = n.Content,
                    });
                }
         
            }
            return reminderLists;
        }
        public List<Appointment> ReminderList()
        {
            var apptList = appointmentRepository.List().Where(c=>c.Status == (int)AppointmentStatus.Confirmed).ToList();
            List<Appointment> reminderList = new List<Appointment>();
            if (apptList.Count > 0)
            {
                foreach(var n in apptList)
                {
                    if (TimeSlotHelper.ReturnOneDayBefore(n.Date))
                    {
                        reminderList.Add(n);
                    }
                }
                return reminderList;
            }
            else
            {
                return reminderList;
            }
        }
        public List<ReminderPushNotificationViewModel> PushNotificationReminderList()
        {
            var reminderList = ReminderList();
            List<ReminderPushNotificationViewModel> notificationReminder = new List<ReminderPushNotificationViewModel>();
            if (reminderList.Count>0)
            {  foreach(var n in reminderList)
                {   
                    var accountBindList = accProfileRepository.List().Where(c => c.userId == n.userId).ToList();
                    if (accountBindList.Count >0)
                    {
                        foreach(var q in accountBindList)
                        {
                            var accountList = accRepository.List().Where(c => c.Id == q.accountId).ToList();
                            if (accountList.Count > 0)
                            {
                                foreach (var a in accountList)
                                {
                                    if (a.AppointmentPushReminderEnabled && !string.IsNullOrEmpty(a.FirebaseToken))
                                    {
                                        notificationReminder.Add(new ReminderPushNotificationViewModel
                                        {
                                            Id = a.Id,
                                            userId = n.userId,
                                            date = n.Date,
                                            slot = $"{n.StartTime}-{n.EndTime}",
                                            title = "Appointment Reminder",
                                            FirebaseToken = a.FirebaseToken,
                                            sendDate = DateTime.Now.ToString("dd/MM/yyyy")
                                        });
                                    }
                                }
                            }
                        }
                        
                       
                    }
                }

                return notificationReminder;
            }
            else
            {
                return notificationReminder;
            }
        }
        public List<ReminderSMSNotificationViewModel> SMSNotificationReminderList()
        {
            var reminderList = ReminderList();
            List<ReminderSMSNotificationViewModel> notificationReminder = new List<ReminderSMSNotificationViewModel>();
            if (reminderList.Count > 0)
            {
                foreach (var n in reminderList)
                {
                    var accountBind = accProfileRepository.List().Where(c => c.userId == n.userId).FirstOrDefault();
                    if (accountBind != null)
                    {
                        var account = accRepository.List().Where(c => c.Id == accountBind.accountId).FirstOrDefault();
                        if (account.AppointmentSMSReminderEnabled)
                        {
                            var profile = userRepository.List().Where(c => c.Id == accountBind.userId).FirstOrDefault();
                            if(profile.PhoneNumber != null)
                            {
                                notificationReminder.Add(new ReminderSMSNotificationViewModel
                                {
                                    userId = n.userId,
                                    date = n.Date,
                                    slot = $"{n.StartTime}-{n.EndTime}",
                                    title = "Appointment Reminder",
                                    phoneNumber = profile.PhoneNumber,
                                    sendDate = DateTime.Now.ToString("dd/MM/yyyy")
                                });
                            }                 
                        }
                    }
                }
                return notificationReminder;
            }
            else
            {
                return notificationReminder;
            }
        }
        public async Task SendPushNotificationReminder()
        {
            var list = PushNotificationReminderList();
           await firebase.SendPushReminder(list);
        }
        public async Task SendSMSReminder()
        {
            var list = SMSNotificationReminderList();
             await twilio.SendSMSReminder(list);
        }
    }
}
