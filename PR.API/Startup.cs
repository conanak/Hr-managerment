using System.Text.Json.Serialization;
using HRM.Domain.Constants;
using HRM.Domain.Entities;
using HRM.Service.HR.Interfaces;
using HRM.Service.HR.Services;
using HRM.Service.HRM.Interfaces;
using HRM.Service.HRM.Services;
using HRM.Service.PR.Interfaces;
using HRM.Service.PR.Services;
using Microsoft.EntityFrameworkCore;

namespace PR.API
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
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(p => p.AddDefaultPolicy(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddDbContext<HrmContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(ConfigurationKey.HRMConnectionString));
            });
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IOpenApiPrService, OpenApiPrService>();
            services.AddTransient<IVacationService, VacationService>();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payrolment Department API");
                });
            }
            app.UseCors(build =>
            {
                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.MapControllers();
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

        }
    }
}
