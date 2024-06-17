using System;
using System.Collections.Generic;

namespace HRM.Domain.Entities;

public partial class BenefitPlan
{
    public decimal BenefitPlansId { get; set; }

    public string? PlanName { get; set; }

    public decimal? Deductable { get; set; }

    public decimal? PercentageCopay { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
