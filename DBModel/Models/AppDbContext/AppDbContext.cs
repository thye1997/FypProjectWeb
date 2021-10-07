using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FypProject.Models.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {

        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(LAPTOP-5T49C199)\MSSQLSERVER01;Database=FypProjectDB; User Id=sa_2; password=sa_2;");
        }*/

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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(c => c.systemUser)
                .WithMany(b => b.appointments)
                .HasForeignKey(b => b.doctorId);

                
            modelBuilder.SeedWorkDay();
            modelBuilder.InitSlot();
            modelBuilder.InitDuration();
            modelBuilder.InitSysUser();
            modelBuilder.InitServiceType();
        }
    }
}
