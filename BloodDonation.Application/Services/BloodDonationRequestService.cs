using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
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
    public class BloodDonationRequestService : IBloodDonationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        public BloodDonationRequestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<object> SearchAsync(int? pageIndex = 1, int? pageSize = 10,
                                              BloodDonationRequestStatus? status = null, string? keyword = null)
        {
            Expression<Func<BloodDonationRequest, bool>> filter = b =>
                (!status.HasValue || b.Status == status.Value) &&
                (b.IsDeleted == false) &&
                (string.IsNullOrEmpty(keyword) ||
                    (b.User.FullName != null && b.User.FullName.Contains(keyword)));

            var pagedData = await unitOfWork.BloodDonationRequestRepository.Search(
                filter: filter,
                includeProperties: "User,HealthCheckForm",
                orderBy: q => q.OrderByDescending(b => b.DonatedDateRequest),
                pageIndex: pageIndex,
                pageSize: pageSize);

            int totalRecords = await unitOfWork.BloodDonationRequestRepository.Count(filter);

            int actualPageSize = pageSize ?? 10;
            int actualPageIndex = pageIndex ?? 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            var mappedData = unitOfWork.Mapper.Map<List<BloodDonationRequestViewModel>>(pagedData);

            return new
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                PageIndex = actualPageIndex,
                PageSize = actualPageSize,
                Records = mappedData
            };
        }

        public async Task<BloodDonationRequestViewModel?> CreateAsync(BloodDonationRequestCreateModel model, CancellationToken cancellationToken = default)
        {
            // Validate the model
            var user = await unitOfWork.UserRepository.GetByCondition(u => u.Id == model.UserId);
            if (user == null)
            {
                throw new ArgumentException($"Người dùng với ID {model.UserId} không tồn tại.");
            }
            var bloodDonationRequest = unitOfWork.Mapper.Map<Domain.Entities.BloodDonationRequest>(model);
            await unitOfWork.BloodDonationRequestRepository.CreateAsync(bloodDonationRequest, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var bloodDonationRequestWithUser = await unitOfWork
                .BloodDonationRequestRepository
                .GetByCondition(b => b.Id == bloodDonationRequest.Id, includeProperties: "User");
            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(bloodDonationRequestWithUser);
        }
        public async Task<BloodDonationRequestViewModel> UpdatePartialAsync(BloodDonationRequestUpdateModel model, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.Id.ToString() == model.Id, includeProperties: "User");

            if (entity == null)
                throw new KeyNotFoundException($"Không tìm thấy yêu cầu hiến máu với ID {model.Id}");

            // Validate ngày không nằm trong quá khứ
            if (model.DonatedDateRequest.Date < DateTime.Today)
                throw new ArgumentException("Ngày yêu cầu không được nhỏ hơn hôm nay");

            // Update fields
            entity.BloodType = model.BloodType;
            entity.DonatedDateRequest = model.DonatedDateRequest;
            entity.TimeSlot = model.TimeSlot;
            entity.HasBloodTransfusionHistory = model.HasBloodTransfusionHistory;
            entity.HasRecentIllnessOrMedication = model.HasRecentIllnessOrMedication;
            entity.HasRecentSkinPenetrationOrSurgery = model.HasRecentSkinPenetrationOrSurgery;
            entity.HasDrugInjectionHistory = model.HasDrugInjectionHistory;
            entity.HasVisitedEpidemicArea = model.HasVisitedEpidemicArea;

            unitOfWork.BloodDonationRequestRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.BloodDonationRequestRepository.GetByCondition(b => b.Id.ToString() == id);
            if (entity == null)
                throw new KeyNotFoundException($"Không tìm thấy yêu cầu hiến máu với ID {id}");

            entity.IsDeleted = true; // Đánh dấu là đã xóa
            unitOfWork.BloodDonationRequestRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task<BloodDonationRequestViewModel?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.Id.ToString() == id, includeProperties: "User,HealthCheckForm");

            if (entity == null)
                return null;

            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(entity);
        }
        public async Task<BloodDonationRequestViewModel?> UpdateStatusAsync(string id, string? rejectNote, BloodDonationRequestStatus status, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.Id.ToString() == id, includeProperties: "User");

            if (entity == null)
                throw new KeyNotFoundException($"Không tìm thấy yêu cầu hiến máu với ID {id}");
            if (status == BloodDonationRequestStatus.Rejected)
            {
                if(rejectNote == null || rejectNote.Length < 2)
                    throw new ArgumentException("Lý do từ chối phải có ít nhất 2 ký tự.");
                entity.ReasonReject = rejectNote;
            }

            //add logic for accepted and rejected status

            entity.Status = status;
            unitOfWork.BloodDonationRequestRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(entity);
        }
        public async Task<List<BloodDonationRequestViewModel>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var requests = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.UserId == userId && !b.IsDeleted, includeProperties: "User,HealthCheckForm");

            return unitOfWork.Mapper.Map<List<BloodDonationRequestViewModel>>(requests);
        }
        public async Task<object> GetByUserIdAsync(Guid userId, int? pageIndex = 1, int? pageSize = 10, CancellationToken cancellationToken = default)
        {
            Expression<Func<BloodDonationRequest, bool>> filter = b =>
                b.UserId == userId && !b.IsDeleted;

            var pagedData = await unitOfWork.BloodDonationRequestRepository.Search(
                filter: filter,
                includeProperties: "User,HealthCheckForm",
                orderBy: q => q.OrderByDescending(b => b.DonatedDateRequest),
                pageIndex: pageIndex,
                pageSize: pageSize);

            int totalRecords = await unitOfWork.BloodDonationRequestRepository.Count(filter);

            int actualPageSize = pageSize ?? 10;
            int actualPageIndex = pageIndex ?? 1;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            var mappedData = unitOfWork.Mapper.Map<List<BloodDonationRequestViewModel>>(pagedData);

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
