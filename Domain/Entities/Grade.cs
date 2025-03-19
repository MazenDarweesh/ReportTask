using static System.Collections.Specialized.BitVector32;

namespace Domain.Entities;

public class Grade
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }

    public Ulid SectionId { get; set; }
    public Section Section { get; set; }
    public ICollection<Classroom> Classrooms { get; set; }
}
