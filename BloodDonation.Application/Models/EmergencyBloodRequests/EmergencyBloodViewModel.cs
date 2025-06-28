using BloodDonation.Application.Models.BloodComponents;
using BloodDonation.Application.Models.BloodGroups;
using BloodDonation.Domain.Enums;
namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodViewModel : BaseModel
    {
        public EmergencyBloodRequestEnum Status { get; set; }
        public double Volume { get; set; }
        public string Address { get; set; } = string.Empty;
        public BloodComponentViewModel BloodComponent { get; set; }
        public BloodGroupViewModel BloodGroup { get; set; }
    }
}
