using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Config;
using FypProject.Models;
using FypProject.Services;
using FypProject.Utils;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FypProject.Repository;

namespace FypProject.Controllers
{    
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class AppointmentController : BasicController
    {
        protected override string pageName { get; set; } = SystemData.View.AppointmentIndex;
        private string apptSchedulePageName { get; set; } = SystemData.View.AppointmentScheduleIndex;

        private readonly AppointmentService apptService;
        private readonly IGenericRepository<SystemUser> sysUserRepository;
        private readonly IConfiguration config;
        private readonly IHttpClientFactory clientFactory;
        public AppointmentController(AppointmentService apptService, IUserRepository userRepository,IConfiguration config, IHttpClientFactory clientFactory,
            IGenericRepository<SystemUser> sysUserRepository)
        {
            this.apptService = apptService;
            this.config = config;
            this.clientFactory = clientFactory;
            this.sysUserRepository = sysUserRepository;
        }
        public IActionResult Index()
        {
          return base.Index();
        }

        public IActionResult AppointmentSchedule()
        { var apptSchedule = apptService.RetrieveApptSchedule();
            
            return View(apptSchedulePageName, apptSchedule);
        }

       // [HttpPut]
        public IActionResult UpdateOffDay(int[] isChecked)
        {          
            var updatedOffDaySchedule =  apptService.UpdateOffDaySchedule(isChecked);
            return PartialView(SystemData.ViewPagePath.WorkDayTableView, updatedOffDaySchedule);
        }

        //[HttpPost]
        public JsonResult RetrieveSpecialHoliday(string dataRequest)
        {
            try
            {
                var dataList = apptService.GetSpecialHoliday();
                return this.DataTableResult(dict, dataList.spHolidayList);
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");
                return SetError(e);

            }
        }
        //[HttpPost]
        public JsonResult RetriveAppointmentList(int[] apptStatus, int today)
        {
            try
            {
                var searchValue = dict["search[value]"]?.ToLower();
                var dataList = apptService.GetAppointmentList(apptStatus,searchValue);
                if(today == 1) //done for today
                {
                    dataList = apptService.GetAppointmentList(apptStatus, searchValue).Where(c => DateTime.Parse(c.Date) == DateTime.Today).ToList();
                }
                return this.DataTableResult<AppointmentViewModel,Appointment>(dict, dataList);
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }
        //[HttpPost]
        public async Task<JsonResult> AddAppointment(Appointment obj, User user, int patientType)
        {
            Debug.WriteLine("new user details:=> "+ user.FullName);
            try
            {
                var sysUserId = Convert.ToInt32(User.FindFirst("Id").Value.ToString());
                obj.doctorId = sysUserId;
               await apptService.AddAppointment(obj, user, patientType);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment added successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }

        //[HttpPost]
        public JsonResult AddSpecialHoliday(SpecialHoliday obj)
        {
            try
            {
                apptService.AddSpecialHoliday(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Special Holiday added successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }

        //[HttpGet]
        public JsonResult GetApptRequestConfigData()
        {
            try
            {
               var result= apptService.GetSpecialHolidayList();
                var offDay = apptService.RetrieveOffDaySchedule();
                var service = apptService.GetServiceList();
                return SetMessage(SystemData.ResponseStatus.Success, data: new { spHoliday = result, offDay = offDay, service = service });
           }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }

        //[HttpGet]
        public JsonResult LoadSlotTime(string slot)
        {
            try
            {
                var result = apptService.LoadTimeSlot(slot);
                return SetMessage(data: new { timeslots = result });
            }
            catch(Exception ex)
            {
                return SetError(ex);
            }
        }

        //[HttpPut]
        public JsonResult UpdateTimeSlot(TimeSlot obj)
        {
            try
            {
                apptService.UpdateTimeSlot(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Time Slot Updated successfully.");
            }
            catch(Exception ex)
            {
                return SetError(ex);
            }
        }

        //[HttpGet]
        public JsonResult LoadSpecificTimeSlot(string date, int slot)
        {
            try
            {
                var result = apptService.LoadSpecificTimeSlot(date, slot);
                return Json(new { timeslots = result });
            }
            catch(Exception ex)
            {
                return SetError(ex);
            }
        }

        //[HttpGet]
        public JsonResult GetCurrentSlotDuration()
        {
            try
            {
                var result = apptService.GetSlotDuration();
                return SetMessage(data: result);
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }

       // [HttpPut]
        public JsonResult EditSlotDuration(int Id)
        {
            try
            {
               var result= apptService.EditSlotDuration(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Slot duration updated successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }

        //[HttpPut]
        public JsonResult CheckInAppt(int Id)
        {
            try
            {
                apptService.CheckInAppointment(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment check-in successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }

        public IActionResult AppointmentDetail(int Id)
        {
            try
            {
                var viewModel = apptService.AppointmentDetail(Id);
                return View(SystemData.ViewPagePath.ApptDetailView, viewModel);
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }
        }

        //[HttpPut]
        public JsonResult RescheduleAppointment(Appointment appt)
        {
            try
            {
               var result = apptService.RescheduleAppointment(appt);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment rescheduled successfully.", data:new { obj = result });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }

        //[HttpPut]
        public JsonResult UpdateAppointmentDetail(Appointment appt)
        {
            try
            {
                appt.doctorId = sysUserRepository.List().Where(c => c.userName == User.Identity.Name).FirstOrDefault().Id;
                var result = apptService.UpdateAppointmentDetail(appt);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment Updated successfully.", data: new { obj = result });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetError(ex);
            }
        }

        //[HttpPut]
        public JsonResult CancelAppointment(int Id)
        {
            try
            {
               var apptType= apptService.CancelAppointment(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment cancelled successfully.", data: new { apptStatus = apptType });
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }

       // [HttpPut]
        public JsonResult ChangeApptStatus(int Id, int Status)
        {
            try
            {
                var sysUserId = Convert.ToInt32(User.FindFirst("Id").Value.ToString());

                var apptStatus = apptService.ChangeAppointmentStatus(Id, Status, sysUserId);
                if (Status == (int)SystemData.AppointmentStatus.OnGoing) return SetMessage(SystemData.ResponseStatus.Success, "Changed to on going successfully.", data: new { apptStatus = apptStatus });
                else if (Status == (int)SystemData.AppointmentStatus.Completed) return SetMessage(SystemData.ResponseStatus.Success, "Changed to completed successfully.", data: new { apptStatus = apptStatus });
                else { return Json(new { }); }
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }

        //[HttpPost]
        public JsonResult AddAppointmentResult(MedicinePrescriptionViewModel viewModel)
        {     
            try
            {
                apptService.AddAppointmentResult(viewModel);
               return SetMessage(SystemData.ResponseStatus.Success, "Appointment edited successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }
    }
}
