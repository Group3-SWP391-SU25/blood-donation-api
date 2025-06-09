namespace BloodDonation.Domain.Entities
{
    public class BloodDonation : BaseEntity
    {
        public string BloodType { get; set; } = string.Empty;
        public double Volume { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? DonationDate { get; set; }

        #region Config Relationship
        public virtual BloodStorage? BloodStorage { get; set; }
        public Guid BloodDonationRequestId { get; set; } = Guid.Empty;
        public virtual BloodDonationRequest BloodDonationRequest { get; set; }

        #endregion




    }
}
