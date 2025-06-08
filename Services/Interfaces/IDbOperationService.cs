using System.Collections.Generic;
using System.Threading.Tasks;
using DailyNotificationService.Models;

namespace DailyNotificationService.Services.Interfaces
{
    public interface IDbOperationService
    {
        Task<List<User>> GetUsersWithNotificationsEnabled();
    }
}
