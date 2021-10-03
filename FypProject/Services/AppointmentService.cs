using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Config;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.Repository;
using FypProject.Utils;
using FypProject.ViewModel;
using Microsoft.Extensions.Configuration;
using static FypProject.Config.SystemData;

namespace FypProject.Services
{
    public class AppointmentService
    {
        private readonly IGenericRepository<SlotDuration> slotDurationRepository;
        private readonly IAppointmentRepository apptRepository;
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository<AccountProfile> accProfileRepository;
        private readonly IGenericRepository<Service> serviceRepository;
        private readonly IGenericRepository<MedicalPrescriptions> medPrescRepository;
        private readonly IGenericRepository<QRCode> qrRepository;
        private readonly IGenericRepository<Account> accRepository;
        public AppointmentService(
        IGenericRepository<SlotDuration> slotDurationRepository,
        IAppointmentRepository apptRepository,
        IUserRepository userRepository,
        IGenericRepository<Service> serviceRepository,
        IGenericRepository<MedicalPrescriptions> medPrescRepository,
        IGenericRepository<AccountProfile> accProfileRepository,
        IGenericRepository<QRCode> qrRepository,
        IGenericRepository<Account> accRepository
            )
        {
            this.slotDurationRepository = slotDurationRepository;
            this.serviceRepository = serviceRepository;
            this.apptRepository = apptRepository;
            this.userRepository = userRepository;
            this.medPrescRepository = medPrescRepository;
            this.accProfileRepository = accProfileRepository;
            this.qrRepository = qrRepository;
            this.accProfileRepository = accProfileRepository;
            this.accRepository = accRepository;
        }


        public IQueryable<AppointmentViewModel> GetAppointmentList(int[] apptStatus, string searchValue)
        {    List<User> user = new List<User>();
            List<int> userId = new List<int>();

            apptRepository.CheckNoShowAppointment();
            if (!string.IsNullOrEmpty(searchValue)) {
                 user = userRepository.GetUserListBySearch(searchValue).ToList();
                if (user != null)
                {
                    foreach (var obj in user)
                    {
                        userId.Add(obj.Id);
                    }
                }
                //.OrderBy(c => DateTime.Parse(c.Date))
                var result = apptRepository.GetAppointmentList(apptStatus).Where(c => userId.Contains(c.userId));
                return result;
            }
            else
            {
                //.OrderBy(c => DateTime.Parse(c.Date))
                var result = apptRepository.GetAppointmentList(apptStatus);
                return result;
            }
       }

        public List<Service> GetServiceList()
        {
            var serviceList = serviceRepository.Where(c=>c.isActive).ToList();
            return serviceList;
        }

        public async Task AddAppointment(Appointment obj, User user, int patientType)
        {
            if(patientType == SystemData.PatientType.NewPatient)
            {
                var ifExist = userRepository.Where(c => c.NRIC == user.NRIC).FirstOrDefault();
                if (ifExist != null) throw new BusinessException("Patient has existed.");
                userRepository.Add(user);
                var newPatient = userRepository.Where(c => c.NRIC == user.NRIC).FirstOrDefault().Id;
                AddAppointmentNewPatient(obj, newPatient);
            }
            else
            {
                int duration = slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
                obj.RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                obj.isActive = true;
                
                    obj.Status = (int)SystemData.AppointmentStatus.Confirmed;
                
                if (obj.ApptType == (int)SystemData.AppointmentType.Schedule)
                {
                    obj.EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt");
                    var patient = userRepository.Find(obj.userId);//get patient details from DB
                    //await twilioHelper.SendSMSAsync(obj);
                }
                apptRepository.Add(obj);
            }
           
        }

