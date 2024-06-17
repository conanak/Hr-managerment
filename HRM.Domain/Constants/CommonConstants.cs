namespace HRM.Domain.Constants
{
    public static class ConfigurationKey
    {
        public const string HRMConnectionString = "HRMConnectStr";
        public const string MySqlConnectionString = "MySqlConnectStr";
    }
    public static class RoutesAPI_HRM
    {
        public const string RootHRM_APIUrl = "http://localhost:5247/api/";
        public const string GetListEmploymentsIncludePersonal = "OpenApiPr/get-list-employments-include-personal";
        public const string GetListEmploymentsIncludeWorkingTime = "OpenApiPr/get-list-employment-include-working-time";
    }
    public static class RoutesAPI_PR
    {
        public const string RootPR_APIUrl = "http://localhost:5097/api/";
        public const string GetAllNotifications = "notification/getall";
        public const string GetEmployeesVacations = "openapi/hr/getallvacations";
    }
}
