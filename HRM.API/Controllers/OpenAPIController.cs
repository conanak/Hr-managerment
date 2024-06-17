using HRM.Domain.Models;
using HRM.Service.PR.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAPIController : ControllerBase
    {
        private readonly IOpenAPIHRService _openAPIHRService;
        public OpenAPIController(IOpenAPIHRService openAPIHRService)
        {
            _openAPIHRService = openAPIHRService;
        }

        [HttpPost("hr/getallvacations")]
        public async Task<List<EmployeesVactionModel>> GetEmployeesVacationDays([FromBody] List<decimal> employeeIds)
        {
            try
            {
                var result = await _openAPIHRService.GetAllEmployeesVacation(employeeIds);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine("error when get all employees vacation days" + ex.Message);
                throw;
            }
        }
    }
}
