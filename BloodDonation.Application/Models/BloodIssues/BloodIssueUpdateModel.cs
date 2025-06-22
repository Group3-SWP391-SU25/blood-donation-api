using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Models.BloodIssues
{
    public class BloodIssueUpdateModel
    {
        public BloodIssueStatusEnum Status { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
    }
}
