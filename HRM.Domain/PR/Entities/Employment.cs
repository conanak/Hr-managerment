using System;
using System.Collections.Generic;

namespace HRM.Domain.Entities;

public partial class Employment
{
    public decimal EmploymentId { get; set; }

    public string? EmploymentCode { get; set; }

    public string? EmploymentStatus { get; set; }

    public DateOnly? HireDateForWorking { get; set; }

    /// <summary>
    /// MÃ CÔNG VIỆC
    /// </summary>
    public string? WorkersCompCode { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public DateOnly? RehireDateForWorking { get; set; }

    public DateOnly? LastReviewDate { get; set; }

    public decimal? NumberDaysRequirementOfWorkingPerMonth { get; set; }

    public decimal? PersonalId { get; set; }

    public virtual ICollection<EmploymentWorkingTime> EmploymentWorkingTimes { get; set; } = new List<EmploymentWorkingTime>();

    public virtual ICollection<JobHistory> JobHistories { get; set; } = new List<JobHistory>();

    public virtual Personal? Personal { get; set; }
}
