using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;

namespace FypProject.Base
{
    public abstract class BaseService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;

        public abstract Task DoWork(CancellationToken cancellationToken);
        protected BaseService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _expression = CronExpression.Parse(cronExpression, CronFormat.Standard);
            //_expression = CronExpression.Parse(cronExpression, CronFormat.IncludeSeconds);
            _timeZoneInfo = timeZoneInfo;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("This part runned");
            //var cancelToken = new CancellationTokenSource();
            //cancelToken.Cancel();
           await SetupTimer(cancellationToken);
            //await ScheduleJob(cancellationToken);
        }
        /*protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"cancellation token value => {cancellationToken.IsCancellationRequested.ToString()}");
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            //Debug.WriteLine($"next schedule job time is => {next.Value.ToUniversalTime()}");
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                Debug.WriteLine($"Delay total seconds => {delay.TotalSeconds}");
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.AutoReset = true;
                _timer.Elapsed += async (sender, args) =>
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                        //await ScheduleJob(cancellationToken);    // reschedule next
                    }
                    //Dispose();  // reset and dispose timer
                   // _timer = null;

                   

                   /* if (!cancellationToken.IsCancellationRequested)
                    {
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }*/

        /*public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"is this runned?");
            await Task.Delay(5000, cancellationToken);  // do the work
        }*/

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }


        private async Task SetupTimer(CancellationToken cancellationToken)
        {
            var interval = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo); // calculate interval based on the cronos expression
            Debug.WriteLine($"Delay total seconds => {interval.Value.ToUniversalTime()}");

            if (interval.HasValue)
            {
                var nextScheduled = interval.Value - DateTimeOffset.Now;

                if (_timer == null)
                {
                    _timer = new System.Timers.Timer(nextScheduled.TotalMilliseconds);

                }

                _timer.Enabled = true;
                _timer.AutoReset = true;
                _timer.Elapsed += async (sender, args) => {
                    await DoWork(cancellationToken);
                    Dispose();
                    _timer = null;
                    await SetupTimer(cancellationToken); 
                };
                _timer.Start();
            }
           await Task.CompletedTask;
        } 
    }
}