        public async Task<AppointmentDetailViewModel> RescheduleAppointment (Appointment obj)
        {
            int duration = slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var appt = apptRepository.Where(c => c.Id == obj.Id).FirstOrDefault();
            if (appt != null)
            {
                appt.Date = obj.Date;
                appt.StartTime = obj.StartTime;
                appt.EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt");
                apptRepository.SaveChanges();
                var accountProfile = accProfileRepository.Where(c => c.userId == appt.userId).FirstOrDefault();
                if (accountProfile != null)
                {
                    var account = accRepository.Where(c => c.Id == accountProfile.accountId).FirstOrDefault();
                    if(!string.IsNullOrEmpty(account.FirebaseToken))
                    {
                        string content = $"Your appointment has been rescheduled to {appt.Date} at time {appt.StartTime+" - "+ appt.EndTime}.";
                        //firebaseNotificationHelper.SendRescheduleNotificationAsync(account, content);
                        //twilioHelper.SendSMSRescheduleReminderAsync(appt);
                    }
                }
            }
            var apptDetail = new AppointmentDetailViewModel
            {
                Date = appt.Date,
                Slot = appt.StartTime + "-" + appt.EndTime
            };
            return apptDetail;
        }
        public AppointmentDetailViewModel UpdateAppointmentDetail(Appointment obj)
        {
            var appt = apptRepository.Where(c => c.Id == obj.Id).FirstOrDefault();
            if (appt != null)
            {

                apptRepository.SaveChanges();
            }
            return new AppointmentDetailViewModel
            {
                Date = appt.Date,
                Slot = appt.StartTime + "-" + appt.EndTime
            };
        }
        public AppointmentDetailViewModel AppointmentDetail(int Id)
        {
            if (Id <= 0) throw new BusinessException("Invalid Appointment Id.");
            return apptRepository.GetAppointmentDetail(Id);
        }
        public void AddAppointmentNewPatient(Appointment obj, int userId)
        {
            int duration = slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            obj.RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            obj.isActive = true;
            obj.userId = userId;
            if (obj.ApptType == (int)SystemData.AppointmentType.WalkIn && string.IsNullOrEmpty(obj.Date))
            {
                obj.Date = DateTime.Now.ToString("dd/MM/yyyy");
                obj.Status = (int)SystemData.AppointmentStatus.InQueue;
            }
            else
            {
                obj.Status = (int)SystemData.AppointmentStatus.Confirmed;
            }
            if (obj.ApptType == (int)SystemData.AppointmentType.Schedule) obj.EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt");
            apptRepository.Add(obj);
        }
        
        public void CheckInAppointment(int Id)
        {
            if (Id <= 0) throw new BusinessException("Invalid Appointment Id.");
            var appt = apptRepository.Where(c => c.Id == Id).FirstOrDefault();
            if (appt != null) appt.Status = (int)SystemData.AppointmentStatus.InQueue;
            apptRepository.SaveChanges();
        }
        public string CancelAppointment (int Id)
        {
            if (Id <= 0) throw new BusinessException("Invalid Appointment Id.");
            var appt = apptRepository.Where(c => c.Id == Id).FirstOrDefault();
            if (appt != null) appt.Status = (int)SystemData.AppointmentStatus.Cancelled;
            apptRepository.SaveChanges();

            return AppointmentRepository.RestructStatusName(appt.Status);
        }
        
        public string ChangeAppointmentStatus(int Id, int Status, int sysUserId)
        {
            var appt = apptRepository.Where(c => c.Id == Id).FirstOrDefault();
            if (appt != null)
            {
                if (Status == (int)SystemData.AppointmentStatus.OnGoing)
                {
                    appt.Status = (int)SystemData.AppointmentStatus.OnGoing;
                    apptRepository.SaveChanges();
                }
                if (Status == (int)SystemData.AppointmentStatus.Completed)
                {
                    appt.doctorId = sysUserId;
                    appt.Status = (int)SystemData.AppointmentStatus.Completed;
                    apptRepository.SaveChanges();
                }
            }
            return AppointmentRepository.RestructStatusName(appt.Status);
        }

        public void AddAppointmentResult(MedicinePrescriptionViewModel viewModel)
        { List<MedicalPrescriptions> medicalPrescriptions = new List<MedicalPrescriptions>();
            var appt = apptRepository.Where(c => c.Id == viewModel.AppointmentId).FirstOrDefault();
           
            if(viewModel.PrescriptionList != null)
            {
                foreach (var n in viewModel.PrescriptionList)
                {
                    medicalPrescriptions.Add(
                        new MedicalPrescriptions
                        {
                            apptId = viewModel.AppointmentId,
                            medId = n.Id,
                            Description = n.Description
                        }
                        );
                }
                medicalPrescriptions.ForEach(c => medPrescRepository.Add(c)); // add each of medical prescription one by one
            }
            
            appt.Result = viewModel.result;
            apptRepository.SaveChanges();
        }

