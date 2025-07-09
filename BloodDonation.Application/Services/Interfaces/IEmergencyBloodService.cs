using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IEmergencyBloodService
    {
        Task<EmergencyBloodViewModel> CreateAsync(EmergencyBloodCreateModel requestModel,
            CancellationToken cancellationToken = default);
        Task<object> GetAllAsync(int pageIndex = 0,
            int pageSize = 10,
            string? searchTerm = null,
            EmergencyBloodRequestEnum? status = null,
            CancellationToken cancellationToken = default);
        Task<EmergencyBloodViewModel> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Guid id,
            EmergencyBloodUpdateModel model,
            CancellationToken cancellationToken = default);
        Task<bool> DelAsync(Guid id,
            CancellationToken cancellationToken = default);
        Task<object> GetSummaryAsync(DateRangeFilter range);
    }
}
