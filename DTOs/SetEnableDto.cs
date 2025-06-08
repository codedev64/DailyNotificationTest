namespace DailyNotificationService.DTOs
{
    public class SetEnableDto
    {
        public string UserId { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
    }
}
