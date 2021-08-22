using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class MedicineViewModel:ListViewModel<Medicine>
    {
        public Medicine medicine { get; set; }
        public override List<Medicine> DataList { get; set; }
    }

    public class MedicineListViewModel
    {
        public int Id { set; get; }
        public string medName { set; get; }
        public string Description { set; get; }
    }

    public class MedicinePrescriptionViewModel
    {
        public int AppointmentId { set; get; }
        public int UserId { set; get; }
        public string result { set; get; }
        public List<MedicineListViewModel> PrescriptionList { set; get; }
    }
}
