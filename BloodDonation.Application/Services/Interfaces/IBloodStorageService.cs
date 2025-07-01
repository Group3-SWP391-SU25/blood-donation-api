using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodStorageService
    {
        //search
        Task<object> SearchAsync(Guid? bloodGroupId, Guid? componentId, int pageIndex, int pageSize, string? search = "", BloodStorageStatusEnum? status = null, CancellationToken cancellationToken = default);
        Task ExpireOutdatedBloodAsync();
        Task<object> GetAvailableBloods(int pageIndex, int pageSize, BloodStorageStatusEnum? status = null, Guid? BloodComponentId = null, Guid? BloodGroupId = null, int? volume = null, CancellationToken cancellationToken = default);
        Task PrepareBloodAsync(Guid id, BloodStorageCreateModel dto);
        Task<object> VolumeSummary(Guid? bloodGroupId, Guid? componentId);
    }
}
