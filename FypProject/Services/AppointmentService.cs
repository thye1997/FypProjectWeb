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
        private readonly IGenericRepository<OffDay> offDayRepository;
        private readonly IGenericRepository<Models.TimeSlot> timeSlotRepository;
        private readonly IGenericRepository<SpecialHoliday> spHolidayRepository;
        private readonly IGenericRepository<SlotDuration> slotDurationRepository;
        private readonly IAppointmentRepository apptRepository;
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository<AccountProfile> accProfileRepository;
        private readonly IGenericRepository<Service> serviceRepository;
        private readonly IGenericRepository<MedicalPrescriptions> medPrescRepository;
        private readonly IConfiguration config;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IGenericRepository<QRCode> qrRepository;
        private readonly TwilioHelper twilioHelper;
        private readonly FirebaseNotificationHelper firebaseNotificationHelper;
        private readonly IGenericRepository<Account> accRepository;
        public AppointmentService(IGenericRepository<OffDay> offDayRepository,
        IGenericRepository<SpecialHoliday> spHolidayRepository,
        IGenericRepository<Models.TimeSlot> timeSlotRepository,
        IGenericRepository<SlotDuration> slotDurationRepository,
        IAppointmentRepository apptRepository,
        IUserRepository userRepository,
        IGenericRepository<Service> serviceRepository,
        IGenericRepository<MedicalPrescriptions> medPrescRepository,
        IGenericRepository<AccountProfile> accProfileRepository,
        IConfiguration config,
        IHttpClientFactory httpClientFactory,
        IGenericRepository<QRCode> qrRepository,
        TwilioHelper twilio,
        FirebaseNotificationHelper firebaseNotificationHelper,
        IGenericRepository<Account> accRepository
            )
        {
            this.offDayRepository = offDayRepository;
            this.timeSlotRepository = timeSlotRepository;
            this.spHolidayRepository = spHolidayRepository;
            this.slotDurationRepository = slotDurationRepository;
            this.serviceRepository = serviceRepository;
            this.apptRepository = apptRepository;
            this.userRepository = userRepository;
            this.medPrescRepository = medPrescRepository;
            this.accProfileRepository = accProfileRepository;
            this.config = config;
            this.httpClientFactory = httpClientFactory;
            this.qrRepository = qrRepository;
            this.twilioHelper = twilio;
            this.firebaseNotificationHelper = firebaseNotificationHelper;
            this.accProfileRepository = accProfileRepository;
            this.accRepository = accRepository;
        }

        public AppointmentScheduleViewModel RetrieveApptSchedule()
        {
            AppointmentScheduleViewModel apptSchedule = new AppointmentScheduleViewModel();
            apptSchedule.offDayList = offDayRepository.List().ToList();
            apptSchedule.timeSlotList = timeSlotRepository.List().ToList();
            return apptSchedule;
        }

        public List<int> RetrieveOffDaySchedule()
        {
            var offDayList = offDayRepository.List().ToList();
            List<int> offDayArr = new List<int>();
            for(int i =0; i<offDayList.Count; i++)
            {
                if (offDayList[i].isOffDay)
                {
                    if (offDayList[i].Id == 7)
                    {
                        offDayArr.Add(0);
                    }
                    else
                    {
                        offDayArr.Add(offDayList[i].Id);
                    }
                }          
            }
            return offDayArr;
        }
        public  AppointmentScheduleViewModel UpdateOffDaySchedule(int[] isChecked)
        {
            var apptScheduleList =   offDayRepository.List();
            foreach(var n in apptScheduleList)
            {
                if (isChecked.Contains(n.Id)) n.isOffDay = false;
                else n.isOffDay = true;
            }

           int savechanges = offDayRepository.SaveChanges(); //debugging purpose
            //Debug.WriteLine("savechange value=>"+ savechanges.ToString());
            var updatedScheduleList = new AppointmentScheduleViewModel
            {
                offDayList = apptScheduleList.ToList()
            };
            return updatedScheduleList;
        }

        public AppointmentScheduleViewModel GetSpecialHoliday()
        {
            AppointmentScheduleViewModel apptSchedule = new AppointmentScheduleViewModel();
            apptSchedule.spHolidayList = spHolidayRepository.List().ToList();
            int count = 1;
            foreach (var cD in apptSchedule.spHolidayList)
            {
                cD.Id = count;
                count++;
            }
            return apptSchedule;         
        }

        public List<AppointmentViewModel> GetAppointmentList(int[] apptStatus, string searchValue)
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
                var result = apptRepository.GetAppointmentList(apptStatus).Where(c => userId.Contains(c.userId)).OrderBy(c => DateTime.Parse(c.Date)).ToList();
                return result;
            }
            else
            {
                var result = apptRepository.GetAppointmentList(apptStatus).OrderBy(c => DateTime.Parse(c.Date)).ToList();
                return result;
            }
       }

        public void AddSpecialHoliday(SpecialHoliday obj)
        {
            var isExist = spHolidayRepository.List().Where(c => obj.Date.Contains(c.Date)).FirstOrDefault();
            if (isExist != null) throw new BusinessException("Same date has exist.");
            else spHolidayRepository.Add(obj);
        }
        public List<SpecialHoliday> GetSpecialHolidayList()
        {   var formatDate = DateTime.Now.ToString("dd/MM/yyyy");
            var spHolidayList = spHolidayRepository.List().Where(c=> DateTime.Parse(c.Date)>= DateTime.Parse(formatDate)).OrderBy(x=> DateTime.Parse(x.Date)).ToList();
            return spHolidayList;
        }
        public List<TimeSlotHelper> LoadTimeSlot(string slot)
        {
            return TimeSlotHelper.returnSlot(slot);
        }

        public List<TimeSlotHelper> LoadSpecificTimeSlot(string date,int slot)
        {
            int duration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var timeSlot = timeSlotRepository.List().Where(c => c.Id == slot).FirstOrDefault();
            var apptList = apptRepository.List().Where(c => DateTime.Parse(c.Date) == DateTime.Parse(date)).ToList();
            var list= TimeSlotHelper.ReturnSpecificTimeSlot(apptList, timeSlot, duration);
            return list;
        }

        public void UpdateTimeSlot(Models.TimeSlot obj)
        {
            TimeSlotHelper.UpdateTimeSlot(obj, timeSlotRepository);
        }

        public int GetSlotDuration(int Id)
        {
            if (Id > 0) return EditSlotDuration(Id);
            var slotDuration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().Id;
            return slotDuration;
        }

        public int EditSlotDuration(int Id)
        {
            var slotDuration = slotDurationRepository.List();
            slotDuration.Where(c => c.Id == Id).FirstOrDefault().isActive = true;
            slotDuration.Where(c => c.Id != Id).FirstOrDefault().isActive = false;
            slotDurationRepository.SaveChanges();
            return Id;
        }

        public List<Service> GetServiceList()
        {
            var serviceList = serviceRepository.List().ToList().Where(c=>c.isActive).ToList();
            return serviceList;
        }

        public async Task AddAppointment(Appointment obj, User user, int patientType)
        {
            if(patientType == SystemData.PatientType.NewPatient)
            {
                var ifExist = userRepository.List().Where(c => c.NRIC == user.NRIC).FirstOrDefault();
                if (ifExist != null) throw new BusinessException("Patient has existed.");
                userRepository.Add(user);
                var newPatient = userRepository.List().Where(c => c.NRIC == user.NRIC).FirstOrDefault().Id;
                AddAppointmentNewPatient(obj, newPatient);
            }
            else
            {
                int duration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
                obj.RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                obj.isActive = true;
                
                    obj.Status = (int)SystemData.AppointmentStatus.Confirmed;
                
                if (obj.ApptType == (int)SystemData.AppointmentType.Schedule)
                {
                    obj.EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt");
                    var patient = userRepository.Find(obj.userId);//get patient details from DB
                    //await twilioHelper.SendSMS(obj);

                }
                apptRepository.Add(obj);
            }
           
        }

        public AppointmentDetailViewModel RescheduleAppointment (Appointment obj)
        {
            int duration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var appt = apptRepository.List().Where(c => c.Id == obj.Id).FirstOrDefault();
            if (appt != null)
            {
                appt.Date = obj.Date;
                appt.StartTime = obj.StartTime;
                appt.EndTime = DateTime.Parse(obj.StartTime).AddMinutes(duration).ToString("hh:mm tt");
                apptRepository.SaveChanges();
                var accountProfile = accProfileRepository.List().Where(c => c.userId == appt.userId).FirstOrDefault();
                if (accountProfile != null)
                {
                    var account = accRepository.List().Where(c => c.Id == accountProfile.accountId).FirstOrDefault();
                    if(!string.IsNullOrEmpty(account.FirebaseToken))
                    {
                        string content = $"Your appointment has been rescheduled to {appt.Date} at time {appt.StartTime+" - "+ appt.EndTime}.";
                        //firebaseNotificationHelper.SendRescheduleNotification(account, content);
                        //twilioHelper.SendSMSRescheduleReminder(appt);
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
            var appt = apptRepository.List().Where(c => c.Id == obj.Id).FirstOrDefault();
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
            int duration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
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
            var appt = apptRepository.List().Where(c => c.Id == Id).FirstOrDefault();
            if (appt != null) appt.Status = (int)SystemData.AppointmentStatus.InQueue;
            apptRepository.SaveChanges();
        }
        public string CancelAppointment (int Id)
        {
            if (Id <= 0) throw new BusinessException("Invalid Appointment Id.");
            var appt = apptRepository.List().Where(c => c.Id == Id).FirstOrDefault();
            if (appt != null) appt.Status = (int)SystemData.AppointmentStatus.Cancelled;
            apptRepository.SaveChanges();

            return AppointmentRepository.RestructStatusName(appt.Status);
        }
        
        public string ChangeAppointmentStatus(int Id, int Status, int sysUserId)
        {
            var appt = apptRepository.List().Where(c => c.Id == Id).FirstOrDefault();
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
            var appt = apptRepository.List().Where(c => c.Id == viewModel.AppointmentId).FirstOrDefault();
           
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

            var accProfile = accProfileRepository.List().Where(c => c.accountId == obj.accId).ToList();

            if (accProfile.Count > 0)
            {
                foreach(var n in accProfile)
                {
                    profileId.Add(n.userId);
                }
            }
            if (obj.apptId > 0)
            { var isCheckin = false;
                if(obj.apptId == 114 && obj.apptStatusInt == (int)AppointmentStatus.Confirmed)
                {
                    isCheckin = true;
                }
                var apptDetail = apptRepository.GetAppointmentDetail(obj.apptId);
                if (apptDetail != null)
                {
                    var isAction = !TimeSlotHelper.ReturnTodayDate(apptDetail.Date) && !TimeSlotHelper.ReturnPastTodayDate(apptDetail.Date);
                    if(apptDetail.Status != (int)AppointmentStatus.Confirmed)
                    {
                        isAction = false;
                    }
                    appointmentDataList.Add(new AppointmentData
                    {
                        accId = obj.accId,
                        apptId = apptDetail.Id,
                        apptStatusInt = apptDetail.Status,
                        apptStatusString = apptDetail.StatusString,
                        date = apptDetail.Date,
                        service = apptDetail.Service,
                        startTime = apptDetail.StartTime,
                        endTime = apptDetail.EndTime,
                        FullName = apptDetail.FullName,
                        NRIC = apptDetail.NRIC,
                        PhoneNumber = apptDetail.PhoneNumber,
                        Gender = apptDetail.Gender,
                        DOB = apptDetail.DOB,
                        Slot = apptDetail.Slot,
                        note = apptDetail.Note,
                        isCheckIn = isCheckin,
                        isAction = isAction
                    });

                    return appointmentDataList;
                }
            }
            var result = apptRepository.GetAppointmentList(obj.apptStatus).Where(c => profileId.Contains(c.userId)).OrderBy(c => DateTime.Parse(c.Date)).ToList();
            if (result.Count>0)
            {
                foreach(var n in result)
                {
                    appointmentDataList.Add(new AppointmentData
                    {
                        accId = obj.accId,
                        apptId = n.Id,
                        apptStatusString = n.Status,
                        date = n.Date,
                        service = n.Service,
                        startTime = n.StartTime,
                        endTime = n.EndTime,
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
            if(obj.actionType == 1)
            {
                var appt = new Appointment
                {
                    Id = obj.apptId,
                    StartTime = obj.startTime,
                    Date = obj.date
                };
              RescheduleAppointment(appt);
                return new GeneralResponse
                {
                    message = "Appointment reschedule successfully.",
                    isSuccess = true
                };
            }
            else if(obj.actionType == 2)
            {
                CancelAppointment(obj.apptId);
                return new GeneralResponse
                {
                    message = "Appointment cancelled successfully.",
                    isSuccess = true
                };
            }
            else
            {
                CheckInAppointment(obj.apptId);
                return new GeneralResponse
                {
                    message = "Check In successfully.",
                    isSuccess = true
                };
        }
        }

        public GeneralResponse AddAppointmentPatient(AddAppointmentApiViewModel obj)
        {
            int duration = slotDurationRepository.List().Where(c => c.isActive == true).FirstOrDefault().slotDuration; // retrieve duration per appt
            var profile = accProfileRepository.List().Where(c => c.accountId == obj.accId && c.isDefault).FirstOrDefault();
            var user = userRepository.List().Where(c => c.Id == profile.userId).FirstOrDefault();
            if (profile == null) throw new BusinessException("Profile not exist..");
            var appt = new Appointment
            {
                userId = user.Id,
                ApptType = (int)AppointmentType.Schedule,
                Note = obj.note,
                Date = obj.date,
                StartTime = obj.startTime,
                EndTime = DateTime.Parse(obj.startTime).AddMinutes(duration).ToString("hh:mm tt"),
                RequestTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                Status = (int)AppointmentStatus.Confirmed,
                serviceId = obj.serviceId,      
            };
            apptRepository.Add(appt);
            //twilioHelper.SendSMS(appt, user, httpClientFactory);
            return new GeneralResponse
            {
                message = "Appointment request successfully.",
                isSuccess = true
            };
        }

        public GeneralResponse CheckInAppointmentQR(CheckInAppointmentApiViewModel obj)
        {
            var qrCode = qrRepository.List().Where(c => c.UniqueString == obj.uniqueString && c.isActive).FirstOrDefault();
            if(qrCode != null)
            {
                CheckInAppointment(obj.apptId);

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
