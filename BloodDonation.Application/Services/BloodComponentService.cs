using BloodDonation.Application.Models.BloodComponents;
using BloodDonation.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class BloodComponentService : IBloodComponentService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodComponentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<BloodComponentViewModel>> GetAllAsync(string? search = null)
        {
            var bloodComponents = await unitOfWork.BloodComponentRepository.Search(filter: b => b.Name.Contains(search!));
            return unitOfWork.Mapper.Map<IEnumerable<BloodComponentViewModel>>(bloodComponents);
        }
    }
}
