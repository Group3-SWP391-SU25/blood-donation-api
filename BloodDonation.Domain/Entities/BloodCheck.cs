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
        public bool IsHIVPositive { get; set; }
        public bool IsHBsAgPositive { get; set; }
        public bool IsAntiHCVPositive { get; set; }
        public bool IsOtherInfectionsPositive { get; set; }
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
