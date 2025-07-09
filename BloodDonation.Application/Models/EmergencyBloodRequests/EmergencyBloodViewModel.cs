using BloodDonation.Application.Models.BloodComponents;
using BloodDonation.Application.Models.BloodGroups;
using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Domain.Enums;
namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodViewModel : BaseModel
    {
        public string Code { get; set; } = string.Empty;
        public EmergencyBloodRequestEnum Status { get; set; }
        public double Volume { get; set; }
        public string Address { get; set; } = string.Empty;
        public BloodComponentViewModel BloodComponent { get; set; }
        public BloodGroupViewModel BloodGroup { get; set; }
        //public ICollection<BloodIssueViewModel> BloodIssues { get; set; }
    }
}
