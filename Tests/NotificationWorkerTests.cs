using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.Models;
using DailyNotificationService.Services.DB;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class NotificationWorkerTests
{
    [Fact]
    public void FilterUsersToNotify_Returns_Users_With_Matching_Times()
    {
        var now = new TimeSpan(15, 0, 0);
        var users = new List<User>
        {
            new User
            {
                UserId = "user1",
                NotificationsEnabled = true,
                NotificationTime = new TimeSpan(15, 0, 0),
            },
            new User
            {
                UserId = "user2",
                NotificationsEnabled = true,
                NotificationTime = new TimeSpan(14, 59, 0),
            },
            new User
            {
                UserId = "user3",
                NotificationsEnabled = true,
                NotificationTime = null,
            },
        };

        var toNotify = NotificationWorkerTestsHelper.FilterUsersToNotify(users, now);

        Assert.Single(toNotify);
        Assert.Equal("user1", toNotify[0].UserId);
    }
}
