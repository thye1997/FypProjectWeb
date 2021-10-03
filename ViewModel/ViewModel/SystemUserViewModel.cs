using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class SystemUserViewModel : ListViewModel<SystemUser>
    {
        public SystemUser systemUser { set; get; }
        public override List<SystemUser> DataList { get; set; }
    }
}
