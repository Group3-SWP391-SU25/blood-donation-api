using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodDonationService
    {
        //search
        Task<object> SearchAsync(string? searchKey, int? pageIndex, int? pageSize, BloodDonationStatusEnum? status);
        //update status
        Task<bool> UpdateStatusAsync(Guid id, BloodDonationStatusEnum status, CancellationToken cancellationToken = default);
    }
}
