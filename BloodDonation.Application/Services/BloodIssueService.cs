using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class BloodIssueService : IBloodIssueService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodIssueService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<BloodIssueViewModel> CreateAsync(BloodIssueCreateModel bloodIssueModel, CancellationToken cancellationToken = default)
        {
            var bloodIssue = unitOfWork.Mapper.Map<BloodIssue>(bloodIssueModel);
            var bloodStorage = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == bloodIssue.BloodStorageId,
                cancellationToken,
                includes: [x => x.BloodGroup!, x => x.BloodComponent, x => x.BloodDonate]);
            if (bloodStorage is not null)
            {
                if (bloodStorage.Status == BloodStorageStatusEnum.Safe)
                {
                    if (bloodIssue.Status == BloodIssueStatusEnum.Approved)
                    {
                        bloodStorage.Status = BloodStorageStatusEnum.Locked;
                        unitOfWork.BloodStorageRepository.Update(bloodStorage);
                    }
                    await unitOfWork.BloodIssueRepository.CreateAsync(bloodIssue);
                    await unitOfWork.SaveChangesAsync();
                    return unitOfWork.Mapper.Map<BloodIssueViewModel>(bloodIssue);
                }
                /// Other state is not valid to create blood issue
                else throw new InvalidOperationException("Máu đang trong trạng thái không thể xuất kho");
            }
            else throw new InvalidOperationException("Blood Storage Not found");
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var bloodIssue = await unitOfWork.BloodIssueRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: [x => x.BloodStorage]);
            if (bloodIssue is not null)
            {
                if (bloodIssue.Status == BloodIssueStatusEnum.Done)
                {
                    throw new InvalidOperationException("Đã cấp máu khỏi kho! Không thể xoá");
                }
                else
                {
                    unitOfWork.BloodIssueRepository.SoftRemove(bloodIssue);
                    return await unitOfWork.SaveChangesAsync(cancellationToken);

                }
            }
            else throw new InvalidOperationException("Không tìm thấy yêu cầu nhận máu phù hợp");
        }

        public async Task<object> GetAll(int pageIndex,
            int pageSize,
            string? search = "",
            BloodIssueStatusEnum? status = null,
            CancellationToken cancellationToken = default)
        {
            // Biểu thức lọc
            Expression<Func<BloodIssue, bool>> filter = b =>
                (!status.HasValue || b.Status == status);
            int totalRecords = await unitOfWork.BloodIssueRepository.Count(filter);

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.BloodIssueRepository.Search(
                filter: filter,
                includeProperties: "BloodStorage,BloodStorage.BloodComponent,BloodStorage.BloodGroup",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);
            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<BloodIssueViewModel>>(pagedData);

            // Trả kết quả
            return new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageIndex = actualPageIndex,
                PageSize = actualPageSize,
                Records = mappedData
            };
        }

        public async Task<BloodIssueViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var bloodIssue = await unitOfWork.BloodIssueRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken,
                x => x.BloodStorage) ?? throw new InvalidOperationException("Không tìm thấy yêu cầu xuất kho");
            bloodIssue.BloodStorage = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == bloodIssue.BloodStorageId,
                cancellationToken,
                includes: [x => x.BloodGroup!, x => x.BloodComponent, x => x.BloodDonate]) ?? new();
            return unitOfWork.Mapper.Map<BloodIssueViewModel>(bloodIssue);

        }
        /// <summary>
        /// Split The Blood Storage Into Two Component Storage
        /// </summary>
        /// <param name="bloodStorage"></param>
        /// <param name="actualVolume"></param>
        /// <returns></returns>
        private async Task<BloodStorage?> SplitBloodStorage(BloodStorage bloodStorage,
            double actualVolume)
        {
            bloodStorage.Childs = [new BloodStorage()
            {

                BloodGroupId = bloodStorage.BloodGroupId,

                BloodComponentId = bloodStorage.BloodComponentId,
                Status = bloodStorage.Status,
                ParentStorageId = bloodStorage.Id,
                ExpiredDate = bloodStorage.ExpiredDate,
                Volume = actualVolume
            },
            new BloodStorage()
            {

                BloodGroupId = bloodStorage.BloodGroupId,

                BloodComponentId = bloodStorage.BloodComponentId,
                Status = bloodStorage.Status,
                ParentStorageId = bloodStorage.Id,
                ExpiredDate = bloodStorage.ExpiredDate,
                Volume =  bloodStorage.Volume - actualVolume
            }];
            unitOfWork.BloodStorageRepository.Update(bloodStorage);
            await unitOfWork.SaveChangesAsync();
            return bloodStorage.Childs.FirstOrDefault();
        }
        public async Task<BloodIssueViewModel> UpdateAsync(Guid id, BloodIssueUpdateModel updateModel,
            CancellationToken cancellationToken = default)
        {
            var bloodIssue = await unitOfWork.BloodIssueRepository.FirstOrDefaultAsync(x => x.Id == id,
               cancellationToken,
               includes: [x => x.BloodStorage, x => x.EmergencyBloodRequest!]) ?? throw new InvalidOperationException("Không tìm thấy yêu cầu xuất kho");
            unitOfWork.Mapper.Map(updateModel, bloodIssue);
            switch (bloodIssue.Status)
            {
                case BloodIssueStatusEnum.Approved:
                    // Locked the blood Storage 
                    bloodIssue.BloodStorage.Status = BloodStorageStatusEnum.Locked;
                    if (bloodIssue.Volume < bloodIssue.BloodStorage.Volume)
                    {
                        // Split it into two item of storage
                        //var bloodStorage = await SplitBloodStorage(bloodIssue.BloodStorage, bloodIssue.Volume);
                        //bloodIssue.BloodStorage = null!;
                        //bloodIssue.BloodStorageId = bloodStorage?.Id ?? Guid.Empty;
                        bloodIssue.BloodStorage.Status = BloodStorageStatusEnum.Locked;
                    }

                    break;
                case BloodIssueStatusEnum.Pending:
                    // Do Nothing
                    break;
                case BloodIssueStatusEnum.Done:
                    bloodIssue.BloodStorage.Status = BloodStorageStatusEnum.Exported;
                    // Set Storage To Exported
                    break;
            }
            if (bloodIssue.EmergencyBloodRequest != null
                && (bloodIssue.Status == BloodIssueStatusEnum.Done))
            {
                var emergencyReq = bloodIssue.EmergencyBloodRequest;
                emergencyReq.Status = BloodEmergencyStatusEnum.Done;
                unitOfWork.EmergencyBloodRequestRepository.Update(emergencyReq);
            }

            unitOfWork.BloodIssueRepository.Update(bloodIssue);
            await unitOfWork.SaveChangesAsync();
            return unitOfWork.Mapper.Map<BloodIssueViewModel>(bloodIssue);

        }
    }
}
