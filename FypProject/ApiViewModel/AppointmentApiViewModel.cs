using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ApiViewModel
{
    public class AppointmentData
    {
        public int accId { set; get; }
        public int apptId { set; get; }
        public string FullName { set; get; }
        public string Gender { set; get; }
        public string NRIC { set; get; }
        public string PhoneNumber { set; get; }
        public string DOB { set; get; }
        public int [] apptStatus { set; get; }
        public int apptStatusInt { set; get; }
        public string apptStatusString { set; get; }
        public string date { set; get; }
        public string Slot { set; get; }
        public string startTime { set; get; }
        public string endTime { set; get; }
        public string service { set; get; }
        public string note { set; get; }
        public bool isCheckIn { set; get; }
        public bool isAction { set; get; }
    }


    public class AppointmentApiScheduleViewModel
    {
        public List<SpecialHoliday> spHolidayList { set; get; }
        public List<int> offDay { set; get; }
        public List<Service> serviceList { set; get; }

    }

    public class SpecificSlotApiViewModel
    {
        public string date { set; get; }
        public int slot { set; get; }
    }

    public class RescheduleAppointmentApiViewModel
    {
        public int actionType { set; get; }
        public int apptId { set; get; }
        public string startTime { set; get; }
        public string date { set; get; }
    }

    public class AddAppointmentApiViewModel
    {
        public int accId { set; get; }
        public string date { set; get; }
        public string startTime { set; get; }
        public int serviceId { set; get; }
        public string note { set; get; }

    }

    public class CheckInAppointmentApiViewModel
    {
        public int apptId { set; get; }
        public string uniqueString { set; get; }
    }
}
