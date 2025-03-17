using System.Diagnostics;
using System.Security.Claims;


namespace Domain.Entities;

public class Student
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public string MobileNumber { get; set; } 
    public string Nationality { get; set; } 
    public string Gender { get; set; }

    // Relationship: Many-to-Many via StudentAcademicYear
    public ICollection<StudentAcademicYear> StudentAcademicYears { get; set; }
}
