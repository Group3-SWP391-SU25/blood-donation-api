using BloodDonation.Domain.Enums;

namespace BloodDonation.Domain.Entities
{
    public class BloodDonation : BaseEntity
    {
        public BloodTypeEnum BloodType { get; set; } = BloodTypeEnum.Unknown;
        public double Volume { get; set; }
        public string Description { get; set; } = string.Empty;
        public BloodDonationStatusEnum Status { get; set; } = BloodDonationStatusEnum.InProgress;
        public DateTime? DonationDate { get; set; }

        #region Config Relationship
        public ICollection<BloodStorage>? BloodStorage { get; set; }
        public virtual BloodCheck? BloodCheck { get; set; }
        public Guid BloodDonationRequestId { get; set; }
        public virtual BloodDonationRequest BloodDonationRequest { get; set; }

        #endregion




    }
}
