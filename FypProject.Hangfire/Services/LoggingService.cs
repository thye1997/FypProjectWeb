using FypProject.Hangfire.Contract;

namespace FypProject.Hangfire.Services
{
    public class LoggingService
    {
        private readonly Logging _logging;

        public LoggingService(Logging logging)
        {
             _logging = logging;
        }

        public void Log(string message)
        {
            if(string.IsNullOrEmpty(_logging.Message))
                _logging.Message = message;
            else
                _logging.Message += message;
        }
    }
}
