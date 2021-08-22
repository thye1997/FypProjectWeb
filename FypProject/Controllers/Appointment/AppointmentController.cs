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
    [Authorize]
    public class AppointmentController : BaseController<Appointment,AppointmentViewModel>
    {
        protected override string pageName { get; set; } = SystemData.View.AppointmentIndex;
        private string apptSchedulePageName { get; set; } = SystemData.View.AppointmentScheduleIndex;

        private readonly AppointmentService apptService;
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository<SystemUser> sysUserRepository;
        private readonly IConfiguration config;
        private readonly IHttpClientFactory clientFactory;
        public AppointmentController(AppointmentService apptService, IUserRepository userRepository,IConfiguration config, IHttpClientFactory clientFactory,
            IGenericRepository<SystemUser> sysUserRepository)
        {
            this.apptService = apptService;
            this.userRepository = userRepository;
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

        [HttpPost]
        public IActionResult UpdateOffDay(int[] isChecked)
        {          
            var updatedOffDaySchedule =  apptService.UpdateOffDaySchedule(isChecked);
            return PartialView(SystemData.ViewPagePath.WorkDayTableView, updatedOffDaySchedule);
        }

        [HttpPost]
        public JsonResult RetrieveSpecialHoliday(string dataRequest)
        {
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            try
            {
                int recordsTotal = 0;
                // getting all Customer data  
                var result = apptService.GetSpecialHoliday();                
                recordsTotal = result.spHolidayList.Count;
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);

                //Returning Json Data  
                var data = result.spHolidayList.Skip(skip).Take(pageSize).ToList();
                return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");
                return this.SetMessage(ex: e);

            }
        }
        [HttpPost]
        public JsonResult RetriveAppointmentList(int[] apptStatus, int today)
        {
            Debug.WriteLine("today :=>" + today);
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            try
            {
                int recordsTotal = 0;
                // getting all Customer data  
                var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
                var result = apptService.GetAppointmentList(apptStatus,searchValue);
                if(today == 1) //done for today
                {
                    result = apptService.GetAppointmentList(apptStatus, searchValue).Where(c => DateTime.Parse(c.Date) == DateTime.Today).ToList();
                }
                recordsTotal = result.Count;
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);

                //Returning Json Data  
                var data = result.Skip(skip).Take(pageSize).ToList();
                return Json(new { recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return this.SetMessage(ex: e);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddAppointment(Appointment obj, User user, int patientType)
        {
            Debug.WriteLine("new user details:=> "+ user.FullName);
            try
            {
                var sysUserId = Convert.ToInt32(User.FindFirst("Id").Value.ToString());
                obj.doctorId = sysUserId;
               await apptService.AddAppointment(obj, user, patientType, config, clientFactory);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment added successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
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
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
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
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult LoadSlotTime(string slot)
        {
            var result = apptService.LoadTimeSlot(slot);
            return SetMessage(data: new { timeslots = result });
        }

        [HttpPost]
        public JsonResult UpdateTimeSlot(TimeSlot obj)
        {
            try
            {
                apptService.UpdateTimeSlot(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Time Slot Updated successfully.");
            }
            catch(Exception ex)
            {
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult LoadSpecificTimeSlot(string date, int slot)
        {
            var result = apptService.LoadSpecificTimeSlot(date,slot);
            return Json(new { timeslots = result });
        }

        [HttpPost]
        public JsonResult SlotDuration(int Id)
        {
            try
            {
               var result= apptService.GetSlotDuration(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Slot duration updated successfully.");
            }
            catch (Exception ex)
            {
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult CheckInAppt(int Id)
        {
            try
            {
                apptService.CheckInAppointment(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment check-in successfully.");
            }
            catch (Exception ex)
            {
                return SetMessage(ex: ex);
            }
        }

        public IActionResult AppointmentDetail(int Id)
        {
            var viewModel = apptService.AppointmentDetail(Id);
            return View(SystemData.ViewPagePath.ApptDetailView, viewModel);
        }

        [HttpPost]
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
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
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
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult CancelAppointment(int Id)
        {
            try
            {
               var apptType= apptService.CancelAppointment(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "Appointment cancelled successfully.", data: new { apptStatus = apptType });
            }
            catch (Exception ex)
            {
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
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
                return SetMessage(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult AddAppointmentResult(MedicinePrescriptionViewModel viewModel)
        {     
            try
            {
                apptService.AddAppointmentResult(viewModel);
               return SetMessage(SystemData.ResponseStatus.Success, "Appointment edited successfully.");
            }
            catch (Exception ex)
            {
                return SetMessage(ex: ex);
            }
        }
    }
}
