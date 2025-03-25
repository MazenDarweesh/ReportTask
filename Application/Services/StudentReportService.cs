using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class StudentReportService : IStudentReportService
    {
        private readonly IStudentReportRepository _studentReportRepository;
        private readonly Dictionary<ExportFormat, IExportStrategy> _exportStrategies;

        public StudentReportService(
            IStudentReportRepository studentReportRepository,
            IDictionary<ExportFormat, IExportStrategy> exportStrategies)
        {
            _studentReportRepository = studentReportRepository;
            _exportStrategies = new Dictionary<ExportFormat, IExportStrategy>(exportStrategies);
        }

        public async Task<(object? reportData, List<StudentAcademicYear>? rawData)> GetReportData(string? schoolId, string? yearId, string? gradeId, string? classId)
        {
            var studentAcademicYears = await _studentReportRepository.GetFilteredStudentAcademicYearsAsync(schoolId, yearId, gradeId, classId);
            if (!studentAcademicYears.Any()) return (null, null);

            var groupedData = studentAcademicYears
                .GroupBy(say => new { say.Classroom.Id, say.Classroom.Name })
                .Select(g => new
                {
                    ClassName = g.Key.Name,
                    StudentCount = g.Count(),
                    Date = System.DateTime.UtcNow,
                    Students = g.Select(say => new
                    {
                        say.Student.Name,
                        say.Student.MobileNumber,
                        say.Student.Nationality,
                        say.Student.Gender
                    }).ToList()
                })
                .ToList();

            var school = studentAcademicYears.First().School;

            var reportData = new
            {
                SchoolHeader = new
                {
                    school.ReportHeaderOneEn,
                    school.ReportHeaderTwoEn,
                    school.ReportImage,
                    school.ReportHeaderOneAr,
                    school.ReportHeaderTwoAr
                },
                ReportData = groupedData
            };

            return (reportData, studentAcademicYears);
        }

        public string GetContentType(ExportFormat format)
        {
            if (!_exportStrategies.TryGetValue(format, out var exportStrategy))
            {
                throw new ArgumentException($"Export format {format} is not supported.");
            }

            return exportStrategy.GetContentType();
        }

        public string GetFileName(ExportFormat format)
        {
            if (!_exportStrategies.TryGetValue(format, out var exportStrategy))
            {
                throw new ArgumentException($"Export format {format} is not supported.");
            }

            return exportStrategy.GetFileName();
        }

        public byte[] ExportReport(ExportFormat format, List<StudentAcademicYear> data)
        {
            if (!_exportStrategies.TryGetValue(format, out var exportStrategy))
            {
                throw new ArgumentException($"Export format {format} is not supported.");
            }

            return exportStrategy.Export(data);
        }
    }
}