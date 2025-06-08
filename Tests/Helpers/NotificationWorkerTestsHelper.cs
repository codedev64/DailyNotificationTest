using System;
using System.Collections.Generic;
using System.Linq;
using DailyNotificationService.Models;

public static class NotificationWorkerTestsHelper
{
    public static List<User> FilterUsersToNotify(List<User> users, TimeSpan now)
    {
        return users
            .Where(u =>
                u.NotificationTime != null &&
                Math.Abs((u.NotificationTime.Value - now).TotalMinutes) < 1)
            .ToList();
    }
}