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
        private readonly AppointmentService apptService;
        public AppointmentApiController(AppointmentService apptService)
        {
            this.apptService = apptService;
        }

        [HttpPost]
        public IActionResult AppointmentData([FromBody]AppointmentData viewModel)
        {
            try
            {
                    var result = apptService.AppointmentListData(viewModel);
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
                var spHoliday = apptService.GetSpecialHolidayList();
                var offDay = apptService.RetrieveOffDaySchedule();
                var service = apptService.GetServiceList();
                var result = new AppointmentApiScheduleViewModel
                {
                    spHolidayList = spHoliday,
                    serviceList = service,
                    offDay = offDay
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
                Debug.WriteLine($"this is called: {viewModel.date} {viewModel.slot}");
                var result = apptService.LoadSpecificTimeSlot(viewModel.date,viewModel.slot);
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
                Debug.WriteLine($"appt details is called: {viewModel.date} {viewModel.apptId}");

                res = apptService.AppointmentAction(viewModel);
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
                Debug.WriteLine($"id {viewModel.accId} service id {viewModel.serviceId} date {viewModel.date}  startTime {viewModel.startTime}");
                var result = apptService.AddAppointmentPatient(viewModel);
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
                var result = apptService.CheckInAppointmentQR(viewModel);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

    }

}
