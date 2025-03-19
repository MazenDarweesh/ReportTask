using Domain.DTOs;



namespace Infrastructure.Interfaces
{
    public interface IAcademicYearService
    {
        Task<List<AcademicYearDTO>> GetAcademicYearsAsync(string schoolId);
    }
}
