using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using DailyNotificationService.Configurations;
using DailyNotificationService.DTOs;
using DailyNotificationService.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DailyNotificationService.Services
{
    public class NotificationService
    {
        private readonly OneSignalSettings _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IOptions<OneSignalSettings> options,
            ILogger<NotificationService> logger
        )
        {
            _settings = options.Value;
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public async Task SendNotification(User user)
        {
            var payload = new OneSignalDto
            {
                app_id = _settings.AppId,
                include_external_user_ids = new[] { user.UserId },
                channel_for_external_user_ids = "push",
                headings = new Dictionary<string, string> { { "en", "Daily Reminder" } },
                contents = new Dictionary<string, string>
                {
                    { "en", "Hey! Here is your daily update!" },
                },
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://onesignal.com/api/v1/notifications"
            )
            {
                Content = JsonContent.Create(payload),
            };
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic",
                _settings.ApiKey
            );
            request.Content = new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await _httpClient.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "Failed to send notification to {UserId}. Status: {StatusCode}, Response: {Response}",
                        user.UserId,
                        response.StatusCode,
                        result
                    );
                }
                else
                {
                    _logger.LogInformation(
                        "Notification sent to {UserId}. Status: {StatusCode}",
                        user.UserId,
                        response.StatusCode
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Exception while sending notification to {UserId}",
                    user.UserId
                );
            }
        }
    }
}
