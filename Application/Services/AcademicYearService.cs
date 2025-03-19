using Domain.Utilities;

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
            var schoolUlid = schoolId.ConvertToUlid(); 

            return await _context.AcademicYears
                .Where(ay => ay.SchoolId == schoolUlid)
                .Select(ay => new AcademicYearDTO
                {
                    Id = ay.Id.ConvertFromUlid(),  
                    Name = ay.Name
                })
                .ToListAsync();
        }
    }
}
