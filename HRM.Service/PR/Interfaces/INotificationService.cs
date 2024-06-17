using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Domain.Entities;

namespace HRM.Service.HR.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationModel> GetAllNotificationsAsync();
    }
}
