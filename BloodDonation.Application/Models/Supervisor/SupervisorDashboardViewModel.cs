using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.Supervisor
{
    public class SupervisorDashboardViewModel
    {
        public BloodReceivedStats BloodReceivedStats { get; set; } = new();
        public ExportRequestStats ExportRequestStats { get; set; } = new();
        public List<BloodDistributionDto> BloodDistribution { get; set; } = new();
    }
    public class BloodDistributionDto
    {
        public string Type { get; set; } = null!;
        public double TotalVolume { get; set; }
        public double SafeVolume { get; set; }
        public double UnsafeVolume { get; set; }
        public int TotalBags { get; set; }
        public int SafeBags { get; set; }
        public int UnsafeBags { get; set; }
    }
    public class ExportRequestStats
    {
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Finished { get; set; }
        public int Rejected { get; set; }

        public int Total { get; set; }
    }
    public class BloodReceivedStats
    {
        public int Total { get; set; }
        public int Checked { get; set; }
        public int Unchecked { get; set; }
        public int InProgress { get; set; }
    }
}
