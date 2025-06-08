namespace DailyNotificationService.DTOs
{
    public class SetDisableDto
    {
        public string UserId { get; set; } = string.Empty;
        public bool Disabled { get; set; } = false;
    }
}
