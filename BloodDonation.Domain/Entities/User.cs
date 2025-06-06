using System;
using System.Collections.Generic;
using BloodDonation.Domain.Enums;
namespace BloodDonation.Domain.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Status { get; set; } = UserStatusEnum.Active.ToString();
    public bool Gender { get; set; }
    public string Addresss { get; set; } = string.Empty;
    public string FrontUrlIdentity { get; set; } = string.Empty;
    public string BackUrlIdentity { get; set; } = string.Empty;
    

    #region  Relationship Configuration
    public Guid RoleId { get; set; } 
    public virtual Role Role { get; set; } 
    public Guid? BloodGroupId { get; set; } 
    public virtual BloodGroup? BloodGroup { get; set; } 
    public virtual ICollection<EmergencyBloodRequest>? EmergencyBloodRequests { get; set; }
    public virtual ICollection<Blog>? Blogs { get; set; }
    public virtual ICollection<BloodDonationRequest>? BloodRequests { get; set; }

    #endregion

}
