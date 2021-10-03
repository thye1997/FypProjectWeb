using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Base;

namespace FypProject.Models
{
    public class Medicine: IBusinessEntity
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { set; get; }
        [Required(ErrorMessage = "Medicine name is required.")]
        public string medName { set; get; }
        public string createdOn { set; get; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
        public string createdBy { set; get; }
        public bool isActive { set; get; } = true;
    }

    public class MedicalPrescriptions : IBusinessEntity
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public int medId { set; get; }
        public int apptId { set; get; }
        [ForeignKey("medId")]
        public Medicine medicine { set; get; }
        [ForeignKey("apptId")]
        public Appointment appointment { set; get; }

    }
    public class MedicalPrescription : IBusinessEntity
    {
        public int Id { set; get; }
        public string Description { set; get; }
        public int medId { set; get; }
        public Medicine medicine { set; get; }
        public int apptId { set; get; }
        public Appointment appointment { set; get; }
        public int userId { set; get; }
        public User user { set; get; }

    }
}
