using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodRequestUpdateModel
    {
        public BloodEmergencyStatusEnum Status { get; set; }
        public string Address { get; set; } = string.Empty;
        public double Volume { get; set; }
        public string? ReasonReject { get; set; } = string.Empty;
        public Guid BloodStorageId { get; set; }


    }
}
