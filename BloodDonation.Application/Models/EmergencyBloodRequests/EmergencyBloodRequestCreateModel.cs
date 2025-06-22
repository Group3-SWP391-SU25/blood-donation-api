namespace BloodDonation.Application.Models.BloodEmergencyRequests
{
    public class EmergencyBloodRequestCreateModel
    {
        public double Volume { get; set; }
        public string Address { get; set; } = string.Empty;

    }
}
