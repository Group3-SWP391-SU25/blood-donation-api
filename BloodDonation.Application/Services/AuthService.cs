using BloodDonation.Application.Models.Auth;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Repositories;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BloodDonation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;

    public AuthService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AuthResponseModel> LoginAsync(AuthRequestModel requestModel, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == requestModel.Email, cancellationToken, x => x.Role);
        var hashPassword = requestModel.Password.Hashing();
        if (hashPassword == user?.HashPassword)
        {
            var token = TokenGenerator.GenerateToken(user, user.Role.Name);
            return new AuthResponseModel()
            {
                Token = token,
                User = unitOfWork.Mapper.Map<UserViewModel>(user)
            };
        }
        throw new UnauthorizedAccessException("Invalid email or password.");
    }
}