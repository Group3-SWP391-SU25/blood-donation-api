namespace BloodDonation.Domain.Entities;

public class BloodStorage : BaseEntity
{
    public double Volume { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ExpiredDate { get; set; }
    #region Relationship Configuration 
    public Guid BloodDonationId { get; set; } = Guid.Empty;
    public virtual BloodDonation BloodDonate { get; set; }
    public Guid BloodComponentId { get; set; } = Guid.Empty;
    public virtual BloodComponent BloodComponent { get; set; }

    public Guid BloodGroupId { get; set; } = Guid.Empty;
    public virtual BloodGroup BloodGroup { get; set; }

    public virtual BloodIssue? BloodIssue { get; set; }

    #endregion
}