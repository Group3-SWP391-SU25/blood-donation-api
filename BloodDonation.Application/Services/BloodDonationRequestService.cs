using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Services.Interfaces;


namespace BloodDonation.Application.Services
{
    public class BloodDonationRequestService : IBloodDonationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodDonationRequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BloodDonationRequestViewModel>?> SearchAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default)
        {
            var bloodDonationRequests = await unitOfWork
                .BloodDonationRequestRepository
                .Search(pageSize: pageSize, pageIndex: pageIndex);

            return unitOfWork.Mapper.Map<IEnumerable<BloodDonationRequestViewModel>>(bloodDonationRequests);
        }
    }
}
