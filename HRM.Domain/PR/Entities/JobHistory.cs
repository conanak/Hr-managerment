using System;
using System.Collections.Generic;

namespace HRM.Domain.Entities;

public partial class JobHistory
{
    public decimal JobHistoryId { get; set; }

    public decimal? EmploymentId { get; set; }

    public string? Department { get; set; }

    public string? Division { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ThruDate { get; set; }

    public string? JobTitle { get; set; }

    public string? Supervisor { get; set; }

    public string? Location { get; set; }

    public short? TypeOfWork { get; set; }

    public virtual Employment? Employment { get; set; }
}
