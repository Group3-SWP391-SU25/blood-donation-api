using BloodDonation.Application.Models.Users;

namespace BloodDonation.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel?> CreateUserAsync(UserCreateModel model,
CancellationToken cancellationToken = default);
    Task<object> GetAsync(int? pageSize = null,
        string search = "",
        int? pageIndex = null,
        CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<UserViewModel?> GetById(Guid id,
        CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id,
        UserUpdateModel model,
        CancellationToken cancellationToken = default);
    Task<UserViewModel> GetByEmailAsync(string email,
        string? password = null,
        CancellationToken cancellationToken = default);
}