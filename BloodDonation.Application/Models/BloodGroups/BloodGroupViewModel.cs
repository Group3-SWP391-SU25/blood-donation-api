using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodGroups
{
    public class BloodGroupViewModel : BaseModel
    {
        public string Type { get; set; } = string.Empty;
        public string RhFactor { get; set; } = string.Empty;
        public string DisplayName => $"{Type}{RhFactor}";
    }
}
