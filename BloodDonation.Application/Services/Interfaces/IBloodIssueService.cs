using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services.Interfaces;

public interface IBloodIssueService
{
    Task<object> GetAll(int pageIndex,
            int pageSize,
            string? search = "",
            BloodIssueStatusEnum? status = null,
            CancellationToken cancellationToken = default);
    Task<BloodIssueViewModel> CreateAsync(BloodIssueCreateModel bloodIssue,
        CancellationToken cancellationToken = default);
    Task<BloodIssueViewModel> GetByIdAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id,
        CancellationToken cancellationToken = default);
    Task<BloodIssueViewModel> UpdateAsync(Guid id,
        BloodIssueUpdateModel updateModel,
        CancellationToken cancellationToken = default);
}
