using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using OfficeOpenXml;

namespace Infrastructure.ExportStrategies
{
    public class ExcelExportStrategy : IExportStrategy
    {
        public byte[] Export(List<StudentAcademicYear> data)
        {
            // Set the license context to NonCommercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Report");

            // Add school headers
            var school = data.First().School;
            worksheet.Cells[1, 1].Value = school.ReportHeaderOneEn;
            worksheet.Cells[2, 1].Value = school.ReportHeaderTwoEn;
            worksheet.Cells[3, 1].Value = school.ReportImage;
            worksheet.Cells[4, 1].Value = school.ReportHeaderOneAr;
            worksheet.Cells[5, 1].Value = school.ReportHeaderTwoAr;

            // Add class info
            worksheet.Cells[7, 1].Value = "Class:";
            worksheet.Cells[7, 2].Value = data.First().Classroom.Name;
            worksheet.Cells[7, 3].Value = "Number of Students:";
            worksheet.Cells[7, 4].Value = data.Count;
            worksheet.Cells[7, 5].Value = "Date:";
            worksheet.Cells[7, 6].Value = DateTime.UtcNow.ToString("yyyy-MM-dd");

            // Add headers
            worksheet.Cells[9, 1].Value = "No.";
            worksheet.Cells[9, 2].Value = "Name";
            worksheet.Cells[9, 3].Value = "Mobile";
            worksheet.Cells[9, 4].Value = "Nationality";
            worksheet.Cells[9, 5].Value = "Gender";

            // Add data
            int row = 10;
            int index = 1;
            foreach (var item in data)
            {
                worksheet.Cells[row, 1].Value = index;
                worksheet.Cells[row, 2].Value = item.Student.Name;
                worksheet.Cells[row, 3].Value = item.Student.MobileNumber;
                worksheet.Cells[row, 4].Value = item.Student.Nationality;
                worksheet.Cells[row, 5].Value = item.Student.Gender;
                row++;
                index++;
            }

            return package.GetAsByteArray();
        }

        public string GetContentType()
        {
            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }

        public string GetFileName()
        {
            return "report.xlsx";
        }
    }
}
