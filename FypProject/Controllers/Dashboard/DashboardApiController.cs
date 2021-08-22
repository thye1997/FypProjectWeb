using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DashboardApiController : ControllerBase
    {
        private readonly DashboardService dashboardService;
        public DashboardApiController(DashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }
        [HttpPost]
        public IActionResult RetrieveData([FromBody]int accId)
        {
            try
            {
                var result = dashboardService.RetrievePatientDataCount(accId);
                return Ok(result);
            }catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
    }

}
