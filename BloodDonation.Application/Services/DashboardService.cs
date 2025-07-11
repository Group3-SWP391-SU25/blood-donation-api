using BloodDonation.Application.Models.Nurse;
using BloodDonation.Application.Models.Supervisor;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                                                            || p.Status == Domain.Enums.BloodDonationStatusEnum.Checked))
            };

            var exportRequests = await unitOfWork.EmergencyBloodRepository.GetAllAsync();

            var exportStats = new Models.Nurse.ExportRequestStats
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

            var bloodGroupsFromDb = await unitOfWork.BloodGroupRepository.GetAllAsync();
            var bloodGroupKeys = bloodGroupsFromDb
                .Select(bg => new
                {
                    Key = bg.Type + bg.RhFactor,
                    Display = bg.Type + bg.RhFactor
                })
                .Distinct()
                .ToList();

            // Lấy tất cả túi máu có nhóm máu, chưa xóa
            var bloodStoragesRaw = await unitOfWork.BloodStorageRepository
                .GetAllAsync(includes: b => b.BloodGroup);

            var bloodStorages = bloodStoragesRaw
                .Where(b => !b.IsDeleted && b.BloodGroupId != null && b.Volume > 0)
                .ToList();

            var groupedStorages = bloodStorages
                .GroupBy(b => b.BloodGroup!.Type + b.BloodGroup.RhFactor)
                .ToDictionary(g => g.Key, g => g.ToList());

            var totalVolume = bloodStorages.Sum(b => b.Volume);

            // Tạo bloodDistribution đầy đủ 8 nhóm từ DB
            var bloodDistribution = bloodGroupKeys
                .Select(bg =>
                {
                    var group = groupedStorages.ContainsKey(bg.Key) ? groupedStorages[bg.Key] : new List<BloodDonation.Domain.Entities.BloodStorage>();

                    var total = group.Sum(b => b.Volume);
                    var safe = group.Where(b => b.Status == BloodStorageStatusEnum.Safe).Sum(b => b.Volume);
                    var unsafeVolume = total - safe;

                    var totalBags = group.Count;
                    var safeBags = group.Count(b => b.Status == BloodStorageStatusEnum.Safe);
                    var unsafeBags = totalBags - safeBags;

                    return new Models.Nurse.BloodDistributionDto
                    {
                        Type = bg.Display,
                        TotalVolume = Math.Round(total, 1),
                        SafeVolume = Math.Round(safe, 1),
                        UnsafeVolume = Math.Round(unsafeVolume, 1),
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
        public async Task<SupervisorDashboardViewModel> GetSupervisorDashboard()
        {

            var bloodReceivedStats = new Models.Supervisor.BloodReceivedStats
            {
                Total = await unitOfWork.BloodDonationRepository.Count(b => b.Status != BloodDonationStatusEnum.Cancelled),
                Checked = await unitOfWork.BloodDonationRepository.Count(b => b.Status == BloodDonationStatusEnum.Checked),
                Unchecked = await unitOfWork.BloodDonationRepository.Count(b => b.Status == BloodDonationStatusEnum.Donated),
                InProgress = await unitOfWork.BloodDonationRepository.Count(b => b.Status == BloodDonationStatusEnum.InProgress)
            };

            var exportStats = new Models.Supervisor.ExportRequestStats
            {
                Total = await unitOfWork.EmergencyBloodRepository.Count(x => x.Status != EmergencyBloodRequestEnum.Cancel),
                Pending = await unitOfWork.EmergencyBloodRepository.Count(x => x.Status == Domain.Enums.EmergencyBloodRequestEnum.Pending),
                Approved = await unitOfWork.EmergencyBloodRepository.Count(x => x.Status == Domain.Enums.EmergencyBloodRequestEnum.Processing),
                Finished = await unitOfWork.EmergencyBloodRepository.Count(x => x.Status == Domain.Enums.EmergencyBloodRequestEnum.Finish),
                Rejected = await unitOfWork.EmergencyBloodRepository.Count(x => x.Status == Domain.Enums.EmergencyBloodRequestEnum.Reject),
            };
            var bloodGroupsFromDb = await unitOfWork.BloodGroupRepository.GetAllAsync();
            var bloodGroupKeys = bloodGroupsFromDb
                .Select(bg => new
                {
                    Key = bg.Type + bg.RhFactor,
                    Display = bg.Type + bg.RhFactor
                })
                .Distinct()
                .ToList();

            // Lấy tất cả túi máu có nhóm máu, chưa xóa
            var bloodStoragesRaw = await unitOfWork.BloodStorageRepository
                .GetAllAsync(includes: b => b.BloodGroup);

            var bloodStorages = bloodStoragesRaw
                .Where(b => !b.IsDeleted && b.BloodGroupId != null && b.Volume > 0)
                .ToList();

            var groupedStorages = bloodStorages
                .GroupBy(b => b.BloodGroup!.Type + b.BloodGroup.RhFactor)
                .ToDictionary(g => g.Key, g => g.ToList());

            var totalVolume = bloodStorages.Sum(b => b.Volume);

            // Tạo bloodDistribution đầy đủ 8 nhóm từ DB
            var bloodDistribution = bloodGroupKeys
                .Select(bg =>
                {
                    var group = groupedStorages.ContainsKey(bg.Key) ? groupedStorages[bg.Key] : new List<BloodDonation.Domain.Entities.BloodStorage>();

                    var total = group.Sum(b => b.Volume);
                    var safe = group.Where(b => b.Status == BloodStorageStatusEnum.Safe).Sum(b => b.Volume);
                    var unsafeVolume = total - safe;

                    var totalBags = group.Count;
                    var safeBags = group.Count(b => b.Status == BloodStorageStatusEnum.Safe);
                    var unsafeBags = totalBags - safeBags;

                    return new Models.Supervisor.BloodDistributionDto
                    {
                        Type = bg.Display,
                        TotalVolume = Math.Round(total, 1),
                        SafeVolume = Math.Round(safe, 1),
                        UnsafeVolume = Math.Round(unsafeVolume, 1),
                        TotalBags = totalBags,
                        SafeBags = safeBags,
                        UnsafeBags = unsafeBags,
                    };
                })
                .OrderByDescending(x => x.TotalVolume)
                .ToList();
            return new SupervisorDashboardViewModel
            {
                BloodReceivedStats = bloodReceivedStats,
                ExportRequestStats = exportStats,
                BloodDistribution = bloodDistribution
            };
        }
    }
}
