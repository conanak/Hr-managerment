using HRM.Service.HRM.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PR.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OpenApiPrController : Controller
{
    private readonly IOpenApiPrService _service;

    public OpenApiPrController(IOpenApiPrService service)
    {
        _service = service;
    }

    [HttpGet("get-list-employments-include-personal")]
    public async Task<IActionResult> GetListEmploymentsIncludePersonal()
    {
        try
        {
            var result = await _service.GetListEmploymentsIncludePersonal();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-list-employment-include-working-time")]
    public async Task<IActionResult> GetListEmploymentIncludeWorkingTime()
    {
        try
        {
            var result = await _service.GetListEmploymentIncludeWorkingTime();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}