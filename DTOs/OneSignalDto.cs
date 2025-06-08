using System;
using System.Collections.Generic;

namespace DailyNotificationService.DTOs
{
    public class OneSignalDto
    {
        public string app_id { get; set; } = string.Empty;
        public string[] include_external_user_ids { get; set; } = Array.Empty<string>();
        public string channel_for_external_user_ids { get; set; } = "push";
        public Dictionary<string, string> headings { get; set; } = new();
        public Dictionary<string, string> contents { get; set; } = new();
    }
}
