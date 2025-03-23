using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Infrastructure.ExportStrategies;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using System.Text.Json.Serialization;
using Infrastructure;

namespace ReportTask.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddRepositories();
            services.AddServices();
            services.AddControllerServices();
            services.AddSwaggerServices();
            services.AddStudentReportServices();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
            }
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentReportRepository, StudentReportRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IAcademicYearService, AcademicYearService>();
            services.AddScoped<IGradeService, GradeService>();
            services.AddScoped<IClassroomService, ClassroomService>();
            services.AddScoped<IStudentReportService, StudentReportService>();
            return services;
        }

        public static IServiceCollection AddControllerServices(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    });
            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddStudentReportServices(this IServiceCollection services)
        {
            // Register export strategies
            services.AddScoped<PdfExportStrategy>();
            services.AddScoped<ExcelExportStrategy>();
            services.AddScoped<JsonExportStrategy>();

            // Register strategies dictionary
            services.AddScoped<IDictionary<ExportFormat, IExportStrategy>>(provider =>
            {
                var strategies = new Dictionary<ExportFormat, IExportStrategy>
                {
                    { ExportFormat.Pdf, provider.GetRequiredService<PdfExportStrategy>() },
                    { ExportFormat.Excel, provider.GetRequiredService<ExcelExportStrategy>() },
                    { ExportFormat.Json, provider.GetRequiredService<JsonExportStrategy>() }
                };
                return strategies;
            });

            return services;
        }
    }
}
