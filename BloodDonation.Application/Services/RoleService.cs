using BloodDonation.Application.Models.Roles;
using BloodDonation.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllAsync()
        {
            var roles = await unitOfWork.RoleRepository.Search(pageIndex:1, pageSize: 999);
            var roleViewModels = unitOfWork.Mapper.Map<IEnumerable<RoleViewModel>>(roles);

            return roleViewModels;
        }
    }
}
