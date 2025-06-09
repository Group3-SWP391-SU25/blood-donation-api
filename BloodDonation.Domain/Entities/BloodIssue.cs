namespace BloodDonation.Domain.Entities;

public class BloodIssue : BaseEntity
{

    public double Volume { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime DateIssue { get; set; }

    #region  Relationship Configuration
    public virtual BloodStorage BloodStorage { get; set; }
    public Guid BloodStorageId { get; set; } = Guid.Empty;

    public virtual EmergencyBloodRequest EmergencyBloodRequest { get; set; }
    public Guid EmergencyBloodRequestId { get; set; } = Guid.Empty;


    #endregion
}