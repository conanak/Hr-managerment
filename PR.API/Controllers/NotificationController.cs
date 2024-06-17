using HRM.Domain.Models;
using HRM.Service.HR.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("getall")]
        public async Task<NotificationModel> GetAllNotifications()
        {
            try
            {
                var result = await _notificationService.GetAllNotificationsAsync();
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine("error when get all notifications: " + ex.Message);
                throw;
            }
        }

    }
}
