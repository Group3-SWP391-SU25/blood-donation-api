using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodUpdateModel : EmergencyBloodCreateModel
    {
        public string? ReasonReject { get; set; }
        public EmergencyBloodRequestEnum? Status { get; set; } 
    }
}
