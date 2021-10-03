using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class NotificationViewModel : ListViewModel<Notification>
    {
        public override List<Notification> DataList { get; set; }
    }

    public class ReminderNotificationViewModel
    {
        public int Id { set; get; }
        public int userId { set; get; }
        public string title { set; get; }
        public string slot { set; get; }
        public string date { set; get; }
        public string sendDate { set; get; }
    }

    public class ReminderPushNotificationViewModel: ReminderNotificationViewModel
    {
        public string FirebaseToken { set; get; }
    }

    public class ReminderSMSNotificationViewModel: ReminderNotificationViewModel
    {
        public string phoneNumber { set; get; }

    }

    public class ReminderListViewModel
    {
        public string content { set; get; }
    }
}
