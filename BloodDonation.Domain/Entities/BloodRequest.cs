using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class BloodRequest : BaseEntity
{
    public string BloodType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ReasonReject { get; set; } = string.Empty;
    public DateTime DonatedDateRequest { get; set; } = DateTime.Now;
    #region Relationship Configuration 
    public virtual User User { get; set; } = new();
    public Guid UserId { get; set; }
    public BloodDonate? BloodDonate { get; set; }
    #endregion
}