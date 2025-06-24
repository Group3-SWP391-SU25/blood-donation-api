using BloodDonation.Application.IntegrationServices.Interfaces;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;



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

    public async Task<object> GetAsync(int? pageSize = null,
        string search = "",
        int? pageIndex = null,
        CancellationToken cancellationToken = default)
    {
        var pagedData = await unitOfWork.UserRepository.Search(
                 filter: null!,
                 includeProperties: "Role,BloodGroup",
                 orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                 pageIndex: pageIndex,
                 pageSize: pageSize);
        // Tổng số bản ghi
        int totalRecords = pagedData.Count();

        // Tính toán phân trang
        int actualPageSize = pageSize ?? 10;
        int actualPageIndex = pageIndex ?? 1;
        int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

        // Ánh xạ sang ViewModel
        var mappedData = unitOfWork.Mapper.Map<List<UserViewModel>>(pagedData);

        // Trả kết quả
        return new
        {
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            PageIndex = actualPageIndex,
            PageSize = actualPageSize,
            Records = mappedData
        };
    }

    public async Task<UserViewModel> GetByEmailAsync(string email, string? password = null, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Email == email, cancellationToken, [x => x.Role, x => x.BloodGroup!]);
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
        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken, [x => x.Role, x => x.BloodGroup!]));
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id,
            cancellationToken, includes: [x => x.Role]);
        if (user is not null)
        {
            user.IsDeleted = true;
            user.Status = UserStatusEnum.InActive.ToString();
            unitOfWork.UserRepository.Update(user);

            return await unitOfWork.SaveChangesAsync();
        }
        throw new InvalidOperationException("không tìm thấy user với id: " + id);

    }

    public async Task<bool> UpdateAsync(Guid id, UserUpdateModel model,
        CancellationToken cancellationToken = default)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == id,
            cancellationToken,
            includes: [x => x.Role, x => x.BloodGroup!]);
        if (user is not null)
        {
            unitOfWork.Mapper.Map(model, user);
            unitOfWork.UserRepository.Update(user);
            return await unitOfWork.SaveChangesAsync();
        }
        else throw new InvalidOperationException("Không tìm thấy user với id: " + id);
    }
}
