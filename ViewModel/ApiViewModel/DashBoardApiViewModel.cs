using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ApiViewModel
{
    public class DashBoardApiViewModel
    {
        public int UpcomingCount { set; get; }
        public int PastCount { set; get; }
        public int NoShowCount { set; get; }
        public AppointmentData ApptData { set; get; }
    }
}
