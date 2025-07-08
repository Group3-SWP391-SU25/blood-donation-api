using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class BloodIssueService : IBloodIssueService
    {
        private readonly IUnitOfWork unitOfWork;

        public BloodIssueService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateBloodIssueAsync(Guid emergencyBloodRequestId, BloodIssueCreateModel reqDto)
        {
            var emergencyBloodRequest = await unitOfWork.EmergencyBloodRepository.GetByCondition(e => e.Id == emergencyBloodRequestId);
            if (emergencyBloodRequest == null)
            {
                throw new ArgumentException("Không tìm thấy yêu cầu máu khẩn cấp để xuất máu!", nameof(emergencyBloodRequestId));
            }
            if(emergencyBloodRequest.Status != Domain.Enums.EmergencyBloodRequestEnum.Processing)
            {
                throw new InvalidOperationException("Yêu cầu máu khẩn cấp không được phê duyệt, không thể xuất máu!");
            }

            foreach (var bloodStorageId in reqDto.BloodStorageIds)
            {
                var bloodStorage = await unitOfWork.BloodStorageRepository.GetByCondition(e => e.Id == bloodStorageId);
                if (bloodStorage == null)
                {
                    throw new ArgumentException($"Không tìm thấy kho máu với ID {bloodStorageId}!", nameof(bloodStorageId));
                }
                if (bloodStorage.Volume <= 0)
                {
                    throw new InvalidOperationException($"Kho máu với ID {bloodStorageId} không đủ số lượng để xuất!");
                }
                //if(bloodStorage.BloodGroupId != emergencyBloodRequest.BloodGroupId)
                //{
                //    throw new InvalidOperationException($"Kho máu với ID {bloodStorageId} không phù hợp với loại máu yêu cầu!");
                //}
                if(bloodStorage.Status != Domain.Enums.BloodStorageStatusEnum.Safe)
                {
                    throw new InvalidOperationException($"Kho máu với ID {bloodStorageId} không đủ tiêu chuẩn an toàn để xuất!");
                }
                var bloodIssue = new Domain.Entities.BloodIssue
                {
                    BloodStorageId = bloodStorageId,
                    EmergencyBloodRequestId = emergencyBloodRequestId,
                    Volume = bloodStorage.Volume,
                    DateIssue = DateTime.Now,
                    Status = "Đã xuất máu"
                };
                // Reduce the volume in the blood storage
                bloodStorage.Volume -= bloodIssue.Volume;
                unitOfWork.BloodStorageRepository.Update(bloodStorage);
                await unitOfWork.BloodIssueRepository.CreateAsync(bloodIssue);
            }

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBloodIssueAsync(Guid emergencyBloodRequestId, BloodIssueCreateModel reqDto)
        {
            var emergencyBloodRequest = await unitOfWork.EmergencyBloodRepository.GetByCondition(e => e.Id == emergencyBloodRequestId);
            if (emergencyBloodRequest == null)
            {
                throw new ArgumentException("Không tìm thấy yêu cầu máu khẩn cấp để cập nhật!", nameof(emergencyBloodRequestId));
            }

            // 1. Get existing blood issues for the emergency blood request
            var existingIssues = await unitOfWork.BloodIssueRepository
                .Search(e => e.EmergencyBloodRequestId == emergencyBloodRequestId, pageIndex: 1, pageSize: 999);

            // 2. Revert the volumes of existing blood issues back to their respective blood storages
            foreach (var issue in existingIssues)
            {
                var bloodStorage = await unitOfWork.BloodStorageRepository.GetByCondition(s => s.Id == issue.BloodStorageId);
                if (bloodStorage != null)
                {
                    bloodStorage.Volume += issue.Volume;
                    unitOfWork.BloodStorageRepository.Update(bloodStorage);
                }

                await unitOfWork.BloodIssueRepository.DeleteHardAsync(issue);
            }

            // 3. Create new blood issues based on the updated request
            foreach (var bloodStorageId in reqDto.BloodStorageIds)
            {
                var bloodStorage = await unitOfWork.BloodStorageRepository.GetByCondition(e => e.Id == bloodStorageId);
                if (bloodStorage == null)
                {
                    throw new ArgumentException($"Không tìm thấy kho máu với ID {bloodStorageId}!", nameof(bloodStorageId));
                }
                if (bloodStorage.Volume <= 0)
                {
                    throw new InvalidOperationException($"Kho máu với ID {bloodStorageId} không đủ số lượng để xuất!");
                }
                if (bloodStorage.Status != Domain.Enums.BloodStorageStatusEnum.Safe)
                {
                    throw new InvalidOperationException($"Kho máu với ID {bloodStorageId} không đủ tiêu chuẩn an toàn để xuất!");
                }

                var bloodIssue = new Domain.Entities.BloodIssue
                {
                    BloodStorageId = bloodStorageId,
                    EmergencyBloodRequestId = emergencyBloodRequestId,
                    Volume = bloodStorage.Volume,
                    DateIssue = DateTime.Now,
                    Status = "Đã xuất máu"
                };

                bloodStorage.Volume -= bloodIssue.Volume;
                unitOfWork.BloodStorageRepository.Update(bloodStorage);
                await unitOfWork.BloodIssueRepository.CreateAsync(bloodIssue);
            }

            // Save changes to the database
            await unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
