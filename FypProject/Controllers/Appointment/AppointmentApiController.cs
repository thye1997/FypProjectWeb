using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Config;
using FypProject.Models;
using FypProject.Services;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        private readonly AppointmentService _apptService;
        private readonly AppointmentScheduleService _apptScheduleService;

        public AppointmentApiController(AppointmentService apptService, AppointmentScheduleService apptScheduleService)
        {
            this._apptService = apptService;
            this._apptScheduleService = apptScheduleService;
        }

        [HttpPost]
        public IActionResult AppointmentData([FromBody]AppointmentData viewModel)
        {
            try
            {
                    var result = _apptService.AppointmentListData(viewModel);
                    return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("throw error");
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult GetAppointmentConfigData()
        {
            try
            {
                var spHoliday = _apptScheduleService.GetSpecialHolidayList();
                var offDay = _apptScheduleService.RetrieveOffDaySchedule();
                var service = _apptService.GetServiceList();
                var result = new AppointmentApiScheduleViewModel
                {
                    SpHolidayList = spHoliday,
                    ServiceList = service,
                    OffDay = offDay
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("throw error");
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult LoadSpecificSlot([FromBody]SpecificSlotApiViewModel viewModel)
        {
            try
            {
                Debug.WriteLine($"this is called: {viewModel.Date} {viewModel.Slot}");
                var result = _apptScheduleService.LoadSpecificTimeSlot(viewModel.Date,viewModel.Slot);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("throw error");
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult ChangeAppointmentStatus([FromBody] RescheduleAppointmentApiViewModel viewModel)
        {
            try
            {
                var res = (GeneralResponse)null;
                Debug.WriteLine($"appt details is called: {viewModel.Date} {viewModel.ApptId}");

                res = _apptService.AppointmentAction(viewModel);
                return Ok(res);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("throw error");
                return NotFound(ex);
            }
        }

        [HttpPost]
        public IActionResult AddAppointment([FromBody] AddAppointmentApiViewModel viewModel)
        {
            try
            {
                Debug.WriteLine($"id {viewModel.AccId} service id {viewModel.ServiceId} date {viewModel.Date}  startTime {viewModel.StartTime}");
                var result = _apptService.AddAppointmentPatient(viewModel);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
                return NotFound(ex);
            }
        }

        [HttpPost]
        public IActionResult CheckInAppointment([FromBody] CheckInAppointmentApiViewModel viewModel)
        {
            try
            {
                var result = _apptService.CheckInAppointmentQR(viewModel);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

    }

}
