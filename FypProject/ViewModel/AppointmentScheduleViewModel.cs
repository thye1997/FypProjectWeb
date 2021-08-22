using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class AppointmentScheduleViewModel
    {
      public List<OffDay> offDayList { set; get; }
      public List<TimeSlot> timeSlotList { set; get; }
      public List<SpecialHoliday> spHolidayList { set; get; }
      
    }
}
