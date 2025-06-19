using BloodDonation.Application.IntegrationServices.Interfaces;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;



namespace BloodDonation.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFirebaseService firebaseService;

    public UserService(IUnitOfWork unitOfWork,
        IFirebaseService firebaseService)
    {
        this.firebaseService = firebaseService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<UserViewModel?> CreateUserAsync(UserCreateModel model, CancellationToken cancellationToken = default)
    {
        var user = unitOfWork.Mapper.Map<User>(model);
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.UtcNow;
        if (model.IdentityFront != null)
        {
            var res = await firebaseService.SaveFileAsync(model.IdentityFront,
                @"blood-donation/identity");
            if (!string.IsNullOrEmpty(res.Url))
            {
                user.FrontUrlIdentity = res.Url;
            }
        }
        if (model.IdentityBack != null)
        {
            var res = await firebaseService.SaveFileAsync(model.IdentityBack,
                @"blood-donation/identity");
            if (!string.IsNullOrEmpty(res.Url))
            {
                user.BackUrlIdentity = res.Url;
            }
        }
        if (model.RoleId is null)
        {
            var role = await unitOfWork.RoleRepository.FirstOrDefaultAsync(x => x.Name == RoleNames.MEMBER, cancellationToken);
            if (role is not null)
            {
                user.RoleId = role.Id;
            }
        }
        user.HashPassword = model.Password.Hashing();
        var createdUserId = await unitOfWork.UserRepository.CreateAsync(user, cancellationToken);
        if (createdUserId == Guid.Empty)
            return null;
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return unitOfWork.Mapper.Map<UserViewModel>(user);
    }

    public async Task<PaginatedList<UserViewModel>?> GetAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<UserViewModel> GetByEmailAsync(string email, string? password = null, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == email, cancellationToken, x => x.Role);
        if (user is null)
            throw new Exception("User not found");
        else
        {
            if (string.IsNullOrEmpty(password))
            {
                return unitOfWork.Mapper.Map<UserViewModel>(user);
            }
            else
            {
                if (user.HashPassword == password)
                {
                    return unitOfWork.Mapper.Map<UserViewModel>(user);
                }
                else
                {
                    throw new Exception("Invalid password");
                }
            }
        }
    }

    public async Task<UserViewModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return unitOfWork.Mapper.Map<UserViewModel>(await unitOfWork
            .UserRepository
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, UserUpdateModel model, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
