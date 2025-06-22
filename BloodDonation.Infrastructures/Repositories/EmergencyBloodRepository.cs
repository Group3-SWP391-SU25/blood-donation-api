using BloodDonation.Application.Repositories;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Infrastructures.Repositories
{
    public class EmergencyBloodRepository : GenericRepository<EmergencyBloodRequest>,
        IEmergencyBloodRepository
    {
        public EmergencyBloodRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
