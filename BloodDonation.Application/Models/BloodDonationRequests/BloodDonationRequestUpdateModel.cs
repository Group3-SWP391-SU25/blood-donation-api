using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodDonationRequests
{
    public class BloodDonationRequestUpdateModel
    {
        [JsonIgnore]
        public string Id { get; set; } 

        [Required]
        [EnumDataType(typeof(BloodTypeEnum))]
        public BloodTypeEnum BloodType { get; set; }

        //[Required]
        //[EnumDataType(typeof(BloodDonationRequestStatus))]
        //public BloodDonationRequestStatus Status { get; set; }

        //public string ReasonReject { get; set; } = string.Empty;

        [Required]
        public DateTime DonatedDateRequest { get; set; }

        [Required]
        [EnumDataType(typeof(TimeSlotEnum))]
        public TimeSlotEnum TimeSlot { get; set; }

        public bool HasBloodTransfusionHistory { get; set; }
        public bool HasRecentIllnessOrMedication { get; set; }
        public bool HasRecentSkinPenetrationOrSurgery { get; set; }
        public bool HasDrugInjectionHistory { get; set; }
        public bool HasVisitedEpidemicArea { get; set; }
    }
}
