
using Domain.DTOs;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class AcademicYearService : IAcademicYearService
    {
        private readonly AppDbContext _context;

        public AcademicYearService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AcademicYearDTO>> GetAcademicYearsAsync(string schoolId)
        {
            return await _context.AcademicYears
                .Where(ay => ay.SchoolId.ToString() == schoolId)
                .Select(ay => new AcademicYearDTO
                {
                    Id = ay.Id.ToString(),
                    Name = ay.Name
                })
                .ToListAsync();
        }
    }
}
