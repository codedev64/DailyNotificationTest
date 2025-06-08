using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.Models;
using DailyNotificationService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyNotificationService.Services.DB
{
    public class DbOperationService : IDbOperationService
    {
        private readonly AppDbContext _context;

        public DbOperationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersWithNotificationsEnabled()
        {
            var enabledUsers = await _context
                .Users.Where(u => u.NotificationsEnabled && u.NotificationTime != null)
                .ToListAsync();
            return enabledUsers;
        }
    }
}
