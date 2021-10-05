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
        private readonly IGenericRepository<Notification> _notificationRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IGenericRepository<AccountProfile> _accProfileRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Account> _accRepository;
        private readonly FirebaseNotificationHelper _firebaseHelper;
        private readonly TwilioHelper _twilioHelper;
        private readonly IGenericRepository<Reminder> _reminderRepository;

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
            _notificationRepository = notificationRepository;
            _appointmentRepository = appointmentRepository;
            _accProfileRepository = accProfileRepository;
            _userRepository = userRepository;
            _accRepository = accRepository;
            _firebaseHelper = firebase;
            _twilioHelper = twilio;
            _reminderRepository = reminderRepository;
        }
        public List<NotificationListResponse> GetNotificationList()
        {
            List<NotificationListResponse> list = new List<NotificationListResponse>();
            var result = _notificationRepository.ToQueryable().ToList();
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
            var accProfileId = _accProfileRepository.Where(c => c.accountId == accId).Select(c=>c.userId).ToList();
            List<ReminderListViewModel> reminderLists = new List<ReminderListViewModel>();

            if (accProfileId.Count > 0)
            {
                reminderLists = _reminderRepository.Where(c => accProfileId.Contains(c.userId))
                .Select(c=> new ReminderListViewModel 
                { 
                    content = c.Content
                
                }).ToList();      
            }

            return reminderLists;
        }
        public List<Appointment> ReminderList()
        {
            var apptList = _appointmentRepository.Where(c=>c.Status == (int)AppointmentStatus.Confirmed).ToList();
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
                    var accountBindList = _accProfileRepository.Where(c => c.userId == n.userId).ToList();
                    if (accountBindList.Count >0)
                    {
                        foreach(var q in accountBindList)
                        {
                            var accountList = _accRepository.Where(c => c.Id == q.accountId).ToList();
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
                    var accountBind = _accProfileRepository.Where(c => c.userId == n.userId).FirstOrDefault();
                    if (accountBind != null)
                    {
                        var account = _accRepository.Where(c => c.Id == accountBind.accountId).FirstOrDefault();
                        if (account.AppointmentSMSReminderEnabled)
                        {
                            var profile = _userRepository.Where(c => c.Id == accountBind.userId).FirstOrDefault();
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
           await _firebaseHelper.SendPushReminderAsync(list);
        }
        public async Task SendSMSReminder()
        {
            var list = SMSNotificationReminderList();
             await _twilioHelper.SendSMSReminderAsync(list);
        }
    }
}
