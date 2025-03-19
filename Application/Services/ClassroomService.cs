using Domain.Utilities;

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

        public async Task<List<ClassroomDTO>> GetClassroomsAsync()
        {
            return await _context.Classrooms
                .Select(c => new ClassroomDTO
                {
                    Id = c.Id.ConvertFromUlid(),  
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
