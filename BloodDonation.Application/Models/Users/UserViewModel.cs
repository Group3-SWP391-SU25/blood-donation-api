namespace BloodDonation.Application.Models.Users;

public class UserViewModel
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public string Addresss { get; set; } = string.Empty;
    public string FrontUrlIdentity { get; set; } = string.Empty;
    public string BackUrlIdentity { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string IdentityId { get; set; } = string.Empty;
    public bool Gender { get; set; } = true;
    public string Status { get; set; } = string.Empty;
    public Guid RoleId { get; set; } = Guid.Empty;
    public string RoleName { get; set; } = string.Empty;
    public Guid BloodGroupId { get; set; }
    public string BloodGroupType { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }



}