using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class BloodDonationService : IBloodDonationService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodDonationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<object> SearchAsync(string? searchKey, int? pageIndex, int? pageSize, BloodDonationStatusEnum? status)
        {
            // Biểu thức lọc
            Expression<Func<BloodDonation.Domain.Entities.BloodDonation, bool>> filter = b =>
                (!status.HasValue || b.Status == status) &&
                !b.IsDeleted;

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.BloodDonationRepository.Search(
                filter: filter,
                includeProperties: "BloodDonationRequest,BloodDonationRequest.User,BloodDonationRequest.HealthCheckForm",
                orderBy: q => q.OrderByDescending(b => b.DonationDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.BloodDonationRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize ?? 10;
            int actualPageIndex = pageIndex ?? 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<BloodDonationViewModel>>(pagedData);

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
        public async Task<bool> UpdateStatusAsync(Guid id, BloodDonationStatusEnum status, CancellationToken cancellationToken = default)
        {
            //Get blood donation by id
            var bloodDonation = await unitOfWork.BloodDonationRepository.GetByCondition(b => b.Id == id);
            if (bloodDonation == null || bloodDonation.IsDeleted)
            {
                return false; //Not found or deleted
            }

            if (bloodDonation.Status == status)
            {
                return true; //No change needed
            }

            if (bloodDonation.Status == BloodDonationStatusEnum.Donated)
            {
                return false; //Cannot change status from Donated to another status
            }
            if(status == BloodDonationStatusEnum.Donated)
            {
                var bloodComponent = await unitOfWork.BloodComponentRepository.GetByCondition(b => b.Id == BloodComponentType.WholeBlood.Id);

                await unitOfWork.BloodStorageRepository.CreateAsync(new BloodStorage
                {
                    Volume = bloodDonation.Volume,
                    BloodDonationId = bloodDonation.Id,
                    ExpiredDate = DateTime.Now.AddDays(bloodComponent.ShelfLifeInDay)
                });
            }

            // Update status
            bloodDonation.Status = status;

            //Save changes
            unitOfWork.BloodDonationRepository.Update(bloodDonation);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
