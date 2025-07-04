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
    public class BloodDonationReminderService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BloodDonationReminderService> _logger;
        private Timer? _timer;

        private readonly bool _isTesting = false; // set = false để chạy thật

        public BloodDonationReminderService(IServiceProvider services, ILogger<BloodDonationReminderService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BloodDonationReminderService is starting.");
            ScheduleNextRun();
            return Task.CompletedTask;
        }

        private void ScheduleNextRun()
        {
            if (_isTesting)
            {
                _timer = new Timer(RunJob, null, TimeSpan.Zero, TimeSpan.FromSeconds(100));
                _logger.LogInformation("TEST MODE: Scheduled every 10 seconds.");
            }
            else
            {
                var now = DateTime.Now;
                var nextRunTime = DateTime.Today.AddHours(8); // 8h sáng hôm nay

                if (now > nextRunTime)
                    nextRunTime = nextRunTime.AddDays(1); // nếu đã quá 8h sáng thì hẹn tới 8h sáng ngày mai

                var delay = nextRunTime - now;

                _timer = new Timer(RunJob, null, delay, Timeout.InfiniteTimeSpan); // chỉ chạy 1 lần, rồi tự lập lại sau
                _logger.LogInformation("Next blood donation reminder scheduled at: {time}", nextRunTime);
            }
        }

        private async void RunJob(object? state)
        {
            _logger.LogInformation("Blood donation reminder job started at: {time}", DateTime.Now);

            try
            {
                using var scope = _services.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IBloodDonationService>();
                await service.SendReminderEmailsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while sending blood donation reminders.");
            }

            // Schedule next run for tomorrow at 8h
            ScheduleNextRun();
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }

}
