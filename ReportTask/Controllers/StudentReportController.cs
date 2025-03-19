using Domain.Entities;
using Infrastructure;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ReportTask.API.Controllers;

[ApiController]
[Route("api/studentreport")]
public class StudentReportController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentReportController(AppDbContext context)
    {
        _context = context;
    }

    /// 🔹 **Filter and Get Report**
    [HttpGet("report")]
    public async Task<IActionResult> GetReport([FromQuery] string? schoolId, [FromQuery] string? yearId,
                                               [FromQuery] string? gradeId, [FromQuery] string? classId)
    {
        var studentAcademicYears = await GetFilteredStudentAcademicYears(schoolId, yearId, gradeId, classId);
        if (!studentAcademicYears.Any()) return NotFound("No students found.");

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

        var result = new
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

        return Ok(result);
    }

    /// 🔹 **Export Report as PDF**
    [HttpGet("export/pdf")]
    public async Task<IActionResult> ExportToPdf([FromQuery] string? schoolId, [FromQuery] string? yearId,
                                                 [FromQuery] string? gradeId, [FromQuery] string? classId)
    {
        var studentAcademicYears = await GetFilteredStudentAcademicYears(schoolId, yearId, gradeId, classId);
        if (!studentAcademicYears.Any()) return NotFound("No students found.");

        using (var stream = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, stream);
            document.Open();
            document.Add(new Paragraph("Student Report"));

            PdfPTable table = new PdfPTable(4);
            table.AddCell("Name");
            table.AddCell("Mobile");
            table.AddCell("Nationality");
            table.AddCell("Gender");

            foreach (var say in studentAcademicYears)
            {
                table.AddCell(say.Student.Name);
                table.AddCell(say.Student.MobileNumber);
                table.AddCell(say.Student.Nationality);
                table.AddCell(say.Student.Gender);
            }

            document.Add(table);
            document.Close();

            return File(stream.ToArray(), "application/pdf", "StudentReport.pdf");
        }
    }

    /// 🔹 **Export Report as Excel**
    [HttpGet("export/excel")]
    public async Task<IActionResult> ExportToExcel([FromQuery] string? schoolId, [FromQuery] string? yearId,
                                                   [FromQuery] string? gradeId, [FromQuery] string? classId)
    {
        var studentAcademicYears = await GetFilteredStudentAcademicYears(schoolId, yearId, gradeId, classId);
        if (!studentAcademicYears.Any()) return NotFound("No students found.");

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Students");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Mobile";
            worksheet.Cells[1, 3].Value = "Nationality";
            worksheet.Cells[1, 4].Value = "Gender";

            int row = 2;
            foreach (var say in studentAcademicYears)
            {
                worksheet.Cells[row, 1].Value = say.Student.Name;
                worksheet.Cells[row, 2].Value = say.Student.MobileNumber;
                worksheet.Cells[row, 3].Value = say.Student.Nationality;
                worksheet.Cells[row, 4].Value = say.Student.Gender;
                row++;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StudentReport.xlsx");
        }
    }

    /// 🔹 **Export Report for Print (JSON)**
    [HttpGet("export/print")]
    public async Task<IActionResult> ExportToPrint([FromQuery] string? schoolId, [FromQuery] string? yearId,
                                                   [FromQuery] string? gradeId, [FromQuery] string? classId)
    {
        var studentAcademicYears = await GetFilteredStudentAcademicYears(schoolId, yearId, gradeId, classId);
        if (!studentAcademicYears.Any()) return NotFound("No students found.");
        return Ok(studentAcademicYears);
    }

    /// 🔹 **Helper Method to Filter Student Academic Years**
    private async Task<List<StudentAcademicYear>> GetFilteredStudentAcademicYears(string? schoolId, string? yearId,
                                                                                  string? gradeId, string? classId)
    {
        return await _context.StudentAcademicYears
            .Include(say => say.Student)
            .Include(say => say.School)
            .Include(say => say.Classroom)
            .Include(say => say.Grade)
            .Include(say => say.Semester)
            .Where(say =>
                (string.IsNullOrEmpty(schoolId) || say.SchoolId.ToString() == schoolId) &&
                (string.IsNullOrEmpty(yearId) || say.Semester.AcademicYearId.ToString() == yearId) &&
                (string.IsNullOrEmpty(gradeId) || say.GradeId.ToString() == gradeId) &&
                (string.IsNullOrEmpty(classId) || say.ClassroomId.ToString() == classId))
            .ToListAsync();
    }
}
