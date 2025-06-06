using System;
using System.Collections.Generic;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities;

public class BloodDonationRequest : BaseEntity
{
    public BloodTypeEnum BloodType { get; set; } = BloodTypeEnum.Unknown;
    public BloodDonationRequestStatus Status { get; set; } = BloodDonationRequestStatus.Pending;
    public string ReasonReject { get; set; } = string.Empty;
    public DateTime DonatedDateRequest { get; set; } = DateTime.Now;
    public TimeSlotEnum TimeSlot { get; set; }
    public bool HasBloodTransfusionHistory { get; set; }            
    public bool HasRecentIllnessOrMedication { get; set; }           
    public bool HasRecentSkinPenetrationOrSurgery { get; set; }      
    public bool HasDrugInjectionHistory { get; set; }                
    public bool HasVisitedEpidemicArea { get; set; }
    public List<BloodDonationRequirement> BloodDonationRequirements { get; set; } = [];

    #region Relationship Configuration 
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
    public virtual HealthCheckForm? HealthCheckForm { get; set; } = null;
    #endregion
}