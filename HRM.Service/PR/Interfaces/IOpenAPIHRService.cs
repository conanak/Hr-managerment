using HRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Service.PR.Interfaces
{
    public interface IOpenAPIHRService
    {
        Task<List<EmployeesVactionModel>> GetAllEmployeesVacation(List<decimal> employeeIds);
    }
}
