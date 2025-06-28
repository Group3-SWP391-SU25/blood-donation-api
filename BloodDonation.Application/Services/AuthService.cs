using BloodDonation.Application.Models.Auth;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using Firebase.Auth;
namespace BloodDonation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly AppSetting appSetting;

    public AuthService(IUnitOfWork unitOfWork,
        AppSetting appsetting)
    {
        this.appSetting = appsetting;
        this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<AuthResponseModel> LoginAsync(AuthRequestModel requestModel, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == requestModel.Email &&
            x.Status == UserStatusEnum.Active.ToString(), cancellationToken, [x => x.Role, x => x.BloodGroup!]);
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
    private async Task<Domain.Entities.User> LoginAsync(Firebase.Auth.User firebaseUser)
    {
        Domain.Entities.User user = new Domain.Entities.User();
        user.Email = firebaseUser.Email;
        user.FullName = $"{firebaseUser.LastName} {firebaseUser.FirstName}";
        user.HashPassword = string.Empty;
        user.RoleId = (await unitOfWork.RoleRepository.FirstOrDefaultAsync(x => x.Name == RoleNames.MEMBER))?.Id ?? Guid.Empty;
        await unitOfWork.UserRepository.CreateAsync(user);
        await unitOfWork.SaveChangesAsync();
        return user;

    }
    public async Task<AuthResponseModel> LoginFirebaseAsync(string firebaseToken, CancellationToken cancellationToken = default)
    {
        var authResponse = new AuthResponseModel();
        var auth = new Firebase.Auth.FirebaseAuthProvider(new FirebaseConfig(appSetting.FirebaseConfig?.ApiKey));
        var userFirebase = await auth.GetUserAsync(firebaseToken) ?? throw new Exception("Firebase Token Does not exsit");



        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == userFirebase.Email, includes: [x => x.Role, x => x.BloodGroup]);
        // Insert Into Db
        if (user is null)
        {
            user = await LoginAsync(userFirebase);
            authResponse.User = unitOfWork.Mapper.Map<UserViewModel>(user);
            authResponse.Token = TokenGenerator.GenerateToken(user, user.Role.Name);
        }
        else
        {
            if (user.Status == UserStatusEnum.InActive.ToString()) throw new InvalidOperationException("User không được đăng nhập vào hệ thống");
            authResponse.User = unitOfWork.Mapper.Map<UserViewModel>(user);
            authResponse.Token = TokenGenerator.GenerateToken(user, user.Role.Name);
        }
        return authResponse;

    }
}