using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using BloodDonation.Application.Services.Interfaces;

namespace BloodDonation.Application.Services
{
    public class BloodStorageCheckService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<BloodStorageCheckService> _logger;
        private Timer? _timer;

        private readonly int _intervalInMinutes = 9999;

        public BloodStorageCheckService(IServiceProvider services, ILogger<BloodStorageCheckService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BloodStorageCheckService is starting.");
            ScheduleNextRun();
            return Task.CompletedTask;
        }

        private void ScheduleNextRun()
        {
            var interval = TimeSpan.FromMinutes(_intervalInMinutes);
            _timer = new Timer(RunJob, null, TimeSpan.Zero, interval);
            _logger.LogInformation($"Blood storage check scheduled every {_intervalInMinutes} minute(s)");
        }

        private async void RunJob(object? state)
        {
            _logger.LogInformation("Blood storage check job started at: {time}", DateTime.Now);

            using var scope = _services.CreateScope();
            var bloodStorageService = scope.ServiceProvider.GetRequiredService<IBloodStorageService>();

            await bloodStorageService.ExpireOutdatedBloodAsync();

            _logger.LogInformation("Blood storage check completed.");
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
