using FypProject.Hangfire.Contract;
using FypProject.Hangfire.Services;
using Hangfire;
using Hangfire.Storage.Monitoring;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Hangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {

        private readonly RandomJobService _randomJobService;
        private readonly Logging _logging;


        public JobController(RandomJobService randomJobService, Logging logging)
        {
            _randomJobService = randomJobService;
            _logging = logging;
        }

        [HttpGet(Name = "ScheduledJob")]
        public ActionResult ScheduledJob()
        {
            _randomJobService.ScheduleRandJob();
            _randomJobService.ScheduleRandJob2();
            //RecurringJob.AddOrUpdate("myrecurringjob",() => _randomJobService.ScheduleRandJob(), Cron.Daily);

           var finalMessage = _logging.Message;

            return Ok();
        }


        
    }
}
