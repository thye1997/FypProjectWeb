using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.Models;

namespace Microsoft.EntityFrameworkCore
{
    public static class BuilderExtension
    {
       public static ModelBuilder SeedWorkDay(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OffDay>().HasData(
                new OffDay { Id = 1, Day = "Monday" },
                new OffDay { Id = 2, Day = "Tuesday" },
                new OffDay { Id = 3, Day = "Wednesday" },
                new OffDay { Id = 4, Day = "Thursday" },
                new OffDay { Id = 5, Day = "Friday" },
                new OffDay { Id = 6, Day = "Saturday" },
                new OffDay { Id = 7, Day = "Sunday" }
                );
            return modelBuilder;
        }

        public static ModelBuilder InitSlot(this ModelBuilder modelBuilder)
        {
            var Slot1Start = new DateTime(2012, 12, 25, 8, 0, 0);
            var Slot1End = new DateTime(2012, 12, 25, 12, 0, 0);
            var Slot2Start= new DateTime(2012, 12, 25, 12, 0, 0);
            var Slot2End = new DateTime(2012, 12, 25, 19, 0, 0);
            var Slot3Start = new DateTime(2012, 12,25, 19, 0, 0);
            var Slot3End = new DateTime(2012, 12, 25, 22, 0, 0);




            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot { Id = 1, Slot= "Morning", Start= Slot1Start.ToString("hh:mm tt"), End= Slot1End.ToString("hh:mm tt") },
                new TimeSlot { Id = 2, Slot= "Afternoon", Start= Slot2Start.ToString("hh:mm tt"), End= Slot2End.ToString("hh:mm tt") },
                new TimeSlot { Id = 3, Slot= "Night", Start= Slot3Start.ToString("hh:mm tt"), End= Slot3End.ToString("hh:mm tt") }
                );
            return modelBuilder;
        }

        public static ModelBuilder InitDuration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SlotDuration>().HasData(
                new SlotDuration { Id=1, slotDuration=15, isActive = false},
                new SlotDuration { Id=2, slotDuration=30, isActive = true}
                );
            return modelBuilder;
        }

        public static ModelBuilder InitSysUser(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemUser>().HasData(
                new SystemUser { Id = 1, 
                    Role = SystemData.Role.Admin,
                    userName = "Admin_1",
                    createdBy="Admin",
                    createdOn = "18/08/2021 12:24 AM",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456")                 
                }
                );
            return modelBuilder;
        }
        public static ModelBuilder InitServiceType(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType {Id = 1, TypeName = "Medical Test"},
                new ServiceType { Id = 2, TypeName = "Vaccination"},
                new ServiceType { Id = 3, TypeName = "Other" }
                );
            return modelBuilder;
        }

    }
}
