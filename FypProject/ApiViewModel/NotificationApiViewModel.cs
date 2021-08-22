using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ApiViewModel
{
    public class NotificationApiViewModel
    {
    }

    public class NotificationListResponse
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public string Body { set; get; }
        public string Date { set; get; }
    }
}
