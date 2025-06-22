using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class BloodIssue : BaseEntity
{

    public double Volume { get; set; }
    public BloodIssueStatusEnum Status { get; set; } = BloodIssueStatusEnum.Pending;
    public DateTime DateIssue { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string ReceiverName { get; set; } = string.Empty;

    #region  Relationship Configuration
    public virtual BloodStorage BloodStorage { get; set; }
    public Guid BloodStorageId { get; set; } = Guid.Empty;

    public virtual EmergencyBloodRequest? EmergencyBloodRequest { get; set; }



    #endregion
}