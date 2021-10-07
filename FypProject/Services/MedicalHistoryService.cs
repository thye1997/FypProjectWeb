using System;
using System.Collections.Generic;
using System.Linq;
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
            var historyList = _apptRepository.Where(c => c.userId == Id && c.Status == (int)SystemData.AppointmentStatus.Completed).ToList();
            List<MedicalHistoryListViewModel> medHistoryList = new List<MedicalHistoryListViewModel>();
            if(historyList.Count > 0)
            {   int count = 0;
                foreach (var n in historyList)
                { count += 1;
                    medHistoryList.Add(new MedicalHistoryListViewModel
                    {
                        Id = count,
                        Date = n.Date,
                        Slot = n.StartTime + " - " + n.EndTime,
                        Service = _serviceRepository.Where(c => c.Id == n.serviceId).FirstOrDefault().serviceName,
                        Result = !string.IsNullOrEmpty(n.Result) ? n.Result : "-",
                        FormattedMedicalPrescription = FormattedMedicalPrescription(n.Id),
                        DoctorName = _sysUserRepository.Where(c=>c.Id == n.doctorId).FirstOrDefault()?.Name,
                    });
                }
                return medHistoryList;
            }
            else
            {
                return medHistoryList;
            }
        }

        public string FormattedMedicalPrescription(int apptId)
        {
            var medPrescList = _medPrescriptionRepository.Where(c => c.apptId == apptId).ToList();
            string formattedPrescription =null;
            if(medPrescList.Count > 0)
            {
                foreach(var n in medPrescList)
                {
                    formattedPrescription += _medicineRepository.Where(c => c.Id == n.medId).FirstOrDefault().medName +"-"+n.Description + "/n"; // TODO: use better way to concate string
                }
                return formattedPrescription;
            }
            else
            {
                return "-";
            }

        }
    }
}
