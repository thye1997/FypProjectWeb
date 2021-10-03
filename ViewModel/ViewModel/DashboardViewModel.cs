using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class DashboardSummaryViewModel
    {
        public int totalAppointment { get; set; }
        public int todayAppointment { get; set; }
        public int noShowAppointment { get; set; }

    }

    public class DashboardViewModel : ListViewModel<Dashboard>
    {
        public override List<Dashboard> DataList { get; set; }
        public int totalUpcoming { get; set; }
        public int totalPast { get; set; }
        public int totalNoShow { get; set; }
    }

  
}
