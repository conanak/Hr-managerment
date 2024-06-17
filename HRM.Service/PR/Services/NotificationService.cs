using HRM.Domain;
using HRM.Domain.Constants;
using HRM.Domain.Entities;
using HRM.Domain.Enums;
using HRM.Domain.Models;
using HRM.Service.HR.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace HRM.Service.HR.Services
{
    public class NotificationService : INotificationService
    {
        private readonly HrmContext _dbContext;
        private readonly int dateToday = DateTime.UtcNow.Day;
        private readonly int monthToday = DateTime.UtcNow.Month;
        private readonly int yearToday = DateTime.UtcNow.Year;
        public NotificationService(HrmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NotificationModel> GetAllNotificationsAsync()
        {
            var result = new NotificationModel()
            {
                Datas = new List<NotificationData>()
            };

            #region get employees birth date today
            var employeeBirthDates = await GetAllEmployeesHaveDateBirthToday();
            if (employeeBirthDates != null && employeeBirthDates.Any())
            {
                result.Datas.AddRange(GenerateNotificationContent(employeeBirthDates, NotificationType.EmployeeBirthDay));
            }
            #endregion

            #region get employees aniversal today
            var employeesAniversal = await GetAllEmployeesAniveral();
            if (employeesAniversal != null && employeesAniversal.Any())
            {
                result.Datas.AddRange(GenerateNotificationContent(employeesAniversal, NotificationType.HiringAniverary));
            }
            #endregion

            #region get employees limited vacations

            var employeesLimited = await GetEmployeesLimitedVacation();
            if (employeesLimited != null && employeesLimited.Any())
            {
                result.Datas.AddRange(GenerateNotificationContent(employeesLimited, NotificationType.LimitedNumberOfDatysVacation));
            }

            #endregion

            result.Datas = result.Datas.OrderBy(o => o.PublishedTime).ToList();
            result.TotalCount = result.Datas.Count;
            return result;
        }

        public async Task<List<EmployeeBirthDateModel>> GetAllEmployeesHaveDateBirthToday()
        {
            var result = new List<EmployeeBirthDateModel>();
            try
            {
                var allEmployees = await _dbContext.Employments
                .Include(p => p.Personal)
                .Where(e => e.Personal.BirthDate.Value.Day == dateToday
                            && e.Personal.BirthDate.Value.Month == monthToday)
                .Select(e => new EmployeeBirthDateModel
                {
                    BirthDate = e.Personal.BirthDate.Value,
                    EmployeeName = e.Personal.CurrentFirstName + e.Personal.CurrentMiddleName + e.Personal.CurrentLastName,
                    Ages = yearToday - e.Personal.BirthDate.Value.Year
                })
                .ToListAsync();
                if (allEmployees != null && allEmployees.Any())
                {
                    result.AddRange(allEmployees);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("error when get all employee has birth date today " + e.Message);
            }
            
            return result;
        }

        public async Task<List<EmployeeAniveralModel>> GetAllEmployeesAniveral()
        {
            var result = new List<EmployeeAniveralModel>();
            try
            {
                var allEmps = await _dbContext.Employments
                                .Where(e => e.HireDateForWorking.Value.Day == dateToday
                                            && e.HireDateForWorking.Value.Month == monthToday)
                                .Include(p => p.Personal)
                                .Select(e => new EmployeeAniveralModel
                                {
                                    EmployeeName = e.Personal.CurrentFirstName + e.Personal.CurrentMiddleName + e.Personal.CurrentLastName,
                                    AniveralYears = yearToday - e.HireDateForWorking.Value.Year
                                }).ToListAsync();
                if (allEmps != null  && allEmps.Any())
                {
                    result.AddRange(allEmps);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error when get all employee has Aniveral today " + e.Message);
            }
            return result;
        }

        public async Task<List<EmployeesLimitedVacationModel>> GetEmployeesLimitedVacation()
        {
            var result = new List<EmployeesLimitedVacationModel>();
            try
            {
                var allEmpsLimit = await _dbContext.Employments.Include(p => p.Personal)
                               .Include(e => e.EmploymentWorkingTimes).ToListAsync();

                #region get vacations day from OpenAPI from HR

                HttpClient client = new HttpClient();
                var allEmployeesId = allEmpsLimit.Select(x => x.EmploymentId).ToList();
                string jsonData = JsonConvert.SerializeObject(allEmployeesId);
                string urlAPI = $"{RoutesAPI_HRM.RootHRM_APIUrl}{RoutesAPI_PR.GetEmployeesVacations}";
                var responseAPI = CommonUIService.GetDataAPI(urlAPI, MethodAPI.POST, jsonData);
                #endregion

                if (responseAPI.IsSuccessStatusCode)
                {
                    var dataResponse = await responseAPI.Content.ReadAsStringAsync();
                    var employeesHR = JsonConvert.DeserializeObject<List<EmployeesVactionModel>>(dataResponse);
                    if (employeesHR != null && employeesHR.Count > 0)
                    {
                        foreach (var employee in employeesHR)
                        {
                            var currentEmployee = allEmpsLimit
                                                        .Where(x => x.EmploymentId == employee.EmployeeId)
                                                        .FirstOrDefault();
                            if (currentEmployee != null)
                            {
                                var currentWorkingTime = currentEmployee.EmploymentWorkingTimes
                                                        .Where(w => w.YearWorking.Value.Year == yearToday && w.MonthWorking == monthToday)
                                                        .Select(w => w.TotalNumberVacationWorkingDaysPerMonth).FirstOrDefault();
                                if (currentWorkingTime >= employee.VactionDays)
                                {
                                    result.Add(new EmployeesLimitedVacationModel
                                    {
                                        EmployeeName = employee.EmployeeName,
                                        NumberOfVacation = employee.VactionDays
                                    });
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when get all employees limited vacation" + ex.Message);
            }
            return result;
        }

        private List<NotificationData> GenerateNotificationContent<T>(List<T> employees, NotificationType notificationType)
        {
            var result = new List<NotificationData>();
            switch (notificationType)
            {
                case NotificationType.EmployeeBirthDay:
                    var employeesBirthDates = employees as List<EmployeeBirthDateModel>;
                    if (employeesBirthDates != null && employeesBirthDates.Count > 0)
                    {
                        foreach (var employee in employeesBirthDates)
                        {
                            var notificationData = new NotificationData()
                            {
                                PublishedTime = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                                Type = NotificationType.EmployeeBirthDay,
                            };
                            string content = string.Format(NotificationContent.EmployeeBirthDayTitle, employee.EmployeeName, employee.Ages);
                            notificationData.Content = content;
                            result.Add(notificationData);
                        }
                    }
                    break;
                case NotificationType.HiringAniverary:
                    var employeesAniveral = employees as List<EmployeeAniveralModel>;
                    if (employeesAniveral != null)
                    {
                        foreach (var employee in employeesAniveral)
                        {
                            var notificationData = new NotificationData()
                            {
                                PublishedTime = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                                Type = NotificationType.HiringAniverary,
                            };
                            string content = string.Format(NotificationContent.HiringAniveraryTitle, employee.AniveralYears, employee.EmployeeName);
                            notificationData.Content = content;
                            result.Add(notificationData);
                        }
                    }
                    break;
                case NotificationType.LimitedNumberOfDatysVacation:
                    var employeesVacations = employees as List<EmployeesLimitedVacationModel>;
                    if (employeesVacations != null)
                    {
                        foreach (var employee in employeesVacations)
                        {
                            var notificationData = new NotificationData()
                            {
                                PublishedTime = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                                Type = NotificationType.LimitedNumberOfDatysVacation,
                            };
                            string content = string.Format(NotificationContent.LimitedNumberOfDatysVacationTitle, employee.EmployeeName, employee.NumberOfVacation);
                            notificationData.Content = content;
                            result.Add(notificationData);
                        }
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}
