using FluentValidation;

namespace BloodDonation.Application.Models.Users;

public class UserCreateModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public string Addresss { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string IdentityFrontUrl { get; set; } = string.Empty;
    public string IdentityBackUrl { get; set; } = string.Empty;
    public string IdentityId { get; set; } = string.Empty;
    public bool Gender { get; set; } = true;
    public Guid? RoleId { get; set; } = null;
}
public class UserCreateValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

    }
}
