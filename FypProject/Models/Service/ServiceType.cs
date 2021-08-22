using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class ServiceType : IBusinessEntity
    {
        public int Id { set; get; }
        public string TypeName { set; get; }
    }
}
