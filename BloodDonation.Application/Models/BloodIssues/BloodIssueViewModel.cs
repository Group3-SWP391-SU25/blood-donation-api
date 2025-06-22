using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Models.BloodIssues
{
    public class BloodIssueViewModel : BaseModel
    {
        public double Volume { get; set; }
        public BloodIssueStatusEnum Status { get; set; }
        public BloodStorageViewModel BloodStorage { get; set; }

    }
}
