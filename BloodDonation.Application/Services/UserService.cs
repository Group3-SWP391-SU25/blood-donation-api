using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;

namespace BloodDonation.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<UserViewModel?> CreateUserAsync(UserCreateModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<PaginatedList<UserViewModel>?> GetAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<UserViewModel?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return unitOfWork.Mapper.Map<UserViewModel>(await unitOfWork
        .UserRepository
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken));
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Guid id, UserUpdateModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
