namespace BloodDonation.Application.Models.Users;

public class UserUpdateModel
{
    public DateTime? DateOfBirth { get; set; }
    public string? FullName { get; set; }

    public string? PhoneNo { get; set; }
    public bool? Gender { get; set; }
    public string? Addresss { get; set; }
    public string? FrontUrlIdentity { get; set; }
    public string? BackUrlIdentity { get; set; }
    public Guid? RoleId { get; set; }
    public string? Status { get; set; }
    public Guid? BloodGroupId { get; set; }

}
