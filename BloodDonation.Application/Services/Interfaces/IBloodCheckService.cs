using BloodDonation.Application.Models.BloodChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodCheckService
    {
        Task<BloodCheckViewModel> CreateAsync(BloodCheckCreateModel model, CancellationToken cancellationToken = default);
        Task<BloodCheckViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<BloodCheckViewModel?> GetByBloodDonationIdAsync(Guid bloodDonationId, CancellationToken cancellationToken = default);
    }
}
