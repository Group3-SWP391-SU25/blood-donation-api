namespace BloodDonation.Domain.Entities;

public class EmergencyBloodRequest : BaseEntity
{
    public string Status { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public double Volume { get; set; }
    public string? ReasonReject { get; set; } = string.Empty;

    public Guid UserId { get; set; } = Guid.Empty;
    public virtual User User { get; set; } = new();

    public virtual BloodIssue? BloodIssue { get; set; } 
    
}