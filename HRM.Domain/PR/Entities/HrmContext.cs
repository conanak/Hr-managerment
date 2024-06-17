using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HRM.Domain.Entities;

public partial class HrmContext : DbContext
{
    public HrmContext()
    {
    }

    public HrmContext(DbContextOptions<HrmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BenefitPlan> BenefitPlans { get; set; }

    public virtual DbSet<Employment> Employments { get; set; }

    public virtual DbSet<EmploymentWorkingTime> EmploymentWorkingTimes { get; set; }

    public virtual DbSet<JobHistory> JobHistories { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BenefitPlan>(entity =>
        {
            entity.HasKey(e => e.BenefitPlansId);

            entity.ToTable("BENEFIT_PLANS");

            entity.Property(e => e.BenefitPlansId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BENEFIT_PLANS_ID");
            entity.Property(e => e.Deductable)
                .HasColumnType("money")
                .HasColumnName("DEDUCTABLE");
            entity.Property(e => e.PercentageCopay)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PERCENTAGE_COPAY");
            entity.Property(e => e.PlanName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("PLAN_NAME");
        });

        modelBuilder.Entity<Employment>(entity =>
        {
            entity.ToTable("EMPLOYMENT");

            entity.Property(e => e.EmploymentId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("EMPLOYMENT_ID");
            entity.Property(e => e.EmploymentCode)
                .HasMaxLength(50)
                .HasColumnName("EMPLOYMENT_CODE");
            entity.Property(e => e.EmploymentStatus)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("EMPLOYMENT_STATUS");
            entity.Property(e => e.HireDateForWorking).HasColumnName("HIRE_DATE_FOR_WORKING");
            entity.Property(e => e.LastReviewDate).HasColumnName("LAST_REVIEW_DATE");
            entity.Property(e => e.NumberDaysRequirementOfWorkingPerMonth)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("NUMBER_DAYS_REQUIREMENT_OF_WORKING_PER_MONTH");
            entity.Property(e => e.PersonalId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.RehireDateForWorking).HasColumnName("REHIRE_DATE_FOR_WORKING");
            entity.Property(e => e.TerminationDate).HasColumnName("TERMINATION_DATE");
            entity.Property(e => e.WorkersCompCode)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasComment("MÃ CÔNG VIỆC")
                .HasColumnName("WORKERS_COMP_CODE");

            
        });

        modelBuilder.Entity<EmploymentWorkingTime>(entity =>
        {
            entity.ToTable("EMPLOYMENT_WORKING_TIME");

            entity.Property(e => e.EmploymentWorkingTimeId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("EMPLOYMENT_WORKING_TIME_ID");
            entity.Property(e => e.EmploymentId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("EMPLOYMENT_ID");
            entity.Property(e => e.MonthWorking)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("MONTH_WORKING");
            entity.Property(e => e.NumberDaysActualOfWorkingPerMonth)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("NUMBER_DAYS_ACTUAL_OF_WORKING_PER_MONTH");
            entity.Property(e => e.TotalNumberVacationWorkingDaysPerMonth)
                .HasColumnType("numeric(2, 0)")
                .HasColumnName("TOTAL_NUMBER_VACATION_WORKING_DAYS_PER_MONTH");
            entity.Property(e => e.YearWorking).HasColumnName("YEAR_WORKING");

            entity.HasOne(d => d.Employment).WithMany(p => p.EmploymentWorkingTimes)
                .HasForeignKey(d => d.EmploymentId)
                .HasConstraintName("FK_EMPLOYMENT_WORKING_TIME_EMPLOYMENT");
        });

        modelBuilder.Entity<JobHistory>(entity =>
        {
            entity.ToTable("JOB_HISTORY");

            entity.Property(e => e.JobHistoryId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("JOB_HISTORY_ID");
            entity.Property(e => e.Department)
                .HasMaxLength(250)
                .HasColumnName("DEPARTMENT");
            entity.Property(e => e.Division)
                .HasMaxLength(250)
                .HasColumnName("DIVISION");
            entity.Property(e => e.EmploymentId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("EMPLOYMENT_ID");
            entity.Property(e => e.FromDate).HasColumnName("FROM_DATE");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(250)
                .HasColumnName("JOB_TITLE");
            entity.Property(e => e.Location)
                .HasMaxLength(250)
                .HasColumnName("LOCATION");
            entity.Property(e => e.Supervisor)
                .HasMaxLength(250)
                .HasColumnName("SUPERVISOR");
            entity.Property(e => e.ThruDate).HasColumnName("THRU_DATE");
            entity.Property(e => e.TypeOfWork).HasColumnName("TYPE_OF_WORK");

            entity.HasOne(d => d.Employment).WithMany(p => p.JobHistories)
                .HasForeignKey(d => d.EmploymentId)
                .HasConstraintName("FK_JOB_HISTORY_EMPLOYMENT");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.ToTable("PERSONAL");

            entity.Property(e => e.PersonalId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("PERSONAL_ID");
            entity.Property(e => e.BenefitPlanId)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("BENEFIT_PLAN_ID");
            entity.Property(e => e.BirthDate).HasColumnName("BIRTH_DATE");
            entity.Property(e => e.CurrentAddress1)
                .HasMaxLength(255)
                .HasColumnName("CURRENT_ADDRESS_1");
            entity.Property(e => e.CurrentAddress2)
                .HasMaxLength(255)
                .HasColumnName("CURRENT_ADDRESS_2");
            entity.Property(e => e.CurrentCity)
                .HasMaxLength(100)
                .HasColumnName("CURRENT_CITY");
            entity.Property(e => e.CurrentCountry)
                .HasMaxLength(100)
                .HasColumnName("CURRENT_COUNTRY");
            entity.Property(e => e.CurrentFirstName)
                .HasMaxLength(50)
                .HasColumnName("CURRENT_FIRST_NAME");
            entity.Property(e => e.CurrentGender)
                .HasMaxLength(20)
                .HasColumnName("CURRENT_GENDER");
            entity.Property(e => e.CurrentLastName).HasColumnName("CURRENT_LAST_NAME");
            entity.Property(e => e.CurrentMaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("CURRENT_MARITAL_STATUS");
            entity.Property(e => e.CurrentMiddleName)
                .HasMaxLength(50)
                .HasColumnName("CURRENT_MIDDLE_NAME");
            entity.Property(e => e.CurrentPersonalEmail)
                .HasMaxLength(50)
                .HasColumnName("CURRENT_PERSONAL_EMAIL");
            entity.Property(e => e.CurrentPhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("CURRENT_PHONE_NUMBER");
            entity.Property(e => e.CurrentZip)
                .HasColumnType("numeric(18, 0)")
                .HasColumnName("CURRENT_ZIP");
            entity.Property(e => e.DriversLicense)
                .HasMaxLength(50)
                .HasColumnName("DRIVERS_LICENSE");
            entity.Property(e => e.Ethnicity)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ETHNICITY");
            entity.Property(e => e.ShareholderStatus).HasColumnName("SHAREHOLDER_STATUS");
            entity.Property(e => e.SocialSecurityNumber)
                .HasMaxLength(20)
                .HasColumnName("SOCIAL_SECURITY_NUMBER");

            entity.HasOne(d => d.BenefitPlan).WithMany(p => p.Personals)
                .HasForeignKey(d => d.BenefitPlanId)
                .HasConstraintName("FK_PERSONAL_BENEFIT_PLANS");
            entity.HasOne(d => d.Employment).WithOne(p => p.Personal)
                .HasForeignKey<Employment>(p => p.PersonalId)
                .HasConstraintName("FK_EMPLOYMENT_PERSONAL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
