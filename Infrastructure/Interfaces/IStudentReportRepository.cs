using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentReportRepository
    {
        Task<List<StudentAcademicYear>> GetFilteredStudentAcademicYearsAsync(string? schoolId, string? yearId, string? gradeId, string? classId);
    }
}
