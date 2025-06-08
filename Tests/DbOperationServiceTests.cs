using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.Models;
using DailyNotificationService.Services.DB;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DbOperationServiceTests
{
    private AppDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        context.Users.AddRange(
            new User
            {
                UserId = "user1",
                Platform = "android",
                NotificationsEnabled = true,
                NotificationTime = new TimeSpan(15, 0, 0),
            },
            new User
            {
                UserId = "user2",
                Platform = "ios",
                NotificationsEnabled = false,
            },
            new User
            {
                UserId = "user3",
                Platform = "android",
                NotificationsEnabled = true,
                NotificationTime = null,
            }
        );
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetUsersWithNotificationsEnabled_Returns_Correct_Users()
    {
        var context = GetDbContext();
        var service = new DbOperationService(context);

        var result = await service.GetUsersWithNotificationsEnabled();

        Assert.Single(result);
        Assert.Equal("user1", result[0].UserId);
    }
}
