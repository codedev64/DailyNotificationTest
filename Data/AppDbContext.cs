using System.Linq;
using DailyNotificationService.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyNotificationService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
