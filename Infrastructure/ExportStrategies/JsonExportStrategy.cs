using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.ExportStrategies
{
    public class JsonExportStrategy : IExportStrategy
    {
        public byte[] Export(List<StudentAcademicYear> data)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var school = data.FirstOrDefault()?.School;
            var exportData = new
            {
                SchoolHeader = new
                {
                    ReportHeaderOneEn = school?.ReportHeaderOneEn,
                    ReportHeaderTwoEn = school?.ReportHeaderTwoEn,
                    ReportImage = school?.ReportImage,
                    ReportHeaderOneAr = school?.ReportHeaderOneAr,
                    ReportHeaderTwoAr = school?.ReportHeaderTwoAr
                },
                Students = data.Select((item, index) => new
                {
                    No = index + 1,
                    Name = item.Student?.Name,
                    MobileNumber = item.Student?.MobileNumber,
                    Nationality = item.Student?.Nationality,
                    Gender = item.Student?.Gender
                }).ToList()
            };

            return JsonSerializer.SerializeToUtf8Bytes(exportData, options);
        }

        public string GetContentType()
        {
            return "application/json";
        }

        public string GetFileName()
        {
            return "report.json";
        }
    }
}
