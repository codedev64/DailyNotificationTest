using System;
using System.Threading.Tasks;
using DailyNotificationService.Data;
using DailyNotificationService.DTOs;
using DailyNotificationService.Models;
using DailyNotificationService.Services;
using DailyNotificationService.Services.Interfaces;
using DailyNotificationService.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DailyNotificationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IUserService _userService;

        public NotificationsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("enableNotifications")]
        public async Task<IActionResult> Enable([FromBody] SetEnableDto req)
        {
            var success = await _userService.SetNotificationEnabled(req.UserId, req.Enabled);
            return success ? Ok("Notifications enabled.") : NotFound();
        }

        [HttpPost("disableNotifications")]
        public async Task<IActionResult> Disable([FromBody] SetDisableDto req)
        {
            var success = await _userService.SetNotificationDisabled(req.UserId, req.Disabled);
            return success ? Ok("Notifications disabled.") : NotFound();
        }

        [HttpPost("setNotificationTime")]
        public async Task<IActionResult> SetTime([FromBody] SetTimeDto req)
        {
            var result = await _userService.SetNotificationTime(req.UserId, req.TimeUtc);

            return result switch
            {
                UpdateTimeResult.Success => Ok(new { message = "Notification time updated." }),
                UpdateTimeResult.UserNotFound => NotFound(),
                UpdateTimeResult.InvalidTimeFormat => BadRequest("Invalid time format. Use HH:mm"),
                _ => StatusCode(500, new { error = "Unknown error" }),
            };
        }
    }
}
