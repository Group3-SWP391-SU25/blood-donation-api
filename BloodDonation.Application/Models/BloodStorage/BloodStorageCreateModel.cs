using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodStorage
{
    public class BloodStorageCreateModel
    {
        public int Volume { get; set; }
        public Guid BloodComponentId { get; set; }
    }
}
