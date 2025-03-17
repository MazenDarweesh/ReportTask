using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Semester
    {
        public Ulid Id { get; set; } = Ulid.NewUlid();
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public Ulid AcademicYearId { get; set; }
        public AcademicYear AcademicYear { get; set; }
    }
}
