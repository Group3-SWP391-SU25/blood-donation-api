using BloodDonation.Application.Repositories;
using BloodDonation.Domain.Entities;

namespace BloodDonation.Infrastructures.Repositories
{
    public class BloodIssueRepository : GenericRepository<BloodIssue>, IBloodIssueRepository
    {
        public BloodIssueRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
