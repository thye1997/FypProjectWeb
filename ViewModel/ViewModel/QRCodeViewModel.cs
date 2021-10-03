using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;

namespace FypProject.ViewModel
{
    public class QRCodeViewModel : ListViewModel<QRCode>
    {
        public override List<QRCode> DataList { get; set; }
        public int Id { set; get; }
        public int customId { set; get; }
        public string FileName { set; get; }
        public string UniqueString { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }
        public bool isActive { set; get; }
    }
}
