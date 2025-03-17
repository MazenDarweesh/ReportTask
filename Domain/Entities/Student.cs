using System.Diagnostics;
using System.Security.Claims;


namespace Domain.Entities;

public class Student
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }

    public Ulid SchoolId { get; set; }
    public Ulid GradeId { get; set; }
    public Ulid ClassId { get; set; }
    public Ulid YearId { get; set; }

    public School School { get; set; }
    public Grade Grade { get; set; }
    public Classroom Class { get; set; }
    public Year Year { get; set; }
}
