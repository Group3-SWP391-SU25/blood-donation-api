using BloodDonation.Application.Models.BloodGroups;
using BloodDonation.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class BloodGroupService : IBloodGroupService
    {
        private readonly IUnitOfWork unitOfWork;

        public BloodGroupService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BloodGroupViewModel>> GetAllAsync()
        {
            var bloodGroups = await unitOfWork.BloodGroupRepository.GetAllAsync();
            return unitOfWork.Mapper.Map<IEnumerable<BloodGroupViewModel>>(bloodGroups);
        }
    }
}
