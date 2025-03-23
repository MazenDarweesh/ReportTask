using Application.Interfaces;
using Application.Services;
using Domain.Enums;
using Infrastructure;
using Infrastructure.ExportStrategies;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();


// Add the StudentReportService and its dependencies
builder.Services.AddStudentReportServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public static class ServiceExtensions
{
    public static IServiceCollection AddStudentReportServices(this IServiceCollection services)
    {
        // Register repository
        services.AddScoped<IStudentReportRepository, StudentReportRepository>();

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

        // Register the report service
        services.AddScoped<IStudentReportService, StudentReportService>();

        return services;
    }
}
