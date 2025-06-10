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
    }
}
