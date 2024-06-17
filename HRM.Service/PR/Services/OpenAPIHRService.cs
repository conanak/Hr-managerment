using HRM.Domain.HRM.Entities;
using HRM.Domain.Models;
using HRM.Service.PR.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace HRM.Service.PR.Services
{
    public class OpenAPIHRService :  IOpenAPIHRService
    {
        private readonly MydbContext _dbContext;
        public OpenAPIHRService(MydbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmployeesVactionModel>> GetAllEmployeesVacation(List<decimal> employeeIds)
        {
            var result = new List<EmployeesVactionModel>();
            var employeeIdsInt = employeeIds.Select(decimalValue => (int)decimalValue).ToList();
            if (employeeIdsInt != null)
            {
                var employees = await _dbContext.Employees
                    .Where(x => employeeIdsInt.Contains(x.IdEmployee))
                    .Select(x => new EmployeesVactionModel
                    {
                        EmployeeId = x.IdEmployee,
                        VactionDays = x.VacationDays.Value,
                        EmployeeName = x.FirstName + x.LastName
                    })
                    .ToListAsync();
                result.AddRange(employees);
            }
            return result;
        }
    }
}
