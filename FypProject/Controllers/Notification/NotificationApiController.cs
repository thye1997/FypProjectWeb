using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationApiController : ControllerBase
    {
        private readonly NotificationService notificationService;
        public NotificationApiController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult GetNotificationList()
        {
            var result = notificationService.GetNotificationList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult GetReminderList([FromBody]int accId)
        {          
            try
            {
                var result = notificationService.GetReminderList(accId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }

        }
    }
}
