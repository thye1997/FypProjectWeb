using Hangfire;

namespace FypProject.Hangfire.Services
{
    public class RandomJobService
    {
        private readonly LoggingService _loggingService;

        public RandomJobService(LoggingService loggingService)
        {
            _loggingService = loggingService;
        }

        public void ScheduleRandJob()
        {
            Console.WriteLine("Recurring randomjob!");

            var randHour = new Random().Next(10);

            _loggingService.Log("Invoke from first function");
            //RecurringJob.AddOrUpdate("randrecurringjob", () => Console.WriteLine("Recurring!"), Cron.Daily(randHour));

        }

        public void ScheduleRandJob2()
        {
            Console.WriteLine("Recurring randomjob!");

            var randHour = new Random().Next(10);

            _loggingService.Log("Invoke from second function");
            //RecurringJob.AddOrUpdate("randrecurringjob", () => Console.WriteLine("Recurring!"), Cron.Daily(randHour));

        }
    }
}
