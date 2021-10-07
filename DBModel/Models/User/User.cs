using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class User: IBusinessEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string NRIC { get; set; }
        public string PhoneNumber { get; set; }
        public List<MedicalHistory> medicalHistory { set; get; }
        public List<Appointment> appointment { set; get; }
        public List<MedicalPrescription> medicalPrescription { set; get; }
        public List<AccountProfile> accountProfile { set; get; }


    }


}
