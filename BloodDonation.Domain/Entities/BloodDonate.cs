using System.Diagnostics.Contracts;

namespace BloodDonation.Domain.Entities;

public class BloodDonate : BaseEntity
{
    public string Type { get; set; } = string.Empty;
    public double Volume { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    #region  Relationship COnfiguration
    public Guid BloodRequestId { get; set; }
    public BloodRequest BloodRequest { get; set; } = new();

    #endregion
}
