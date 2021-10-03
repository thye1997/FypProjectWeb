using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class ServiceViewModel : ListViewModel<Service>
    {
        public Service service { get; set; }
        public override List<Service> DataList { get; set; }
    }
}
