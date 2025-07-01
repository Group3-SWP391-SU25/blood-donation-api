using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodDonationRequestService
    {
        //Search
        Task<object> SearchAsync(int? pageIndex = 1, int? pageSize = 10,
                                               BloodDonationRequestStatus? status = null, string? keyword = null, TimeSlotEnum? timeSlot = null, DateOnly? dayRequest = null);
        //Create
        Task<BloodDonationRequestViewModel?> CreateAsync(BloodDonationRequestCreateModel model, CancellationToken cancellationToken = default);
        //Update
        Task<BloodDonationRequestViewModel?> UpdatePartialAsync(BloodDonationRequestUpdateModel model, CancellationToken cancellationToken = default);
        //Delete
        Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
        //GetById
        Task<BloodDonationRequestViewModel?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        //update status
        Task<BloodDonationRequestViewModel?> UpdateStatusAsync(string id, string? rejectNote, BloodDonationRequestStatus status, CancellationToken cancellationToken = default);
        //GetByUserId
        Task<object> GetByUserIdAsync(Guid userId, int? pageIndex = 1, int? pageSize = 10, CancellationToken cancellationToken = default);
        Task CancelExpiredPendingRequestsAsync();
        Task<object> GetSummaryAsync(DateRangeFilter range);
    }
}
