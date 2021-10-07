using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.Models;
using FypProject.Repository;
using FypProject.ViewModel;

namespace FypProject.Services
{
    public class MedicalHistoryService
    {
        private readonly IAppointmentRepository _apptRepository;
        private readonly IGenericRepository<Service> _serviceRepository;
        private readonly IGenericRepository<MedicalPrescription> _medPrescriptionRepository;
        private readonly IGenericRepository<Medicine> _medicineRepository;
        private readonly IGenericRepository<SystemUser> _sysUserRepository;
        public MedicalHistoryService(IAppointmentRepository apptRepository,
            IGenericRepository<Service> serviceRepository,
            IGenericRepository<MedicalPrescription> medPrescriptionRepository,
            IGenericRepository<Medicine> medicineRepository,
            IGenericRepository<SystemUser> sysUserRepository)
        {
            _apptRepository = apptRepository;
            _serviceRepository = serviceRepository;
            _medPrescriptionRepository = medPrescriptionRepository;
            _medicineRepository = medicineRepository;
            _sysUserRepository = sysUserRepository;
        }


        public List<MedicalHistoryListViewModel> RetrieveMedicalHistoryListById(int Id)
        {
            //var historyList = _apptRepository.Where(c => c.userId == Id && c.Status == (int)SystemData.AppointmentStatus.Completed).ToList();
            var appointment = _apptRepository.Where(c => c.userId == Id && c.Status == (int)SystemData.AppointmentStatus.Completed);
            var service = _serviceRepository.ToQueryable();
            var sysUser = _sysUserRepository.ToQueryable();
            var medPrescription = _medPrescriptionRepository.ToQueryable();
            var medicine = _medicineRepository.ToQueryable();

            try
            {
                var result = (from a in appointment
                             join s in service on a.serviceId equals s.Id
                             join su in sysUser on a.doctorId equals su.Id
                             //from mL in a.medicalPrescription
                             //join m in medicine on mL.medId equals m.Id into mLm
                             select new MedicalHistoryListViewModel
                             {
                                 Id = a.Id,
                                 Date = a.Date,
                                 Slot = a.StartTime + " - " + a.EndTime,
                                 Service = s.serviceName,
                                 Result = !string.IsNullOrEmpty(a.Result) ? a.Result : "-",
                                 FormattedMedicalPrescription = "",
                                 DoctorName = su.Name,
                             }).ToList();
               //var dataList = result.ToList();
                foreach(var n in result)
                {
                    n.FormattedMedicalPrescription = GetMedicinePrescriptionByAppointmentId(n.Id);
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
        public string GetMedicinePrescriptionByAppointmentId(int apptId)
        {
            var medPrescription = _medPrescriptionRepository.Where(c=>c.apptId == apptId).ToList();
            var medicine = _medicineRepository.ToQueryable();
            var stringBuilder = new StringBuilder();
            if(medPrescription.Count > 0)
            {
                foreach (var n in medPrescription)
                {
                    stringBuilder.Append(_medicineRepository.Where(c => c.Id == n.medId).FirstOrDefault().medName + "-" + n.Description + "/n");
                }
                return stringBuilder.ToString();
            }
            
            return "-";
        }

    }
}
