using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Domain.Entities
{
    public class BloodCheck : BaseEntity
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
        public DateTime CheckedDate { get; set; } = DateTime.UtcNow;

        #region Config Relationship
        public Guid BloodGroupId { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        public Guid BloodDonationId { get; set; }
        public virtual BloodDonation BloodDonation { get; set; }

        #endregion
    }
}
