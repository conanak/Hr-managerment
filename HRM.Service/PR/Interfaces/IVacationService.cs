using HRM.Domain.Enums;
using HRM.Domain.Models;

namespace HRM.Service.PR.Interfaces;

public interface IVacationService
{
    Task<List<TotalVacationDaysTakenModel>> GetTotalVacationDaysTaken(HrmFilterType filterType);
}