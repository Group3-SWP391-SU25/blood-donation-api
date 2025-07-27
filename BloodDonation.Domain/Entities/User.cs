using BloodDonation.Domain.Enums;
namespace BloodDonation.Domain.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string FullName { get; set; }
    public string HashPassword { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string Status { get; set; } = UserStatusEnum.Active.ToString();
    public bool Gender { get; set; }
    public string Addresss { get; set; }
    public string FrontUrlIdentity { get; set; }
    public string BackUrlIdentity { get; set; }


    #region  Relationship Configuration
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; } = null!;
    public Guid? BloodGroupId { get; set; }
    public virtual BloodGroup? BloodGroup { get; set; }
    public virtual ICollection<EmergencyBloodRequest>? EmergencyBloodRequests { get; set; }
    public virtual ICollection<Blog>? Blogs { get; set; }
    public virtual ICollection<BloodDonationRequest>? BloodRequests { get; set; }

    #endregion

}
