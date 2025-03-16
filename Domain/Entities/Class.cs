namespace Domain.Entities;

public class Class
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
}
