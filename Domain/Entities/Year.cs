namespace Domain.Entities;

public class Year
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public int YearValue { get; set; }
}
