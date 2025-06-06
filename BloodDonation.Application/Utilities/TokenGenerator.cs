using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BloodDonation.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BloodDonation.Application.Utilities;

public static class TokenGenerator
{
    public static string GenerateToken(User user,
        string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("VERYSTRONGPASSWORD_CHANGEMEIFYOUNEED");
        var claimsList = new List<Claim>()
            {
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Role, role)
            };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = "BloodDonation.Client",
            Issuer = "BloodDonation.WebApi",
            Subject = new ClaimsIdentity(claimsList),
            Expires = DateTime.UtcNow.AddDays(1000),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}