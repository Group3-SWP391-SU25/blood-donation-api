using BloodDonation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodDonationRequests
{
    public class BloodDonationRequestViewModel
    {
        public string BloodType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ReasonReject { get; set; } = string.Empty;
        public DateTime DonatedDateRequest { get; set; } = DateTime.Now;
        public string Type { get; set; } = string.Empty;
        public double Volume { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<BloodDonationRequirement> BloodDonationRequirements { get; set; } = [];

    }
}
