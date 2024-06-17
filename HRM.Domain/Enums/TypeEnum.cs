using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Enums
{
    public enum MethodAPI : byte
    {
        GET = 0,
        POST = 1,
        PUT = 2,
        DELETE = 3
    }
    
    public enum NotificationType : byte
    {
        HiringAniverary = 0,
        LimitedNumberOfDatysVacation = 1,
        ChangeBenefitPlan = 2,
        EmployeeBirthDay = 3
    }

    public enum HrmFilterType
    {
        Shareholder,
        Gender,
        Ethnicity
    }
}
