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

            // Add class info
            worksheet.Cells[1, 1].Value = "Class:";
            worksheet.Cells[1, 2].Value = data.First().Classroom.Name;
            worksheet.Cells[1, 3].Value = "Number of Students:";
            worksheet.Cells[1, 4].Value = data.Count;
            worksheet.Cells[1, 5].Value = "Date:";
            worksheet.Cells[1, 6].Value = DateTime.UtcNow.ToString("yyyy-MM-dd");

            // Add headers
            worksheet.Cells[3, 1].Value = "No.";
            worksheet.Cells[3, 2].Value = "Name";
            worksheet.Cells[3, 3].Value = "Mobile";
            worksheet.Cells[3, 4].Value = "Nationality";
            worksheet.Cells[3, 5].Value = "Gender";

            // Add data
            int row = 4;
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
