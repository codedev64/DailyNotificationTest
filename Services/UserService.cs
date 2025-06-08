using System;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.Models;
using DailyNotificationService.Services.Interfaces;
using DailyNotificationService.Utils;

namespace DailyNotificationService.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetNotificationEnabled(string userId, bool enabled)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.NotificationsEnabled = enabled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetNotificationDisabled(string userId, bool disabled)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.NotificationsEnabled = disabled;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UpdateTimeResult> SetNotificationTime(string userId, string timeUtc)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return UpdateTimeResult.UserNotFound;

            if (!TimeSpan.TryParse(timeUtc, out var time))
            {
                return UpdateTimeResult.InvalidTimeFormat;
            }

            user.NotificationTime = time;
            await _context.SaveChangesAsync();

            return UpdateTimeResult.Success;
        }
    }
}
