using HRM.Domain.Enums;
using HRM.Service.PR.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacationController : Controller
{
    private readonly IVacationService _service;

    public VacationController(IVacationService service)
    {
        _service = service;
    }

    [HttpGet("get-total-vacation-days-taken/{filterType}")]
    public async Task<IActionResult> GetTotalVacationDaysTaken([FromRoute] HrmFilterType filterType)
    {
        try
        {
            var result = await _service.GetTotalVacationDaysTaken(filterType);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}