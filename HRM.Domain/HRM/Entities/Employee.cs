using System;
using System.Collections.Generic;

namespace HRM.Domain.HRM.Entities;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public uint EmployeeNumber { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public decimal Ssn { get; set; }

    public string? PayRate { get; set; }

    public int PayRatesIdPayRates { get; set; }

    public int? VacationDays { get; set; }

    public decimal? PaidToDate { get; set; }

    public decimal? PaidLastYear { get; set; }

    public virtual PayRate PayRatesIdPayRatesNavigation { get; set; } = null!;
}
