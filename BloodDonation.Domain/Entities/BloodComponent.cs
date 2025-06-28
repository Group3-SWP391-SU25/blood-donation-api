namespace BloodDonation.Domain.Entities;

public class BloodComponent : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public double MinStorageTemerature { get; set; }
    public double MaxStorageTemerature { get; set; }
    public double ShelfLifeInDay { get; set; }
    public string Status { get; set; } = string.Empty;

    #region Relationship Configuration
    public virtual ICollection<BloodStorage> BloodStorages { get; set; }
    public virtual ICollection<EmergencyBloodRequest> EmergencyBloodRequests { get; set; }

    #endregion
}