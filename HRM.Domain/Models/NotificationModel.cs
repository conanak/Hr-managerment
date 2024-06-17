using HRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class NotificationModel
    {
        public List<NotificationData> Datas { get; set; }
        public int TotalCount { get; set; }
    }
    public class NotificationData
    {
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public string PublishedTime { get; set; }
    }
    public class EmployeeBaseModel
    {
        public string EmployeeName { get; set; }
    }

    public class EmployeeBirthDateModel : EmployeeBaseModel
    {
        public DateOnly BirthDate { get; set; }
        public int Ages { get; set; }
    }
    public class EmployeeAniveralModel : EmployeeBaseModel
    {
        public int AniveralYears { get; set; }
    }
    public class EmployeesLimitedVacationModel : EmployeeBaseModel
    {
        public decimal NumberOfVacation { get; set; }
    }
}
