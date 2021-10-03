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

        public DbSet<User> Users { set; get; }
        public DbSet<Account> Accounts { set; get; }
        public DbSet<AccountProfile> AccountProfiles { set; get; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<MedicalHistory> MedicalHistorys { set; get; }
        public DbSet<MedicalPrescription> MedicalPrescriptions { set; get; }
        public DbSet<MedicalPrescriptions> MedicalPrescription { set; get; }

        public DbSet<Service> Services { set; get; }
        public DbSet<Medicine> Medicines { set; get; }
        public DbSet<SystemUser> SystemUsers { set; get; }
        public DbSet<OffDay> OffDays { set; get; }
        public DbSet<SpecialHoliday> specialHolidays { set; get; }
        public DbSet<TimeSlot> timeSlots { set; get; }
        public DbSet<SlotDuration> slotDurations { set; get; }
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
