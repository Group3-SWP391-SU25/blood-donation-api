using BloodDonation.Application.Models.BloodDonationRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodDonationRequestService
    {
        //Get all
        Task<IEnumerable<BloodDonationRequestViewModel>?> SearchAsync(int? pageSize, string search = "", int pageIndex = 0, CancellationToken cancellationToken = default);
    }
}
