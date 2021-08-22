using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class MedicalHistory: IBusinessEntity
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public int userId { set; get; }
        public User user { set; get; }

    }
}
