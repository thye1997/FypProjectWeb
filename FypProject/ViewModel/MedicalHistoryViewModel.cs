using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class MedicalHistoryViewModel : ListViewModel<MedicalHistory>
    {
        public override List<MedicalHistory> DataList { get; set; }
    }


    public class MedicalHistoryListViewModel
    {
        public int Id { set; get; }
        public string Date { set; get; }
        public string Slot { set; get; }
        public string Service { set; get; }
        public string Result { set; get; }
        public string FormattedMedicalPrescription { set; get; }
        public string DoctorName { set; get; }
    }
}
