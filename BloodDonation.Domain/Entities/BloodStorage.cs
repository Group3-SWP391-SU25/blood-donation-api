using System;

namespace BloodDonation.Domain.Entities;

public class BlooodStorage : BaseEntity
{
    public double Volume { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ExpiredDate { get; set; }
    #region Relationship Configuration 
    public Guid BloodDonateId { get; set; } = Guid.Empty;
    public virtual BloodDonationRequest BloodDonate { get; set; } = new();
    public Guid BloodComponentId { get; set; } = Guid.Empty;
    public virtual BloodComponent BloodComponent { get; set; } = new();

    public Guid BloodGroupId { get; set; } = Guid.Empty;
    public virtual BloodGroup BloodGroup { get; set; } = new();
    
    public virtual BloodIssue? BloodIssue { get; set; } = new();
    
    #endregion
}