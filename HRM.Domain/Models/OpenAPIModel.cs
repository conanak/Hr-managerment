using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Models
{
    public class EmployeesVactionModel
    {
        public decimal EmployeeId { get; set; }
        public int VactionDays { get; set; }
        public string EmployeeName { get; set; }
    }
}
