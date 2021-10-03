using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;
using Microsoft.AspNetCore.Identity;

namespace FypProject.Models
{
    public class SystemUser: IBusinessEntity
    {
        public int Id { set; get; }
        public string userName {set; get;}
        public string Name { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public string createdOn { set; get; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public string createdBy { set; get; }
        public List<Appointment> appointments { set; get; }
    
    }
}
