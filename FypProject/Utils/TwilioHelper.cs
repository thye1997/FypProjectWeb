using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.Models;
using FypProject.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using FypProject.Repository;


namespace FypProject.Utils
{
    public class TwilioHelper
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpClientFactory _clientFactory;

        public TwilioHelper(IConfiguration configuration, IServiceProvider serviceProvider, IHttpClientFactory clientFactory)
        {
            this._configuration = configuration;
            this._serviceProvider = serviceProvider;
            this._clientFactory = clientFactory;
        }

        public async Task SendSMSAsync(Appointment appt)
        {
            string TwilioSID = _configuration["Twilio:SID"];
            string TwilioAuth = _configuration["Twilio:AUTH"];
            string TwilioPhoneNumberFrom = _configuration["Twilio:PhoneNumberFrom"];
            string startTime = DateTime.Parse(appt.StartTime).ToUniversalTime().ToString("THHmmssZ");
            string formattedStartTime = DateTime.Parse(appt.Date).ToString("yyyyMMdd") + startTime;
            string formattedEndTime = DateTime.Parse(appt.Date).ToString("yyyyMMdd") + DateTime.Parse(appt.EndTime).ToUniversalTime().ToString("THHmmssZ");
            string calenderUrl = $"https://www.google.com/calendar/render?action=TEMPLATE&text=Your+Event+Name&dates={formattedStartTime}/{formattedEndTime}&details=&location=klinik+crosmed+%E9%A3%9E%E8%B7%83%E8%AF%8A%E6%89%80&sf=true&output=xml";
            string tinyUrl = await TinyUrlRequest(calenderUrl);
            /*TwilioClient.Init(TwilioSID, TwilioAuth);

            var message = await MessageResource.CreateAsync(
                body:$"Dear {user.FullName}, "+ SystemData.SMSMessage.ConfirmedApptMsg+
                $"Date of Appointment: {appt.Date}{Environment.NewLine}" +
                $"Start Time: {appt.StartTime}{Environment.NewLine}" +
                $"End Time: {appt.EndTime}{Environment.NewLine}" +
                $"Click the link below to add appointment event to calender{Environment.NewLine}" +
                tinyUrl,
                from: new Twilio.Types.PhoneNumber(TwilioPhoneNumberFrom),
                to: new Twilio.Types.PhoneNumber($"+6{user.PhoneNumber}")
            );*/
        }


        private  async Task<string> TinyUrlRequest(string calender)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://tinyurl.com/api-create.php?url={calender}");

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.SendAsync(request);
                var responseStream = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"tinyurl content is : {responseStream}");
                return responseStream;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            
        }

        public async Task SendSMSReminderAsync(List<ReminderSMSNotificationViewModel> viewModel)
        {
            string TwilioSID = _configuration["Twilio:SID"];
            string TwilioAuth = _configuration["Twilio:AUTH"];
            string TwilioPhoneNumberFrom = _configuration["Twilio:PhoneNumberFrom"];
            TwilioClient.Init(TwilioSID, TwilioAuth);

            foreach (var n in viewModel)
            {
                var message = await MessageResource.CreateAsync(
                body: $"Appointment Reminder{Environment.NewLine}" +
                $"Your schedule Appointment will be on tomorrow {n.date}{Environment.NewLine}" +
                $"{n.slot}{Environment.NewLine}",
                from: new Twilio.Types.PhoneNumber(TwilioPhoneNumberFrom),
                to: new Twilio.Types.PhoneNumber($"+6{n.phoneNumber}")
            );
            }          
        }

        public async Task SendSMSRescheduleReminderAsync(Appointment viewModel)
        {

            var user = (User)null;
            using (var scoped = _serviceProvider.CreateScope())
            {
                var accountRepository = scoped.ServiceProvider.GetRequiredService<IGenericRepository<User>>();
                 user = accountRepository.Where(c => c.Id == viewModel.userId).FirstOrDefault();
            };
            string TwilioSID = _configuration["Twilio:SID"];
            string TwilioAuth = _configuration["Twilio:AUTH"];
            string TwilioPhoneNumberFrom = _configuration["Twilio:PhoneNumberFrom"];
            TwilioClient.Init(TwilioSID, TwilioAuth);
            
                var message = await MessageResource.CreateAsync(
                body: $"Appointment Reschedule Reminder{Environment.NewLine}" +
                $"Your appointment has been rescheduled to {viewModel.Date}{Environment.NewLine}" +
                $"at {viewModel.StartTime} - {viewModel.EndTime} {Environment.NewLine}",
                from: new Twilio.Types.PhoneNumber(TwilioPhoneNumberFrom),
                to: new Twilio.Types.PhoneNumber($"+6{user.PhoneNumber}")
            );         
        }
    }
}
