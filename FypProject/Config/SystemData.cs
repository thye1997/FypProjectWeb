using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.Config
{
    public static class SystemData
    {
        public static class Cookie
        {
            public const string FypProj = "FypProj";
        }
        public static class PageTitle
        {
            public const string Dashboard = "Dashboard";
            public const string AppointmentList = "Appointment List";
            public const string AppointmentDetail = "Appointment Detail";
            public const string AppointmentSchedule = "Appointment Schedule";
            public const string PatientList = "Patient List";
            public const string PatientDetail = "Patient Detail";
            public const string Notification = "Notification";
            public const string QRCode = "QR Code";
            public const string SystemUser = "System User";
            public const string Medicine = "Medicine";
            public const string Service = "Service";
        }

        public class ControllerName
        {
            public const string Dashboard = "Dashboard";
            public const string Appointment = "Appointment";
            public const string User = "User";
            public const string Notification = "Notification";
            public const string QRCode = "QRCode";
            public const string SystemUser = "SystemUser";
            public const string Medicine = "Medicine";
            public const string Service = "Service";

        }

        public static class ActionName
        {
            public const string UserDetail = "UserDetail";
        }

        public static class View
        {
            public const string UserIndex = "UserIndex";
            public const string ServiceIndex = "ServiceIndex";
            public const string SystemUserIndex = "SystemUserIndex";
            public const string MedicineIndex = "MedicineIndex";
            public const string AppointmentIndex = "AppointmentIndex";
            public const string AppointmentScheduleIndex = "AppointmentScheduleIndex";
            public const string NotificationIndex = "NotificationIndex";
            public const string QRCodeIndex = "QRCodeIndex";
        }

        public static class ViewPagePath
        {
            public const string WorkDayView = "~/VIews/Appointment/WorkDay/_PartialWorkDay.cshtml";
            public const string WorkDayTableView = "~/Views/Appointment/WorkDay/OffDayListPage.cshtml";

            public const string HolidayView = "~/Views/Appointment/Holiday/_PartialHoliday.cshtml";
            public const string HolidayTableView = "~/Views/Appointment/Holiday/HolidayListPage.cshtml";

            public const string TimeSlotView = "~/VIews/Appointment/TimeSlot/_PartialTimeSlot.cshtml";
            public const string TimeSlotTableView = "~/Views/Appointment/TimeSlot/TimeSlotListPage.cshtml";

            public const string SlotDurationView = "~/VIews/Appointment/SlotDuration/_PartialSlotDuration.cshtml";

            public const string ApptListTableView = "~/Views/Appointment/ApptList/ApptListPage.cshtml";
            public const string ApptAddModalView = "~/Views/Appointment/ApptList/AddApptModal.cshtml";
            public const string ApptDetailView = "~/Views/Appointment/ApptList/ApptDetailView.cshtml";

        }


        public static class TimeSlot
        {
            public const string SlotOneStart = "6:00 AM";
            public const string SlotOneEnd = "12:00 PM";

            public const string SlotTwoStart = "12:00 PM";
            public const string SlotTwoEnd = "7:00 PM";

            public const string SlotThreeStart = "7:00 PM";
            public const string SlotThreeEnd = "12:00 AM";

        }

        public static class ResponseStatus
        {
            public const int Success = 0;
            public const int Exist = 1;
            public const int Error = 2;
        }

        public static class Role
        {
            public const string Admin = "Admin";
            public const string Doctor = "Doctor";
            public const string Staff = "Staff";
        }

        public static class PatientType
        {
            public const int NewPatient = 0;
            public const int ExistingPatient = 1;
        }

        public static class ApptAction
        {
            public const int CheckIn = 0;
            public const int Reschedule = 1;
            public const int Cancel = 2;
        }

        public enum AppointmentStatus
        {
            Confirmed = 0,
            [Display(Name = "Check-In")]
            CheckIn = 1,
            [Display(Name = "In Queue")]
            InQueue = 2,
            [Display(Name = "On Going")]
            OnGoing = 3,
            Completed = 4,
            Cancelled = 201,
            [Display(Name = "No Show")]
            NoShow = 301
        }
        public enum AppointmentType{
           WalkIn,
           Schedule
        }

        public static class SMSMessage
        {
            public const string ConfirmedApptMsg = "Your appointment has been confirmed and the details as below:\n";
        }
        public static class DatatableRequest
        {
            public const string draw = "draw";
            public const string start = "start";
            public const string length = "length";
        }


    }
}
