

using Domain.DTOs;

namespace Infrastructure.Interfaces
{
    public interface ISchoolService
    {
        Task<List<SchoolDTO>> GetSchoolsAsync();
    }
}
