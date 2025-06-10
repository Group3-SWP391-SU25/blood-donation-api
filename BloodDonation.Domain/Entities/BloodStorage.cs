using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class BloodStorage : BaseEntity
{
    public double Volume { get; set; }
    public BloodStorageStatusEnum Status { get; set; } = BloodStorageStatusEnum.UnChecked;
    public DateTime ExpiredDate { get; set; }

    #region Relationship Configuration 
    public Guid BloodDonationId { get; set; } = Guid.Empty;
    public virtual BloodDonation BloodDonate { get; set; }
    public Guid BloodComponentId { get; set; } = Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b63"); // Default to BloodComponent is "Máu toàn phần"
    public virtual BloodComponent BloodComponent { get; set; }

    public Guid? BloodGroupId { get; set; }
    public virtual BloodGroup? BloodGroup { get; set; }

    public virtual BloodIssue? BloodIssue { get; set; }

    #endregion
}