        /**appointment api part*/
        public List<AppointmentData> AppointmentListData(AppointmentData obj)
        {
            List<User> user = new List<User>();
            List<int> profileId = new List<int>();
            var appointmentDataList = new List<AppointmentData>();
            apptRepository.CheckNoShowAppointment();

            var accProfile = accProfileRepository.Where(c => c.accountId == obj.AccId).ToList();

            if (accProfile.Count > 0)
            {
                foreach(var n in accProfile)
                {
                    profileId.Add(n.userId);
                }
            }
            if (obj.ApptId > 0)
            { var isCheckin = false;
                if(obj.ApptId == 114 && obj.ApptStatusInt == (int)AppointmentStatus.Confirmed) // for testing
                {
                    isCheckin = true;
                }
                var apptDetail = apptRepository.GetAppointmentDetail(obj.ApptId);
                if (apptDetail != null)
                {
                    var isAction = !TimeSlotHelper.ReturnTodayDate(apptDetail.Date) && !TimeSlotHelper.ReturnPastTodayDate(apptDetail.Date);
                    if(apptDetail.Status != (int)AppointmentStatus.Confirmed)
                    {
                        isAction = false;
                    }
                    appointmentDataList.Add(new AppointmentData
                    {
                        AccId = obj.AccId,
                        ApptId = apptDetail.Id,
                        ApptStatusInt = apptDetail.Status,
                        ApptStatusString = apptDetail.StatusString,
                        Date = apptDetail.Date,
                        Service = apptDetail.Service,
                        StartTime = apptDetail.StartTime,
                        EndTime = apptDetail.EndTime,
                        FullName = apptDetail.FullName,
                        NRIC = apptDetail.NRIC,
                        PhoneNumber = apptDetail.PhoneNumber,
                        Gender = apptDetail.Gender,
                        DOB = apptDetail.DOB,
                        Slot = apptDetail.Slot,
                        Note = apptDetail.Note,
                        IsCheckIn = isCheckin,
                        IsAction = isAction
                    });

                    return appointmentDataList;
                }
            }
            //.OrderBy(c => DateTime.Parse(c.Date))
            var result = apptRepository.GetAppointmentList(obj.ApptStatus).Where(c => profileId.Contains(c.userId)).ToList();
            if (result.Count>0)
            {
                foreach(var n in result)
                {
                    appointmentDataList.Add(new AppointmentData
                    {
                        AccId = obj.AccId,
                        ApptId = n.Id,
                        ApptStatusString = n.Status,
                        Date = n.Date,
                        Service = n.Service,
                        StartTime = n.StartTime,
                        EndTime = n.EndTime,
                        FullName = n.FullName
                    });
                }

                return appointmentDataList;
            }
            else
            {
                return appointmentDataList;
            }
        }

        public GeneralResponse AppointmentAction(RescheduleAppointmentApiViewModel obj)
        {
            //TODO: give action type a const int
            if(obj.ActionType == 1)
            {
                var appt = new Appointment
                {
                    Id = obj.ApptId,
                    StartTime = obj.StartTime,
                    Date = obj.Date
                };
              RescheduleAppointment(appt);
                return new GeneralResponse
                {
                    message = "Appointment reschedule successfully.",
                    isSuccess = true
                };
            }
            else if(obj.ActionType == 2)
            {
                CancelAppointment(obj.ApptId);
                return new GeneralResponse
                {
                    message = "Appointment cancelled successfully.",
                    isSuccess = true
                };
            }
            else
            {
                CheckInAppointment(obj.ApptId);
                return new GeneralResponse
                {
                    message = "Check In successfully.",
                    isSuccess = true
                };
        }
        }

        public GeneralResponse AddAppointmentPatient(AddAppointmentApiViewModel obj)
        {
            int duration = slotDurationRepository.Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var profile = accProfileRepository.Where(c => c.accountId == obj.AccId && c.isDefault).FirstOrDefault();
            var user = userRepository.Where(c => c.Id == profile.userId).FirstOrDefault();
            if (profile == null) throw new BusinessException("Profile not exist..");
            var appt = new Appointment
            {
                userId = user.Id,
                ApptType = (int)AppointmentType.Schedule,
                Note = obj.Note,
                Date = obj.Date,
                StartTime = obj.StartTime,
                EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt"),
                RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                Status = (int)AppointmentStatus.Confirmed,
                serviceId = obj.ServiceId,      
            };
            apptRepository.Add(appt);
            //twilioHelper.SendSMSAsync(appt);
            return new GeneralResponse
            {
                message = "Appointment request successfully.",
                isSuccess = true
            };
        }

        public GeneralResponse CheckInAppointmentQR(CheckInAppointmentApiViewModel obj)
        {
            var qrCode = qrRepository.Where(c => c.UniqueString == obj.UniqueString && c.isActive).FirstOrDefault();
            if(qrCode != null)
            {
                CheckInAppointment(obj.ApptId);

                return new GeneralResponse
                {
                    message = "Appointment check-in successfully.",
                    isSuccess = true
                };
            }
            else
            {
                return new GeneralResponse
                {
                    message = "Invalid QR Code",
                    isSuccess = false
                };
            }
        }
            
    }

}
