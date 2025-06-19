using BloodDonation.Application.Models.BloodGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IBloodGroupService
    {
        Task<IEnumerable<BloodGroupViewModel>> GetAllAsync();
    }
}
