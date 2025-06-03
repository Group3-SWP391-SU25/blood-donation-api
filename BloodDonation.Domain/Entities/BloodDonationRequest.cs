using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class BloodDonationRequest : BaseEntity
{
    public string BloodType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ReasonReject { get; set; } = string.Empty;
    public DateTime DonatedDateRequest { get; set; } = DateTime.Now;
    public string Type { get; set; } = string.Empty;
    public double Volume { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<BloodDonationRequirement> BloodDonationRequirements { get; set; } = [];

    #region Relationship Configuration 
    public virtual User User { get; set; } = new();
    public Guid UserId { get; set; }
    public virtual HealthCheckForm? HealthCheckForm { get; set; } = null;
    #endregion
}