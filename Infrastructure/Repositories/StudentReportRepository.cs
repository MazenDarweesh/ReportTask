using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
namespace Infrastructure.Repositories
{
    public class StudentReportRepository : IStudentReportRepository
    {
        private readonly AppDbContext _context;

        public StudentReportRepository(AppDbContext context)
        {
            _context = context;
        }

        // Stored Procedure approach
        public async Task<List<StudentAcademicYear>> GetFilteredStudentAcademicYearsFromSPAsync(string? schoolId, string? yearId, string? gradeId, string? classId)
        {
            var parameters = new[]
            {
                    new SqlParameter("@SchoolId", schoolId ?? (object)DBNull.Value),
                    new SqlParameter("@YearId", yearId ?? (object)DBNull.Value),
                    new SqlParameter("@GradeId", gradeId ?? (object)DBNull.Value),
                    new SqlParameter("@ClassId", classId ?? (object)DBNull.Value)
                };

            var result = await _context.StudentAcademicYears
                .FromSqlRaw("EXEC GetFilteredStudentAcademicYears @SchoolId, @YearId, @GradeId, @ClassId", parameters)
                .AsEnumerable() // Perform the composition on the client side
                .ToListAsync();

            // Manually include related entities
            var studentIds = result.Select(r => r.StudentId).Distinct().ToList();
            var students = await _context.Students.Where(s => studentIds.Contains(s.Id)).ToListAsync();

            var schoolIds = result.Select(r => r.SchoolId).Distinct().ToList();
            var schools = await _context.Schools.Where(s => schoolIds.Contains(s.Id)).ToListAsync();

            var classroomIds = result.Select(r => r.ClassroomId).Distinct().ToList();
            var classrooms = await _context.Classrooms.Where(c => classroomIds.Contains(c.Id)).ToListAsync();

            var gradeIds = result.Select(r => r.GradeId).Distinct().ToList();
            var grades = await _context.Grades.Where(g => gradeIds.Contains(g.Id)).ToListAsync();

            var semesterIds = result.Select(r => r.SemesterId).Distinct().ToList();
            var semesters = await _context.Semesters.Where(s => semesterIds.Contains(s.Id)).ToListAsync();

            // Attach related entities to the result
            foreach (var item in result)
            {
                item.Student = students.FirstOrDefault(s => s.Id == item.StudentId);
                item.School = schools.FirstOrDefault(s => s.Id == item.SchoolId);
                item.Classroom = classrooms.FirstOrDefault(c => c.Id == item.ClassroomId);
                item.Grade = grades.FirstOrDefault(g => g.Id == item.GradeId);
                item.Semester = semesters.FirstOrDefault(s => s.Id == item.SemesterId);
            }

            return result;
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
