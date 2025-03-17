namespace Domain.Entities;

public class School
{
    public Ulid Id { get; set; } = Ulid.NewUlid();
    public string Name { get; set; }
    public string Address { get; set; }

    // Report Headers
    public string ReportHeaderOneEn { get; set; }
    public string ReportHeaderOneAr { get; set; }
    public string ReportHeaderTwoEn { get; set; }
    public string ReportHeaderTwoAr { get; set; }
    public string ReportImage { get; set; }

}
