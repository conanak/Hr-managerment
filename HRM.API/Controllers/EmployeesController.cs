using HRM.Domain.Enums;
using HRM.Service.HRM.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _service;

        public EmployeesController(IEmployeesService service)
        {
            _service = service;
        }

        [HttpGet("get-total-earning/{filterType}")]
        public async Task<IActionResult> GetTotalEarning(HrmFilterType filterType)
        {
            try
            {
                var result = await _service.GetTotalEarning(filterType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}