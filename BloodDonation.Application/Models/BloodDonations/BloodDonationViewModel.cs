using BloodDonation.Application.Models.BloodChecks;
using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodDonations
{
    public class BloodDonationViewModel : BaseModel
    {
        public string? Code { get; set; } = string.Empty;
        public BloodTypeEnum BloodType { get; set; }
        public double Volume { get; set; }
        public string? Description { get; set; } 
        public BloodDonationStatusEnum Status { get; set; }
        public DateTime? DonationDate { get; set; }
        public Guid BloodDonationRequestId { get; set; }
        public virtual BloodDonationRequestViewModel? BloodDonationRequest { get; set; }
        public virtual BloodCheckViewModel? BloodCheck { get; set; }
    }
}
