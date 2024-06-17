namespace HRM.Domain.Models;

public class TotalVacationDaysTakenModel
{
    public string Name { get; set; }

    public decimal ToDateValue { get; set; }
    
    public decimal PrevYearValue { get; set; }
}