using HRM.Domain.Entities;

namespace HRM.Service.HRM.Interfaces;

public interface IOpenApiPrService
{
    Task<List<Employment>> GetListEmploymentsIncludePersonal();
    
    Task<List<Employment>> GetListEmploymentIncludeWorkingTime();
}