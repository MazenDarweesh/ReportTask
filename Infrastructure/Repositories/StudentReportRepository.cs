using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
namespace Infrastructure.Repositories
{
    public class StudentReportRepository : IStudentReportRepository
    {
        private readonly AppDbContext _context;

        public StudentReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // EF Core approach
        public async Task<List<StudentAcademicYear>> GetFilteredStudentAcademicYearsAsync(string? schoolId, string? yearId, string? gradeId, string? classId)
        {
            return await _context.StudentAcademicYears
                .Include(say => say.Student)
                .Include(say => say.School)
                .Include(say => say.Classroom)
                .Include(say => say.Grade)
                .Include(say => say.Semester)
                .Where(say =>
                    (string.IsNullOrEmpty(schoolId) || say.SchoolId.ToString() == schoolId) &&
                    (string.IsNullOrEmpty(yearId) || say.Semester.AcademicYearId.ToString() == yearId) &&
                    (string.IsNullOrEmpty(gradeId) || say.GradeId.ToString() == gradeId) &&
                    (string.IsNullOrEmpty(classId) || say.ClassroomId.ToString() == classId))
                .ToListAsync();
        }
    }
}
