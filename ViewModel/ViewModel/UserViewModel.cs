using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class UserViewModel : ListViewModel<User>
    {
        public User user { set; get; }
        public override List<User> DataList { get; set; }
        //public MedicalHistory medHistory { get; set; }
        public string Description { set; get; }
        public int userId { set; get; }
    }
}
