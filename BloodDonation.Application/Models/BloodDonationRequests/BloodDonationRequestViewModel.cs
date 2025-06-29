using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Models.HealthCheckForms;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodDonationRequests
{
    public class BloodDonationRequestViewModel : BaseModel
    {
        public string? Code { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public BloodTypeEnum BloodType { get; set; }
        public BloodDonationRequestStatus Status { get; set; }
        public string ReasonReject { get; set; } = string.Empty;
        public DateTime DonatedDateRequest { get; set; }
        public TimeSlotEnum TimeSlot { get; set; }

        public bool HasBloodTransfusionHistory { get; set; }
        public bool HasRecentIllnessOrMedication { get; set; }
        public bool HasRecentSkinPenetrationOrSurgery { get; set; }
        public bool HasDrugInjectionHistory { get; set; }
        public bool HasVisitedEpidemicArea { get; set; }

        // ==== User Info ====
        public string IdentityId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string PhoneNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Addresss { get; set; } = string.Empty;
        public string FrontUrlIdentity { get; set; } = string.Empty;
        public string BackUrlIdentity { get; set; } = string.Empty;

        // ==== Health Check Form ====
        public HealthCheckFormViewModel? HealthCheckForm { get; set; } 
        public BloodDonationViewModel? BloodDonation { get; set; }
    }
}
