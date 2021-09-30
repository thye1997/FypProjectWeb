using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ApiViewModel
{
    public class AppointmentData
    {
        public int AccId { set; get; }
        public int ApptId { set; get; }
        public string FullName { set; get; }
        public string Gender { set; get; }
        public string NRIC { set; get; }
        public string PhoneNumber { set; get; }
        public string DOB { set; get; }
        public int [] ApptStatus { set; get; }
        public int ApptStatusInt { set; get; }
        public string ApptStatusString { set; get; }
        public string Date { set; get; }
        public string Slot { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string Service { set; get; }
        public string Note { set; get; }
        public bool IsCheckIn { set; get; }
        public bool IsAction { set; get; }
    }


    public class AppointmentApiScheduleViewModel
    {
        public List<SpecialHoliday> SpHolidayList { set; get; }
        public List<int> OffDay { set; get; }
        public List<Service> ServiceList { set; get; }

    }

    public class SpecificSlotApiViewModel
    {
        public string Date { set; get; }
        public int Slot { set; get; }
    }

    public class RescheduleAppointmentApiViewModel
    {
        public int ActionType { set; get; }
        public int ApptId { set; get; }
        public string StartTime { set; get; }
        public string Date { set; get; }
    }

    public class AddAppointmentApiViewModel
    {
        public int AccId { set; get; }
        public string Date { set; get; }
        public string StartTime { set; get; }
        public int ServiceId { set; get; }
        public string Note { set; get; }

    }

    public class CheckInAppointmentApiViewModel
    {
        public int ApptId { set; get; }
        public string UniqueString { set; get; }
    }
}
