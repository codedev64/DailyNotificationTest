using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.Services;
using DailyNotificationService.Services.DB;
using DailyNotificationService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DailyNotificationService.Background
{
    public class NotificationWorker : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<NotificationWorker> _logger;

        public NotificationWorker(IServiceProvider services, ILogger<NotificationWorker> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int retries = 0;

            while (retries < 10 && !stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _services.CreateScope();
                    var dbOps = scope.ServiceProvider.GetRequiredService<IDbOperationService>();
                    await dbOps.GetUsersWithNotificationsEnabled(); // probe
                    break; // success
                }
                catch (Exception ex)
                {
                    retries++;
                    _logger.LogWarning(
                        "DB not ready yet (retry {Retry}): {Message}",
                        retries,
                        ex.Message
                    );
                    await Task.Delay(3000, stoppingToken);
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var sender = scope.ServiceProvider.GetRequiredService<NotificationService>();
                var dbOps = scope.ServiceProvider.GetRequiredService<IDbOperationService>();

                var nowUtc = DateTime.UtcNow.TimeOfDay;
                var users = await dbOps.GetUsersWithNotificationsEnabled();

                var nowRounded = new TimeSpan(DateTime.UtcNow.Hour, DateTime.UtcNow.Minute, 0);

                var usersToNotify = users
                    .Where(u =>
                        u.NotificationTime != null
                        && Math.Abs((u.NotificationTime.Value - nowRounded).TotalMinutes) < 1
                    )
                    .ToList();

                foreach (var user in usersToNotify)
                {
                    await sender.SendNotification(user);
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
