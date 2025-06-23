using BloodDonation.Application.Models.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>> GetAllAsync();
    }
}
