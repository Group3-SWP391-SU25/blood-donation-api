using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodComponents
{
    public class BloodComponentViewModel : BaseModel 
    {
        public string Name { get; set; } 
        public double MinStorageTemerature { get; set; }
        public double MaxStorageTemerature { get; set; }
        public double ShelfLifeInDay { get; set; }
        public string Status { get; set; } 
    }
}
