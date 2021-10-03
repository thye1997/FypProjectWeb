using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class Account : IBusinessEntity
    {
        public int Id { set; get; }
        public string EmailAddress { set; get; }
        public string Password { set; get; }
        public string FirebaseToken { set; get; }
        public bool PushNotificationEnabled { set; get; }
        public bool AppointmentPushReminderEnabled { set; get; }
        public bool AppointmentSMSReminderEnabled { set; get; }
        public bool isActive { set; get; } = true;
        public List<AccountProfile> accountProfile { set; get; }
    }

    public class AccountProfile : IBusinessEntity
    {
        public int Id { set; get; }
        public int accountId { set; get; }
        public Account account { set; get; }
        public int userId { set; get; }
        public User user { set; get; }  
        public string Relationship { set; get; }
        public bool isDefault { set; get; }

    }
}
