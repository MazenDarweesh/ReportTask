namespace Domain.Entities;

public class School
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
}
