using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Utilities;

namespace BloodDonation.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel?> CreateUserAsync(UserCreateModel model,
CancellationToken cancellationToken = default);
    Task<PaginatedList<UserViewModel>?> GetAsync(int? pageSize,
        string search = "",
        int pageIndex = 0,
        CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<UserViewModel?> GetById(Guid id,
        CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id,
        UserUpdateModel model,
        CancellationToken cancellationToken = default);
}