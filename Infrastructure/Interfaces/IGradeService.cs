

using Domain.DTOs;

namespace Infrastructure.Interfaces
{
    public interface IGradeService
    {
        Task<List<GradeDTO>> GetGradesAsync();
    }
}
