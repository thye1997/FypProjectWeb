﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FypProject.Config;
using FypProject.Models;
using FypProject.Models.DBContext;
using FypProject.Utils;
using FypProject.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace FypProject.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public IQueryable<AppointmentViewModel> GetAppointmentList(int[] apptStatus)
        {
            var appointment = _dbContexts.appointments.AsQueryable();
            IQueryable<User> user = _dbContexts.Users.AsQueryable();
            IQueryable<Service> service = _dbContexts.Services.AsQueryable();

            var result = from u in user
                         join appt in appointment on u.Id equals appt.userId where apptStatus.Contains(appt.Status)
                         join s in service on appt.serviceId equals s.Id
                         select new AppointmentViewModel
                         {
                             Id = appt.Id,
                             userId = appt.userId,
                             ApptType = RestructApptType(appt.ApptType),
                             Date = appt.Date,
                             Slot = appt.StartTime + "-" + appt.EndTime,
                             Service = s.serviceName,
                             StartTime = appt.StartTime,
                             EndTime = "-" + appt.EndTime,
                             FullName = u.FullName,
                             NRIC = u.NRIC,
                             PhoneNumber = u.PhoneNumber,
                             Status = RestructStatusName(appt.Status),
                             checkIn = TimeSlotHelper.ReturnCheckIn(appt.Date, appt.StartTime),
                         };

            return result;
        }
        public AppointmentDetailViewModel GetAppointmentDetail(int Id)
        {
            var medPrescriptionList = MedPrescriptionList(Id);
            var result = _dbContexts.appointments.Where(c => c.Id == Id)
                        .Select(appt => new AppointmentDetailViewModel
                        {                         
                            Id = appt.Id,
                            UserId = appt.userId,
                            FullName = _dbContexts.Users.Where(c=> c.Id == appt.userId).FirstOrDefault().FullName,
                            PhoneNumber = _dbContexts.Users.Where(c => c.Id == appt.userId).FirstOrDefault().PhoneNumber,
                            Gender = _dbContexts.Users.Where(c => c.Id == appt.userId).FirstOrDefault().Gender,
                            NRIC = _dbContexts.Users.Where(c => c.Id == appt.userId).FirstOrDefault().NRIC,
                            DOB = _dbContexts.Users.Where(c => c.Id == appt.userId).FirstOrDefault().DOB,
                            ApptType = RestructApptType(appt.ApptType),
                            Date=appt.Date,
                            StartTime = appt.StartTime,
                            EndTime = appt.EndTime,
                            Slot = appt.StartTime + "-" + appt.EndTime,
                            Status = appt.Status,
                            StatusString = RestructStatusName(appt.Status),
                            Note = appt.Note,
                            medicalPrescriptions = medPrescriptionList,
                            Service = _dbContexts.Services.Where(c=>c.Id == appt.serviceId).FirstOrDefault().serviceName,
                            Result = appt.Result,
                            isCheckIn =TimeSlotHelper.ReturnCheckIn(appt.Date, appt.StartTime)
                        }).FirstOrDefault();
            return result;
        }

        public void CheckNoShowAppointment()
        {
            var apptList = base.ToQueryable();
            String now = DateTime.Now.ToString("HH:mm tt");
            bool overTime;
            DateTime timeNow = DateTime.Parse(now);
            foreach (var obj in apptList)
            {
                bool isToday = TimeSlotHelper.ReturnTodayDate(obj.Date);
                bool pastToday = TimeSlotHelper.ReturnPastTodayDate(obj.Date);
                if(obj.ApptType == (int)SystemData.AppointmentType.WalkIn )
                {
                    overTime = TimeSlotHelper.ReturnIfOverTime(timeNow, isToday);
                }
                else
                {
                    overTime = TimeSlotHelper.ReturnIfOverTime(DateTime.Parse(obj.StartTime), timeNow, isToday);
                }
                if ((overTime || pastToday) && obj.Status == (int)SystemData.AppointmentStatus.Confirmed)
                {
                    obj.Status = (int)SystemData.AppointmentStatus.NoShow;
                }
            }
            _dbContexts.SaveChanges();
        }

        public static string RestructStatusName(int status)
        {
            if(status == (int)SystemData.AppointmentStatus.CheckIn)
            {
                return "Check-In";
            }
            else if(status == (int)SystemData.AppointmentStatus.InQueue)
            {
                return "In Queue";
            }
            else if(status == (int)SystemData.AppointmentStatus.OnGoing)
            {
                return "On Going";
            }
            else if(status == (int)SystemData.AppointmentStatus.NoShow)
            {
                return "No Show";
            }
            else
            {
                return ((SystemData.AppointmentStatus)status).ToString();
            }
        }
        public static string RestructApptType(int appType)
        {
            if (appType == (int)SystemData.AppointmentType.WalkIn)
            {
                return "Walk-In";
            }         
            else
            {
                return ((SystemData.AppointmentType)appType).ToString();
            }
        }

        public  List<AppointmentMedicalPrescriptionViewModel> MedPrescriptionList(int Id)
        {
            List<AppointmentMedicalPrescriptionViewModel> apptMedViewModel = new List<AppointmentMedicalPrescriptionViewModel>();
            var medPrescription = _dbContexts.MedicalPrescription.Where(c => c.apptId == Id).ToList();
            foreach(var n in medPrescription)
            {
                apptMedViewModel.Add(
                    new AppointmentMedicalPrescriptionViewModel
                    {
                        medType = _dbContexts.Medicines.Where(c => c.Id == n.medId).FirstOrDefault().Type,
                        medName = _dbContexts.Medicines.Where(c => c.Id == n.medId).FirstOrDefault().medName,
                        Description = n.Description
                    });
            }
            return apptMedViewModel;
        }

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { new int[] { 0,4,201,301 }};
        }


    }
}