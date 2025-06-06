
using BloodDonation.Application.Models.Auth;

namespace BloodDonation.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseModel> LoginAsync(AuthRequestModel requestModel, CancellationToken cancellationToken = default);
}