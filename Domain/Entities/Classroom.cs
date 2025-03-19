namespace Domain.Entities;

public class Classroom
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public int Number { get; set; }

    public Ulid GradeId { get; set; }
    public Grade Grade { get; set; }

    public Ulid AcademicYearId { get; set; }
    public AcademicYear AcademicYear { get; set; }

}
