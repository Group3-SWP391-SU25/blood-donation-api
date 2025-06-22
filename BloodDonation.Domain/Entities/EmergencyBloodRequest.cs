using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class EmergencyBloodRequest : BaseEntity
{
    public BloodEmergencyStatusEnum Status { get; set; } = BloodEmergencyStatusEnum.Pending;
    public string Address { get; set; } = string.Empty;
    public double Volume { get; set; }
    public string? ReasonReject { get; set; } = string.Empty;

    public Guid UserId { get; set; } = Guid.Empty;
    public virtual User User { get; set; }

    public virtual BloodIssue BloodIssue { get; set; }
    public Guid? BloodIssueId { get; set; }

}