using BloodDonation.Application.Models.HealthCheckForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IHealthCheckFormService
    {
        //Create
        Task<object> CreateAsync(HealthCheckFormCreateModel model, CancellationToken cancellationToken = default);
        //Update
        Task<object> UpdateAsync(Guid id, HealthCheckFormUpdateModel model, CancellationToken cancellationToken = default);
        //Get by Id
        Task<HealthCheckFormViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
