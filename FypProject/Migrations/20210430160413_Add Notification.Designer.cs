﻿// <auto-generated />
using System;
using FypProject.Models.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FypProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210430160413_Add Notification")]
    partial class AddNotification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FypProject.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("FypProject.Models.AccountProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AppointmentPushReminderEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("AppointmentSMSReminderEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("FirebaseToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PushNotificationEnabled")
                        .HasColumnType("bit");

                    b.Property<int>("accountId")
                        .HasColumnType("int");

                    b.Property<bool>("isDefault")
                        .HasColumnType("bit");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("accountId");

                    b.HasIndex("userId");

                    b.ToTable("AccountProfiles");
                });

            modelBuilder.Entity("FypProject.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApptType")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int>("serviceId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("serviceId");

                    b.HasIndex("userId");

                    b.ToTable("appointments");
                });

            modelBuilder.Entity("FypProject.Models.MedicalHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("MedicalHistorys");
                });

            modelBuilder.Entity("FypProject.Models.MedicalPrescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("appointmentId")
                        .HasColumnType("int");

                    b.Property<int>("apptId")
                        .HasColumnType("int");

                    b.Property<int>("medId")
                        .HasColumnType("int");

                    b.Property<int?>("medicineId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("appointmentId");

                    b.HasIndex("medicineId");

                    b.HasIndex("userId");

                    b.ToTable("MedicalPrescriptions");
                });

            modelBuilder.Entity("FypProject.Models.Medicine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("medName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("FypProject.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("FypProject.Models.OffDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isOffDay")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("OffDays");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Day = "Monday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 2,
                            Day = "Tuesday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 3,
                            Day = "Wednesday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 4,
                            Day = "Thursday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 5,
                            Day = "Friday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 6,
                            Day = "Saturday",
                            isOffDay = false
                        },
                        new
                        {
                            Id = 7,
                            Day = "Sunday",
                            isOffDay = false
                        });
                });

            modelBuilder.Entity("FypProject.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("serviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("FypProject.Models.SlotDuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int>("slotDuration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("slotDurations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            isActive = false,
                            slotDuration = 15
                        },
                        new
                        {
                            Id = 2,
                            isActive = true,
                            slotDuration = 30
                        });
                });

            modelBuilder.Entity("FypProject.Models.SpecialHoliday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("specialHolidays");
                });

            modelBuilder.Entity("FypProject.Models.SystemUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("createdBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SystemUsers");
                });

            modelBuilder.Entity("FypProject.Models.TimeSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Start")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("timeSlots");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            End = "12:00 PM",
                            Slot = "Slot 1",
                            Start = "08:00 AM"
                        },
                        new
                        {
                            Id = 2,
                            End = "07:00 PM",
                            Slot = "Slot 2",
                            Start = "12:00 PM"
                        },
                        new
                        {
                            Id = 3,
                            End = "10:00 PM",
                            Slot = "Slot 3",
                            Start = "07:00 PM"
                        });
                });

            modelBuilder.Entity("FypProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NRIC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FypProject.Models.AccountProfile", b =>
                {
                    b.HasOne("FypProject.Models.Account", "account")
                        .WithMany("accountProfile")
                        .HasForeignKey("accountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FypProject.Models.User", "user")
                        .WithMany("accountProfile")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FypProject.Models.Appointment", b =>
                {
                    b.HasOne("FypProject.Models.Service", "service")
                        .WithMany()
                        .HasForeignKey("serviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FypProject.Models.User", "user")
                        .WithMany("appointment")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("service");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FypProject.Models.MedicalHistory", b =>
                {
                    b.HasOne("FypProject.Models.User", "user")
                        .WithMany("medicalHistory")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("FypProject.Models.MedicalPrescription", b =>
                {
                    b.HasOne("FypProject.Models.Appointment", "appointment")
                        .WithMany("medicalPrescription")
                        .HasForeignKey("appointmentId");

                    b.HasOne("FypProject.Models.Medicine", "medicine")
                        .WithMany()
                        .HasForeignKey("medicineId");

                    b.HasOne("FypProject.Models.User", "user")
                        .WithMany("medicalPrescription")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("appointment");

                    b.Navigation("medicine");

                    b.Navigation("user");
                });

            modelBuilder.Entity("FypProject.Models.Account", b =>
                {
                    b.Navigation("accountProfile");
                });

            modelBuilder.Entity("FypProject.Models.Appointment", b =>
                {
                    b.Navigation("medicalPrescription");
                });

            modelBuilder.Entity("FypProject.Models.User", b =>
                {
                    b.Navigation("accountProfile");

                    b.Navigation("appointment");

                    b.Navigation("medicalHistory");

                    b.Navigation("medicalPrescription");
                });
#pragma warning restore 612, 618
        }
    }
}
