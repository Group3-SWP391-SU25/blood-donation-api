using BloodDonation.Application.Models.BloodComponents;
using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodStorage
{
    public class BloodStorageViewModel : BaseModel
    {
        public double Volume { get; set; }
        public BloodStorageStatusEnum Status { get; set; }
        public DateTime ExpiredDate { get; set; }

        public Guid BloodDonationId { get; set; }
        public BloodDonationViewModel BloodDonate { get; set; }
        public Guid BloodComponentId { get; set; } 
        public BloodComponentViewModel BloodComponent { get; set; }

        public Guid? BloodGroupId { get; set; }
        public string? BloodGroup { get; set; }
    }
}
