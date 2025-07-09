using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class EmergencyBloodRequest : BaseEntity
{
    public string? Code { get; set; } = string.Empty;
    public EmergencyBloodRequestEnum Status { get; set; } = EmergencyBloodRequestEnum.Pending;
    public string Address { get; set; } = string.Empty;
    public double Volume { get; set; }
    public string? ReasonReject { get; set; } = string.Empty;

    public Guid UserId { get; set; } = Guid.Empty;
    public virtual User User { get; set; }


    public ICollection<BloodIssue> BloodIssues { get; set; }

    public Guid BloodComponentId { get; set; }
    public BloodComponent BloodComponent { get; set; }
    public Guid BloodGroupId { get; set; }
    public BloodGroup BloodGroup { get; set; }

}