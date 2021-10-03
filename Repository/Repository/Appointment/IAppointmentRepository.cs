using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;
using FypProject.ViewModel;

namespace FypProject.Repository
{
   public interface IAppointmentRepository: IGenericRepository<Appointment>
    {
        public IQueryable<AppointmentViewModel> GetAppointmentList(int[] apptStatus);
        public AppointmentDetailViewModel GetAppointmentDetail(int Id);
        public void CheckNoShowAppointment();
    }
}
