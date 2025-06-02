namespace BloodDonation.Domain.Entities;

public class BloodDonationRequirement : BaseEntity
{
    public string Requirement { get; set; } = string.Empty;
    public string Actual { get; set; } = string.Empty;
    public bool IsPass { get; set; } = false;
    public string Note { get; set; } = string.Empty;

}