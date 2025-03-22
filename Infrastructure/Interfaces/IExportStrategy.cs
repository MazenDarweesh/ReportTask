using Domain.DTOs;
using Domain.Entities;



namespace Infrastructure.Interfaces
{
    public interface IExportStrategy
    {
        byte[] Export(List<StudentAcademicYear> data);
        string GetContentType();
        string GetFileName();
    }
}
