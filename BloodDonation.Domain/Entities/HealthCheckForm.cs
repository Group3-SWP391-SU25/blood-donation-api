using System;

namespace BloodDonation.Domain.Entities;

public class HealthCheckForm : BaseEntity
{
    public int Age { get; set; }
    public double Weight { get; set; }
    public double VolumeBloodDonated { get; set; }
    public double Hemoglobin { get; set; }
    public bool IsInfectiousDisease { get; set; }
    public bool IsPregnant { get; set; }
    public bool IsUsedAlcoholRecently { get; set; }
    public bool HasChronicDisease { get; set; }
    public bool HasUnsafeSexualBehaviourOrSameSexSexualContact { get; set; }
    public string Note { get; set; } = string.Empty;
    public Guid BloodDonateRequestId { get; set; }
    public virtual BloodDonationRequest BloodDonationRequest { get; set; } = new();
}