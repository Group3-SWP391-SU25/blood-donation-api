using BloodDonation.Application.Models.Users;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodRequestViewModel : BaseModel
    {
        public BloodEmergencyStatusEnum Status { get; set; }
        public BloodIssue BloodIssue { get; set; }
        public UserViewModel User { get; set; }
    }
}
