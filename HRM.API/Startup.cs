using HRM.Domain.Constants;
using HRM.Domain.Entities;
using HRM.Domain.HRM.Entities;
using HRM.Service.HRM.Interfaces;
using HRM.Service.HRM.Services;
using HRM.Service.PR.Interfaces;
using HRM.Service.PR.Services;
using HRM.Domain.HRM.Entities;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HRM.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(p => p.AddDefaultPolicy(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddHealthChecks();
            services.AddDbContext<MydbContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString(ConfigurationKey.MySqlConnectionString));
            });
            services.AddTransient<IOpenAPIHRService, OpenAPIHRService>();
            services.AddTransient<IEmployeesService, EmployeesService>();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseAuthorization();

            app.MapControllers();

            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                },
                AllowCachingResponses = true,
            });

        }
    }
}
