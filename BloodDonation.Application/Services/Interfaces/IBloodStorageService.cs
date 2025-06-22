using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodStorageService
    {
        //search
        Task<object> SearchAsync(int pageIndex,
            int pageSize,
            string? search = "",
            BloodStorageStatusEnum? status = null,
            CancellationToken cancellationToken = default);
        Task<BloodStorageViewModel> CreateAsync(BloodStorageCreateModel createModel,
            CancellationToken cancellationToken = default);
        Task<BloodStorageViewModel> UpdateAsync(Guid id,
            BloodStorageUpdateModel updateModel,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id,
            CancellationToken cancellationToken = default);

    }
}
