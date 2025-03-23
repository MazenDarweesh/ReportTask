using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Infrastructure.ExportStrategies
{
    public class PdfExportStrategy : IExportStrategy
    {
        public byte[] Export(List<StudentAcademicYear> data)
        {
            using var memoryStream = new MemoryStream();
            var document = new Document();
            var writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();

            // Add school headers
            var school = data.FirstOrDefault()?.School;
            var schoolHeaderTable = new PdfPTable(1);
            schoolHeaderTable.AddCell(school?.ReportHeaderOneEn ?? string.Empty);
            schoolHeaderTable.AddCell(school?.ReportHeaderTwoEn ?? string.Empty);
            schoolHeaderTable.AddCell(school?.ReportImage ?? string.Empty);
            schoolHeaderTable.AddCell(school?.ReportHeaderOneAr ?? string.Empty);
            schoolHeaderTable.AddCell(school?.ReportHeaderTwoAr ?? string.Empty);
            document.Add(schoolHeaderTable);

            // Add a blank line
            document.Add(new Paragraph(" "));

            // Add class info table
            var classInfoTable = new PdfPTable(3);
            classInfoTable.AddCell("Class:");
            classInfoTable.AddCell("Number of Students:");
            classInfoTable.AddCell("Date:");
            classInfoTable.AddCell(data.FirstOrDefault()?.Classroom?.Name ?? string.Empty);
            classInfoTable.AddCell(data.Count.ToString());
            classInfoTable.AddCell(DateTime.UtcNow.ToString("yyyy-MM-dd"));
            document.Add(classInfoTable);

            // Add a blank line
            document.Add(new Paragraph(" "));

            // Add student info table
            var table = new PdfPTable(5);
            table.AddCell("No.");
            table.AddCell("Name");
            table.AddCell("Mobile");
            table.AddCell("Nationality");
            table.AddCell("Gender");

            int index = 1;
            foreach (var item in data)
            {
                table.AddCell(index.ToString());
                table.AddCell(item.Student?.Name ?? string.Empty);
                table.AddCell(item.Student?.MobileNumber ?? string.Empty);
                table.AddCell(item.Student?.Nationality ?? string.Empty);
                table.AddCell(item.Student?.Gender ?? string.Empty);
                index++;
            }

            document.Add(table);
            document.Close();

            return memoryStream.ToArray();
        }

        public string GetContentType()
        {
            return "application/pdf";
        }

        public string GetFileName()
        {
            return "report.pdf";
        }
    }
}
