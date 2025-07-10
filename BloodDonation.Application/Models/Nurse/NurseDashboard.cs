using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Models.Nurse
{
    public class NurseDashboard
    {
        public RegistrationStats TodayRegistrations { get; set; } = new();
        public BloodProcessStats BloodProcess { get; set; } = new();
        public ExportRequestStats ExportRequests { get; set; } = new();
        public DonorStats Donors { get; set; } = new();
        public List<BloodDistributionDto> BloodDistribution { get; set; } = new();

    }

    public class RegistrationStats
    {
        public int Total { get; set; }
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Finished { get; set; }
    }

    public class BloodProcessStats
    {
        public int InProgress { get; set; }
        public int CompletedToday { get; set; }
    }

    public class ExportRequestStats
    {
        public int Pending { get; set; }
        public int Total { get; set; }
    }

    public class DonorStats
    {
        public int Total { get; set; }
        public int NewThisMonth { get; set; }
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

}
