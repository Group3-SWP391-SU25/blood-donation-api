using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Models.BloodGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodChecks
{
    public class BloodCheckViewModel : BaseModel
    {
        public double WBC { get; set; }
        public double RBC { get; set; }
        public double HGB { get; set; }
        public double HCT { get; set; }
        public double MCV { get; set; }
        public double MCH { get; set; }
        public double MCHC { get; set; }
        public double PLT { get; set; }
        public double MPV { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CheckedDate { get; set; }
        public bool IsSafe { get; set; }
        public List<string> ValidationErrors { get; set; } = new();

        public Guid BloodGroupId { get; set; }
        public BloodGroupViewModel? BloodGroup { get; set; }

        public Guid BloodDonationId { get; set; }
        public BloodDonationViewModel? BloodDonation { get; set; }
    }
}
