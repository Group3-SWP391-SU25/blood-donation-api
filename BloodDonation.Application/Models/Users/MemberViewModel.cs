using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.Users
{
    public class MemberViewModel
    {
        public UserViewModel User { get; set; }
        public DateTime? LastDonationDate { get; set; }
        public int? LastDonationDaysAgo
        {
            get
            {
                if (LastDonationDate == null)
                    return null;

                return (DateTime.UtcNow.Date - LastDonationDate.Value.Date).Days;
            }
        }
    }
}
