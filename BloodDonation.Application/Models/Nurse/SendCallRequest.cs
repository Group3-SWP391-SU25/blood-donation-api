using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.Nurse
{
    public class SendCallRequest
    {
        public List<Guid> UserIds { get; set; } = [];
    }

}
