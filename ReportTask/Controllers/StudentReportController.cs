using Application.Interfaces;
using Domain.Enums;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ReportTask.API.Controllers;

[ApiController]
[Route("api/studentreport")]
public class StudentReportController : ControllerBase
{
    private readonly IStudentReportService _reportService;

    public StudentReportController(IStudentReportService reportService)
    {
        _reportService = reportService;
    }

    /// <summary>
    /// Get student report data
    /// </summary>
    [HttpGet("report")]
    public async Task<IActionResult> GetReport([FromQuery] string? schoolId, [FromQuery] string? yearId,
                                               [FromQuery] string? gradeId, [FromQuery] string? classId)
    {
        var (reportData, _) = await _reportService.GetReportData(schoolId, yearId, gradeId, classId);
        if (reportData == null) return NotFound("No students found.");

        return Ok(reportData);
    }

    /// <summary>
    /// Export student report in specified format
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> ExportReport([FromQuery] ExportFormat format,
                                                 [FromQuery] string? schoolId,
                                                 [FromQuery] string? yearId,
                                                 [FromQuery] string? gradeId,
                                                 [FromQuery] string? classId)
    {
        var (_, rawData) = await _reportService.GetReportData(schoolId, yearId, gradeId, classId);
        if (rawData == null || !rawData.Any()) return NotFound("No students found.");

        var reportBytes = _reportService.ExportReport(format, rawData);
        return File(reportBytes, _reportService.GetContentType(format), _reportService.GetFileName(format));
    }
}