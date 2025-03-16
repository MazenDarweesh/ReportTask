namespace Domain.Entities;

public class Grade
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
}
