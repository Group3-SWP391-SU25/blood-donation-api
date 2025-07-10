using BloodDonation.Application.Models.Nurse;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<NurseDashboard> GetNurseDashboardAsync()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            var registrations = await unitOfWork.BloodDonationRequestRepository.Search(b => b.DonatedDateRequest == today, pageIndex: 1, pageSize: 99999);

            var registrationStats = new RegistrationStats
            {
                Total = registrations.Count(),
                Pending = registrations.Count(r => r.Status == Domain.Enums.BloodDonationRequestStatus.Pending),
                Approved = registrations.Count(r => r.Status == Domain.Enums.BloodDonationRequestStatus.Approved),
                Rejected = registrations.Count(r => r.Status == Domain.Enums.BloodDonationRequestStatus.Rejected),
                Finished = registrations.Count(r => r.Status == Domain.Enums.BloodDonationRequestStatus.Completed),
            };

            var bloodProcesses = await unitOfWork.BloodDonationRepository.GetAllAsync();
              

            var bloodStats = new BloodProcessStats
            {
                InProgress = bloodProcesses.Count(p => p.Status == Domain.Enums.BloodDonationStatusEnum.InProgress),
                CompletedToday = bloodProcesses.Count(p => (p.Status == Domain.Enums.BloodDonationStatusEnum.Donated 
                                                            || p.Status == Domain.Enums.BloodDonationStatusEnum.Checked)    )
            };

            var exportRequests = await unitOfWork.EmergencyBloodRepository.GetAllAsync();

            var exportStats = new ExportRequestStats
            {
                Total = exportRequests.Count,
                Pending = exportRequests.Count(x => x.Status == Domain.Enums.EmergencyBloodRequestEnum.Pending)
            };

            var donors = await unitOfWork.UserRepository.Search(u => u.Role.Name == "MEMBER", pageIndex: 1, pageSize: 9999);

            var donorStats = new DonorStats
            {
                Total = donors.Count(),
                NewThisMonth = donors.Count(u => u.CreatedDate >= startOfMonth)
            };
            var bloodStorages1 = await unitOfWork.BloodStorageRepository
                 .GetAllAsync(includes: b => b.BloodGroup);
            var bloodStorages = bloodStorages1
                 .Where(b => !b.IsDeleted && b.BloodGroupId != null && b.Volume > 0)
                 .ToList();

            var totalVolume = bloodStorages.Sum(b => b.Volume);

            var bloodDistribution = bloodStorages
                .GroupBy(b => new
                {
                    TypeDisplay = b.BloodGroup!.Type + b.BloodGroup.RhFactor // ví dụ: "A+"
                })
                .Select(g =>
                {
                    var total = g.Sum(b => b.Volume);
                    var safe = g.Where(b => b.Status == BloodStorageStatusEnum.Safe).Sum(b => b.Volume);
                    var unsafeVolume = total - safe;

                    var totalBags = g.Count();
                    var safeBags = g.Count(b => b.Status == BloodStorageStatusEnum.Safe);
                    var unsafeBags = totalBags - safeBags;

                    return new BloodDistributionDto
                    {
                        Type = g.Key.TypeDisplay,
                        TotalVolume = total,
                        SafeVolume = safe,
                        UnsafeVolume = unsafeVolume,
                        TotalBags = totalBags,
                        SafeBags = safeBags,
                        UnsafeBags = unsafeBags,
                    };
                })
                .OrderByDescending(x => x.TotalVolume)
                .ToList();

            return new NurseDashboard
            {
                TodayRegistrations = registrationStats,
                BloodProcess = bloodStats,
                ExportRequests = exportStats,
                Donors = donorStats,
                BloodDistribution = bloodDistribution
            };
        }

    }
}
