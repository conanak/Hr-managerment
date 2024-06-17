using HRM.Domain.Entities;
using HRM.Domain.Enums;
using HRM.Domain.Models;
using HRM.Service.PR.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRM.Service.PR.Services;

public class VacationService : IVacationService
{
    private readonly HrmContext _context;

    public VacationService(HrmContext context)
    {
        _context = context;
    }

    public async Task<List<TotalVacationDaysTakenModel>> GetTotalVacationDaysTaken(HrmFilterType filterType)
    {
        var result = new List<TotalVacationDaysTakenModel>();
        var employments = await _context.Employments
            .Include(e => e.Personal)
            .Include(e => e.EmploymentWorkingTimes)
            .ToListAsync();

        switch (filterType)
        {
            case HrmFilterType.Shareholder:
                var employmentGroupByShareholderStat = employments
                    .GroupBy(
                        e => e.Personal.ShareholderStatus,
                        e => e.EmploymentId,
                        (shareholderStat, ids) => new
                        {
                            ShareHolder = shareholderStat,
                            EmploymentIds = ids.ToList()
                        })
                    .ToList();
                foreach (var item in employmentGroupByShareholderStat)
                {
                    var totalVacationDaysToDate = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.MonthWorking == DateTime.Now.Month &&
                                    e.YearWorking.Value.Year == DateTime.Now.Year)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    var totalVacationDaysPrevYear = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.YearWorking.Value.Year == DateTime.Now.Year - 1)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    result.Add(new TotalVacationDaysTakenModel
                    {
                        Name = item.ShareHolder == 1 ? "Shareholder" : "Non-shareholder",
                        ToDateValue = (decimal)totalVacationDaysToDate,
                        PrevYearValue = (decimal)totalVacationDaysPrevYear
                    });
                }

                break;
            case HrmFilterType.Gender:
                var employmentGroupByGender = employments
                    .GroupBy(
                        e => e.Personal.CurrentGender,
                        e => e.EmploymentId,
                        (gender, ids) => new
                        {
                            Gender = gender,
                            EmploymentIds = ids.ToList()
                        })
                    .ToList();
                foreach (var item in employmentGroupByGender)
                {
                    var totalVacationDaysToDate = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.MonthWorking == DateTime.Now.Month &&
                                    e.YearWorking.Value.Year == DateTime.Now.Year)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    var totalVacationDaysPrevYear = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.YearWorking.Value.Year == DateTime.Now.Year - 1)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    result.Add(new TotalVacationDaysTakenModel
                    {
                        Name = item.Gender,
                        ToDateValue = (decimal)totalVacationDaysToDate,
                        PrevYearValue = (decimal)totalVacationDaysPrevYear
                    });
                }

                break;
            case HrmFilterType.Ethnicity:
                var employmentGroupByEthnicity = employments
                    .GroupBy(
                        e => e.Personal.Ethnicity,
                        e => e.EmploymentId,
                        (ethnicity, ids) => new
                        {
                            Ethnicity = ethnicity,
                            EmploymentIds = ids.ToList()
                        })
                    .ToList();
                foreach (var item in employmentGroupByEthnicity)
                {
                    var totalVacationDaysToDate = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.MonthWorking == DateTime.Now.Month &&
                                    e.YearWorking.Value.Year == DateTime.Now.Year)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    var totalVacationDaysPrevYear = employments
                        .Where(e => item.EmploymentIds.Contains(e.EmploymentId))
                        .SelectMany(e => e.EmploymentWorkingTimes)
                        .Where(e => e.YearWorking.Value.Year == DateTime.Now.Year - 1)
                        .Sum(e => e.TotalNumberVacationWorkingDaysPerMonth);

                    result.Add(new TotalVacationDaysTakenModel
                    {
                        Name = item.Ethnicity,
                        ToDateValue = (decimal)totalVacationDaysToDate,
                        PrevYearValue = (decimal)totalVacationDaysPrevYear
                    });
                }

                break;
        }

        return result;
    }
}