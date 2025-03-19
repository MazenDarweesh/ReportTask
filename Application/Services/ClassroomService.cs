
using Domain.DTOs;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class ClassroomService : IClassroomService
    {
        private readonly AppDbContext _context;

        public ClassroomService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClassroomDTO>> GetClassroomsAsync(string gradeId, string academicYearId)
        {
            return await _context.Classrooms
                .Where(c => c.GradeId.ToString() == gradeId && c.AcademicYearId.ToString() == academicYearId)
                .Select(c => new ClassroomDTO
                {
                    Id = c.Id.ToString(),
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
