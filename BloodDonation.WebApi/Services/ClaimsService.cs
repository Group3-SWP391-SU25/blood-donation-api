using BloodDonation.Application.Services.Interfaces;
using System.Security.Claims;

namespace BloodDonation.WebApi.Services;

public class ClaimsService : IClaimsService
{
    public ClaimsService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(claimType: ClaimTypes.NameIdentifier);
        CurrentUser = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
    }
    public Guid CurrentUser { get; }
}