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
        private readonly IAppointmentRepository apptRepository;
        private readonly IGenericRepository<Service> serviceRepository;
        private readonly IGenericRepository<MedicalPrescriptions> medPrescriptionRepository;
        private readonly IGenericRepository<Medicine> medicineRepository;
        private readonly IGenericRepository<SystemUser> sysUserRepository;
        public MedicalHistoryService(IAppointmentRepository apptRepository,
            IGenericRepository<Service> serviceRepository,
            IGenericRepository<MedicalPrescriptions> medPrescriptionRepository,
            IGenericRepository<Medicine> medicineRepository,
            IGenericRepository<SystemUser> sysUserRepository)
        {
            this.apptRepository = apptRepository;
            this.serviceRepository = serviceRepository;
            this.medPrescriptionRepository = medPrescriptionRepository;
            this.medicineRepository = medicineRepository;
            this.sysUserRepository = sysUserRepository;
        }


        public List<MedicalHistoryListViewModel> RetrieveMedicalHistoryListById(int Id)
        {
            var historyList = apptRepository.Where(c => c.userId == Id && c.Status == (int)SystemData.AppointmentStatus.Completed).ToList();
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
                        Service = serviceRepository.Where(c => c.Id == n.serviceId).FirstOrDefault().serviceName,
                        Result = !string.IsNullOrEmpty(n.Result) ? n.Result : "-",
                        FormattedMedicalPrescription = FormattedMedicalPrescription(n.Id),
                        DoctorName = sysUserRepository.Where(c=>c.Id == n.doctorId).FirstOrDefault()?.Name,
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
            var medPrescList = medPrescriptionRepository.Where(c => c.apptId == apptId).ToList();
            string formattedPrescription =null;
            if(medPrescList.Count > 0)
            {
                foreach(var n in medPrescList)
                {
                    formattedPrescription += medicineRepository.Where(c => c.Id == n.medId).FirstOrDefault().medName +"-"+n.Description + "/n"; // TODO: use better way to concate string
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
