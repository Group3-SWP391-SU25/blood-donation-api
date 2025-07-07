using BloodDonation.Application.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface INurseService
    {
        Task<object> SearchMemberAsync(string?searchKey, int pageIndex, int pageSize, Guid? bloodGroupId, string? address);
        Task SendCallForDonationEmailAsync(List<Guid> userIds);

    }
}
