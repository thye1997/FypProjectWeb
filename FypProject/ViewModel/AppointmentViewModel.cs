using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class AppointmentViewModel : ListViewModel<Appointment>
    {
        public override List<Appointment> DataList { set; get; }

        public int Id { set; get; }
        public int userId { set; get; }
        public string ApptType { set; get; }
        public string FullName { set; get; }
        public string Date { set; get; }
        public string Slot { set; get; }
        public string Service { set; get; }
        public string StartTime { set; get; }
        public string EndTime {set; get; }
        public string NRIC { set; get; }
        public string PhoneNumber { set; get;}
        public string Status { set; get; }
        public bool checkIn { set; get; } = false;

    }

    public class AppointmentDetailViewModel
    {

        public int Id { set; get; }
        public int UserId { set; get; }
        public string ApptType { set; get; }
        public string FullName { set; get; }
        public string Gender { set; get; }
        public string NRIC { set; get; }
        public List<AppointmentMedicalPrescriptionViewModel> medicalPrescriptions { set; get; }
        public string PhoneNumber { set; get; }
        public string DOB { set; get; }
        public string Date { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public string Slot { set; get; } 
        public string Service { set; get; }
        public int Status { set; get; }
        public string StatusString { set; get; }
        public string Note { set; get; }
        public string Result { set; get; }
        public bool isCheckIn { set; get; }

    }

    public class AppointmentMedicalPrescriptionViewModel
    {
        public string medType { set; get; }
        public string medName { set; get; }
        public string Description { set; get; }
    }

    
}
