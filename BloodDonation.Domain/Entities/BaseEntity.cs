namespace BloodDonation.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
}