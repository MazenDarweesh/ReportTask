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

            var exportData = data.Select((item, index) => new
            {
                No = index + 1,
                item.Student.Name,
                item.Student.MobileNumber,
                item.Student.Nationality,
                item.Student.Gender
            }).ToList();

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
