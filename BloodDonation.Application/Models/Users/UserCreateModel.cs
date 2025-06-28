using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Application.Models.Users;

public class UserCreateModel
{
    public string FullName { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public string Addresss { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IFormFile IdentityFront { get; set; }
    public IFormFile IdentityBack { get; set; }
    public string IdentityId { get; set; } = string.Empty;
    public bool Gender { get; set; } = true;
    public Guid? RoleId { get; set; } = null;
}
public class UserCreateValidator : AbstractValidator<UserCreateModel>
{
    private readonly string[] permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
    private readonly string[] permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

    public UserCreateValidator()
    {
        RuleFor(x => x.IdentityBack)
           .NotNull().WithMessage("File is required.")
           .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
           .Must(file => permittedMimeTypes.Contains(file.ContentType)).WithMessage("Invalid image type.")
           .Must(file => permittedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
           .WithMessage("File extension is not allowed.");
        RuleFor(x => x.IdentityFront)
           .NotNull().WithMessage("File is required.")
           .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
           .Must(file => permittedMimeTypes.Contains(file.ContentType)).WithMessage("Invalid image type.")
           .Must(file => permittedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
           .WithMessage("File extension is not allowed.");
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

    }
}
