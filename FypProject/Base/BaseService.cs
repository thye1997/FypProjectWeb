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

        protected BaseService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _expression = CronExpression.Parse(cronExpression, CronFormat.IncludeSeconds);
            _timeZoneInfo = timeZoneInfo;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("This part runned");
            //var cancelToken = new CancellationTokenSource();
            //cancelToken.Cancel();
            await ScheduleJob(cancellationToken);
        }
        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"cancellation token value => {cancellationToken.IsCancellationRequested.ToString()}");
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            //Debug.WriteLine($"next schedule job time is => {next.Value.ToUniversalTime()}");
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }

                   /* if (!cancellationToken.IsCancellationRequested)
                    {
                    }*/
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"is this runned?");
            await Task.Delay(5000, cancellationToken);  // do the work
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }

      
    }
}
