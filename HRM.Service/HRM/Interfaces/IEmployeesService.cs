using HRM.Domain.Enums;
using HRM.Domain.HRM.Entities;
using HRM.Domain.Models;

namespace HRM.Service.HRM.Interfaces;

public interface IEmployeesService
{
    Task<List<TotalEarningModel>> GetTotalEarning(HrmFilterType filterType);
}