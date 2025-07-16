namespace BloodDonation.Application.Models.Users;

public class UserUpdateModel
{
    public DateTime? DateOfBirth { get; set; }
    public string? FullName { get; set; } = string.Empty;

    public string? PhoneNo { get; set; } = string.Empty;
    public bool? Gender { get; set; }
    public string? Addresss { get; set; } = string.Empty;
    public string? FrontUrlIdentity { get; set; } = string.Empty;
    public string? BackUrlIdentity { get; set; } = string.Empty;
    public Guid? RoleId { get; set; }
    public string? Status { get; set; }
    public Guid? BloodGroupId { get; set; }

}
