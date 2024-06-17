using HRM.Domain.Entities;
using HRM.Service.HRM.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRM.Service.HRM.Services;

public class OpenApiPrService : IOpenApiPrService
{
    private readonly HrmContext _context;

    public OpenApiPrService(HrmContext context)
    {
        _context = context;
    }

    public async Task<List<Employment>> GetListEmploymentsIncludePersonal()
    {
        var result = await _context.Employments
            .Include(e => e.Personal)
            .ToListAsync();
        return result;
    }

    public async Task<List<Employment>> GetListEmploymentIncludeWorkingTime()
    {
        var result = await _context.Employments
            .Include(e => e.EmploymentWorkingTimes)
            .ToListAsync();
        return result;
    }
}