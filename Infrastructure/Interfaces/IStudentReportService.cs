using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;



namespace Infrastructure.Interfaces
{
    public interface IStudentReportService
    {
        Task<(object reportData, List<StudentAcademicYear> rawData)> GetReportData(string? schoolId, string? yearId, string? gradeId, string? classId);
        string GetContentType(ExportFormat format);
        string GetFileName(ExportFormat format);
        byte[] ExportReport(ExportFormat format, List<StudentAcademicYear> data);
    }
}
