using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class Reminder : IBusinessEntity
    {
        public int Id { set; get; }
        public int userId { set; get; }
        public User user { set; get; }
        public string Content { set; get; }
        public string sendDate { set; get; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
    }
}
