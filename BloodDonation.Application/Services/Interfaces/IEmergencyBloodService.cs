using BloodDonation.Application.Models.BloodEmergencyRequests;
using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IEmergencyBloodService
    {
        Task<object> GetAll(int pageIndex,
            int pageSize,
            string? search = "",
            BloodEmergencyStatusEnum? status = null,
            CancellationToken cancellationToken = default);
        Task<EmergencyBloodRequestViewModel> GetById(Guid id,
            CancellationToken cancellationToken = default);
        Task<EmergencyBloodRequestViewModel> CreateAsync(EmergencyBloodRequestCreateModel model,
            CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id,
            EmergencyBloodRequestUpdateModel model,
            CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id,
            CancellationToken cancellationToken = default);
    }
}
