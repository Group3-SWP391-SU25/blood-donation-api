using BloodDonation.Application.Models.Users;

namespace BloodDonation.Application.Models.Auth;

public class AuthResponseModel
{
    public string Token { get; set; } = string.Empty;
    public UserViewModel User { get; set; }
}