namespace DailyNotificationService.Utils
{
    public enum Platform
    {
        ios,
        android,
    }

    public enum UpdateTimeResult
    {
        Success,
        UserNotFound,
        InvalidTimeFormat,
    }
}
