namespace BloodDonation.Domain.Entities;

public class Blog : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Html { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public List<string> Imageurls { get; set; } = new();
    public Guid UserId { get; set; } = Guid.Empty;
    public virtual User User { get; set; } = new();

}