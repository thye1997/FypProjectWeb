using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class Service: IBusinessEntity
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Service name is required.")]
        public string serviceName {set; get;}
        public string createdOn { set; get; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public string createdBy { set; get; }
        public int typeId { set; get; }
        public ServiceType serviceType { set; get; }
        public bool isActive { set; get; } = true;
    }
}
