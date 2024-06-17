using HRM.Domain;
using HRM.Domain.Constants;
using HRM.Domain.Entities;
using HRM.Domain.Enums;
using HRM.Domain.HRM.Entities;
using HRM.Domain.Models;
using HRM.Service.HRM.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HRM.Service.HRM.Services;

public class EmployeesService : IEmployeesService
{
    private readonly MydbContext _context;

    public EmployeesService(MydbContext context)
    {
        _context = context;
    }

    public async Task<List<TotalEarningModel>> GetTotalEarning(HrmFilterType filterType)
    {
        var result = new List<TotalEarningModel>();

        var employees = await _context.Employees.ToListAsync();

        const string apiUrl = $"{RoutesAPI_PR.RootPR_APIUrl}{RoutesAPI_HRM.GetListEmploymentsIncludePersonal}";
        var apiResponse = CommonUIService.GetDataAPI(apiUrl, MethodAPI.GET);

        if (apiResponse.IsSuccessStatusCode)
        {
            var dataResponse = await apiResponse.Content.ReadAsStringAsync();
            var employments = JsonConvert.DeserializeObject<List<Employment>>(dataResponse);

            if (employments is not null && employments.Count > 0)
            {
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
                            var totalEarningToDate = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidToDate);
                            var totalEarningLastYear = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidLastYear);

                            result.Add(new TotalEarningModel
                            {
                                Name = item.ShareHolder == 1 ? "Shareholder" : "Non-shareholder",
                                ToDateValue = (decimal)totalEarningToDate,
                                PrevYearValue = (decimal)totalEarningLastYear
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
                            var totalEarningToDate = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidToDate);
                            var totalEarningLastYear = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidLastYear);

                            result.Add(new TotalEarningModel
                            {
                                Name = item.Gender,
                                ToDateValue = (decimal)totalEarningToDate,
                                PrevYearValue = (decimal)totalEarningLastYear
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
                            var totalEarningToDate = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidToDate);
                            var totalEarningLastYear = employees
                                .Where(e => item.EmploymentIds.Contains(e.IdEmployee))
                                .Sum(e => e.PaidLastYear);

                            result.Add(new TotalEarningModel
                            {
                                Name = item.Ethnicity,
                                ToDateValue = (decimal)totalEarningToDate,
                                PrevYearValue = (decimal)totalEarningLastYear
                            });
                        }

                        break;
                }
            }
        }

        return result;
    }
}