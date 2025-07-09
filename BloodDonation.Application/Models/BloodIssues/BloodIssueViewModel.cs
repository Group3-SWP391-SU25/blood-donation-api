using BloodDonation.Application.Models.BloodStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.BloodIssues
{
    public class BloodIssueViewModel : BaseModel
    {
        public Guid BloodStorageId { get; set; }
        public virtual BloodStorageViewModel BloodStorage { get; set; }
        public int Volume { get; set; }
        public DateTime DateIssue { get; set; }

    }
}
