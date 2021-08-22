using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class QRCode: IBusinessEntity
    {
        public int Id { set; get; }
        public string FileName { set; get; }
        public string UniqueString { set; get; }
        public string createdOn { set; get; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public string createdBy { set; get; }
        public bool isActive { set; get; }


    }
}
