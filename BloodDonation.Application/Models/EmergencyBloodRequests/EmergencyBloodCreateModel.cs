namespace BloodDonation.Application.Models.EmergencyBloodRequests
{
    public class EmergencyBloodCreateModel
    {
        public string? Address { get; set; } = string.Empty;
        public double? Volume { get; set; }
        public Guid? BloodComponentId { get; set; }
        public Guid? BloodGroupId { get; set; }

    }
}
