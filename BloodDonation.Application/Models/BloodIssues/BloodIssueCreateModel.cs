using BloodDonation.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BloodDonation.Application.Models.BloodIssues
{
    public class BloodIssueCreateModel
    {
        [Required]
        public Guid BloodStorageId { get; set; }
        public double Volume { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public BloodIssueStatusEnum Status { get; set; } = BloodIssueStatusEnum.Approved;

    }
}
