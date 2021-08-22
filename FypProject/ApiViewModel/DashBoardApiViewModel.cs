using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ApiViewModel
{
    public class DashBoardApiViewModel
    {
        public int upcomingCount { set; get; }
        public int pastCount { set; get; }
        public int noShowCount { set; get; }
        public AppointmentData apptData { set; get; }
    }
}
