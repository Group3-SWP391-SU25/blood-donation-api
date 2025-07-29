using BloodDonation.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class BloodDonationRequestCheckService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BloodDonationRequestCheckService> _logger;
        private Timer? _timer;

        private readonly TimeSpan _checkTime = new TimeSpan(17, 0, 0); //17h VietNam

        public BloodDonationRequestCheckService(IServiceProvider services, ILogger<BloodDonationRequestCheckService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BloodDonationRequestCheckService is starting.");

            ScheduleNextRun(); // Tính thời gian đến lần chạy tiếp theo

            return Task.CompletedTask;
        }

        private void ScheduleNextRun()
        {
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
            var nextRun = now.Date.Add(_checkTime);

            if (now > nextRun)
                nextRun = nextRun.AddDays(1);

            var delay = nextRun - now;

            _timer = new Timer(RunJob, null, delay, Timeout.InfiniteTimeSpan);
            //_timer = new Timer(RunJob, null, TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1));


            _logger.LogInformation($"BloodDonationRequestCheckService next job scheduled at: {nextRun}");
        }

        private async void RunJob(object? state)
        {
            _logger.LogInformation("BloodDonationRequestCheckService job started at: {time}", DateTime.Now);

            using (var scope = _services.CreateScope())
            {
                var requestService = scope.ServiceProvider.GetRequiredService<IBloodDonationRequestService>();

                await requestService.CancelExpiredPendingRequestsAsync(); //Call service to cancel expired requests
            }

            ScheduleNextRun(); //Schedule the next run
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}