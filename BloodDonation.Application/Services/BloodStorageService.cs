using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class BloodStorageService : IBloodStorageService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodStorageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BloodStorageViewModel> CreateAsync(BloodStorageCreateModel createModel,
            CancellationToken cancellationToken = default)
        {
            var bloodDonation = await unitOfWork.BloodDonationRepository.FirstOrDefaultAsync(x => x.Id == createModel.BloodDonationId,
                cancellationToken: cancellationToken,
                includes: [x => x.BloodCheck!, x => x.BloodDonationRequest, x => x.BloodDonationRequest.User]) ?? throw new InvalidOperationException("Not found Blood Donation");

            var bloodComponent = await unitOfWork.BloodComponentRepository.FirstOrDefaultAsync(x => x.Id == createModel.BloodComponentId);
            var bloodStorage = new BloodStorage()
            {
                BloodComponentId = createModel.BloodComponentId,
                BloodGroupId = bloodDonation.BloodDonationRequest.User.BloodGroupId,
                Status = BloodStorageStatusEnum.UnChecked,
                Volume = bloodDonation.Volume,
                BloodDonationId = bloodDonation.Id,
                ExpiredDate = DateTime.Now.AddDays(bloodComponent?.ShelfLifeInDay ?? 0)
            };
            await unitOfWork.BloodStorageRepository.CreateAsync(bloodStorage);
            await unitOfWork.SaveChangesAsync();
            return unitOfWork.Mapper.Map<BloodStorageViewModel>(bloodStorage);

        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: x => x.BloodIssue!);
            if (item != null)
            {
                if (item.BloodIssue != null)
                {
                    throw new InvalidOperationException("Already Had Blood Issue, should not delete");
                }
                item.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();
                return true;

            }
            else throw new InvalidOperationException("Not found Blood Storage");
        }

        //search
        public async Task<object> SearchAsync(int pageIndex, int pageSize, string? search = "", BloodStorageStatusEnum? status = null, CancellationToken cancellationToken = default)
        {
            // Biểu thức lọc
            Expression<Func<BloodStorage, bool>> filter = b =>
                (!status.HasValue || b.Status == status);

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.BloodStorageRepository.Search(
                filter: filter,
                includeProperties: "BloodDonate,BloodComponent,BloodGroup,BloodDonate.BloodDonationRequest,BloodDonate.BloodDonationRequest.User",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.BloodStorageRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<BloodStorageViewModel>>(pagedData);

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

        public async Task<BloodStorageViewModel> UpdateAsync(Guid id, BloodStorageUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            var bloodStorage = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: [x => x.BloodDonate, x => x.BloodComponent, x => x.BloodIssue!, x => x.BloodGroup!]);
            if (bloodStorage is not null)
            {
                unitOfWork.Mapper.Map(updateModel, bloodStorage);
                unitOfWork.BloodStorageRepository.Update(bloodStorage);
                if (await unitOfWork.SaveChangesAsync())
                {
                    return unitOfWork.Mapper.Map<BloodStorageViewModel>(await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                        includes: [x => x.BloodGroup!, x => x.BloodComponent, x => x.BloodIssue!]));
                }
                else throw new InvalidOperationException("Save Changes Failed");

            }
            else throw new InvalidOperationException("Not found Blood Storage with Id " + id);

        }
    }
}
