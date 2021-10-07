using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using FypProject.Config;
using FypProject.Models;
using FypProject.Utils;
using FypProject.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class BenchmarkTest
    {

        [Benchmark]
        //[ArgumentsSource(nameof(Data))]
        public List<AppointmentViewModel> GetAppointmentList()
        {
            int[] apptStatus = new int[] { 0,2,3,4,301 };
            using (var _dbContexts = new BloggingContext())
            {
                var appointment = _dbContexts.Appointment.AsQueryable();
                IQueryable<User> user = _dbContexts.User.AsQueryable();
                IQueryable<Service> service = _dbContexts.Service.AsQueryable();

                var result = from u in user
                             join appt in appointment on u.Id equals appt.userId
                             where apptStatus.Contains(appt.Status)
                             join s in service on appt.serviceId equals s.Id
                             //join u in user on appt.userId equals u.Id into uQuery
                             //join s in service
                             //on appt.serviceId equals s.Id into sQuery
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

                //return result.Take(10).ToList();
                return result.ToList();

            }


        }

        [Benchmark]
        public List<AppointmentViewModel> GetAppointmentList_2()
        {
            int[] apptStatus = new int[] { 0, 2, 3, 4, 301 };
            using (var _dbContexts = new BloggingContext())
            {
                var result = _dbContexts.Appointment.Where(c => apptStatus.Contains(c.Status))
                          .Select(appt => new AppointmentViewModel
                          {
                              Id = appt.Id,
                              userId = appt.userId,
                              ApptType = RestructApptType(appt.ApptType),
                              Date = appt.Date,
                              Slot = appt.StartTime + "-" + appt.EndTime,
                              Service = _dbContexts.Service.Where(c => c.Id == appt.serviceId).FirstOrDefault().serviceName,
                              StartTime = appt.StartTime,
                              EndTime = "-" + appt.EndTime,
                              FullName = _dbContexts.User.Where(c => c.Id == appt.userId).FirstOrDefault().FullName,
                              NRIC = _dbContexts.User.Where(c => c.Id == appt.userId).FirstOrDefault().NRIC,
                              PhoneNumber = _dbContexts.User.Where(c => c.Id == appt.userId).FirstOrDefault().PhoneNumber,
                              Status = RestructStatusName(appt.Status),
                              checkIn = TimeSlotHelper.ReturnCheckIn(appt.Date, appt.StartTime),
                          }
                           ).ToList();

                return result;
            }



            /* */
        }

        public static string RestructStatusName(int status)
        {
            if (status == (int)SystemData.AppointmentStatus.CheckIn)
            {
                return "Check-In";
            }
            else if (status == (int)SystemData.AppointmentStatus.InQueue)
            {
                return "In Queue";
            }
            else if (status == (int)SystemData.AppointmentStatus.OnGoing)
            {
                return "On Going";
            }
            else if (status == (int)SystemData.AppointmentStatus.NoShow)
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

        public IEnumerable<object[]> Data()
        {
            yield return new object[] { new int[] { 0 } };
        }

        public class BloggingContext : DbContext
        {
            public DbSet<User> User { set; get; }
            public DbSet<Account> Account { set; get; }
            public DbSet<AccountProfile> AccountProfile { set; get; }
            public DbSet<Appointment> Appointment { get; set; }
            public DbSet<MedicalHistory> MedicalHistory { set; get; }
            public DbSet<MedicalPrescription> MedicalPrescription { set; get; }
            public DbSet<Service> Service { set; get; }
            public DbSet<Medicine> Medicine { set; get; }
            public DbSet<SystemUser> SystemUser { set; get; }
            public DbSet<OffDay> OffDay { set; get; }
            public DbSet<SpecialHoliday> SpecialHoliday { set; get; }
            public DbSet<TimeSlot> TimeSlot { set; get; }
            public DbSet<SlotDuration> SlotDuration { set; get; }
            public DbSet<Notification> Notification { set; get; }
            public DbSet<QRCode> QRCode { set; get; }
            public DbSet<Reminder> Reminder { set; get; }
            public DbSet<ServiceType> ServiceType { set; get; }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseSqlServer(@"Server=LAPTOP-5T49C199\MSSQLSERVER01; Persist Security Info=True;Database=FypProjectDB; User Id=sa_3; password=sa_3;")
                .LogTo(Console.WriteLine, LogLevel.Information);

        }

        
    }
}
