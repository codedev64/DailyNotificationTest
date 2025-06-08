using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DailyNotificationService.Utils;

namespace DailyNotificationService.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> SetNotificationEnabled(string userId, bool enabled);
        Task<bool> SetNotificationDisabled(string userId, bool disabled);
        Task<UpdateTimeResult> SetNotificationTime(string userId, string timeUtc);
    }
}
