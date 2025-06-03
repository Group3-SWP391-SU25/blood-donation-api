using System.Globalization;
using System.Runtime.InteropServices;
namespace BloodDonation.Domain.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public bool Gender { get; set; }
    public string Addresss { get; set; } = string.Empty;
    public string FrontUrlIdentity { get; set; } = string.Empty;
    public string BackUrlIdentity { get; set; } = string.Empty;
    

    #region  Relationship Configuration
    public Guid RoleId { get; set; } = Guid.Empty;
    public virtual Role Role { get; set; } = new();
    public Guid BloodGroupId { get; set; } = Guid.Empty;
    public virtual BloodGroup BloodGroup { get; set; } = new();
    public virtual ICollection<EmergencyBloodRequest>? EmergencyBloodRequests { get; set; }
    public virtual ICollection<Blog>? Blogs { get; set; }
    public virtual ICollection<BloodDonationRequest>? BloodRequests { get; set; }
    
    #endregion

}
