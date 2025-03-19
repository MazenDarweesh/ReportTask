using Domain.Utilities;

using Domain.DTOs;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly AppDbContext _context;

        public SchoolService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SchoolDTO>> GetSchoolsAsync()
        {
            return await _context.Schools
                .Select(s => new SchoolDTO
                {
                    Id = s.Id.ConvertFromUlid(),
                    Name = s.Name
                })
                .ToListAsync();
        }
    }
}
