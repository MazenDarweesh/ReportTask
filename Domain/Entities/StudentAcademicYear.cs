using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentAcademicYear
    {
        public Ulid Id { get; set; } = Ulid.NewUlid();

        public Ulid StudentId { get; set; }
        public Ulid SchoolId { get; set; }
        public Ulid ClassId { get; set; }
        public Ulid GradeId { get; set; }
        public Ulid SemesterId { get; set; }

        public Student Student { get; set; }
        public School School { get; set; }
        public Classroom Class { get; set; }
        public Grade Grade { get; set; }
        public Semester Semester { get; set; }
    }
}
