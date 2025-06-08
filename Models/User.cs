using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyNotificationService.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("platform")]
        public string Platform { get; set; }

        [Column("notifications_enabled")]
        public bool NotificationsEnabled { get; set; }

        [Column("notification_time")]
        public TimeSpan? NotificationTime { get; set; }
    }
}
