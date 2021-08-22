using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class Appointment : IBusinessEntity
    {
        public int Id { get; set; }
        public int ApptType { get; set; }
        public int serviceId { get; set; }
        public Service service { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RequestTime { get; set; }
        public string Result { get; set; }
        public int Status { get; set; }
        public int userId { set; get; }
        public User user { set; get; }
        public List<MedicalPrescriptions> medicalPrescription { set; get; }
        public bool isActive { get; set; } = true;
        public int doctorId { set; get; }
        public SystemUser systemUser { set; get; }
        
    }

    public class OffDay: IBusinessEntity
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public bool isOffDay { get; set; } = false;
    }

    public class SpecialHoliday: IBusinessEntity
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }

    public class TimeSlot : IBusinessEntity
    {
        public int Id { get; set; }
        public string Slot { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        
    }
    public class SlotDuration: IBusinessEntity
    {
        public int Id { get; set; }
        public int slotDuration { get; set; }
        public bool isActive { get; set; }
    }

}
