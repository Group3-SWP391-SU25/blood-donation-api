using BloodDonation.Application.Models.BloodEmergencyRequests;
using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class EmergencyBloodService : IEmergencyBloodService
    {
        private readonly IUnitOfWork unitOfWork;
        private IClaimsService claimsService;
        private readonly IBloodIssueService bloodIssueService;
        public EmergencyBloodService(IUnitOfWork unitOfWork,
            IClaimsService claimsService,
            IBloodIssueService bloodIssueService)
        {
            this.claimsService = claimsService;
            this.unitOfWork = unitOfWork;
            this.bloodIssueService = bloodIssueService;
        }
        public async Task<EmergencyBloodRequestViewModel> CreateAsync(EmergencyBloodRequestCreateModel model,
            CancellationToken cancellationToken = default)
        {
            var item = unitOfWork.Mapper.Map<EmergencyBloodRequest>(model);
            var currentUser = claimsService.CurrentUser
                != Guid.Empty ?
                claimsService.CurrentUser : throw new InvalidOperationException("Không tìm thấy người dùng cụ thể, Không thể tạo yêu cầu hiến khẩn cấp");
            var user = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == currentUser, includes: [x => x.BloodGroup!]);
            if (user!.BloodGroup == null || user.BloodGroup.Id == Guid.Empty)
            {
                throw new InvalidOperationException("Nguời dùng hiện tại chưa assign nhóm máu, không thể tạo nhu cầu hiến");
            }
            item.UserId = currentUser;
            await unitOfWork.EmergencyBloodRequestRepository.CreateAsync(item, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            return await GetById(item.Id, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var item = await unitOfWork.EmergencyBloodRequestRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (item!.Status == BloodEmergencyStatusEnum.Done) throw new InvalidOperationException("Yêu cầu hiến khẩn cấp đã được thông qua và máu đã được xuất kho, không thể xoá");
            unitOfWork.EmergencyBloodRequestRepository.SoftRemove(item);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<object> GetAll(int pageIndex, int pageSize, string? search = "", BloodEmergencyStatusEnum? status = null, CancellationToken cancellationToken = default)
        {
            Expression<Func<EmergencyBloodRequest, bool>> filter = b =>
                (!status.HasValue || b.Status == status);

            // Truy vấn dữ liệu đã lọc, phân trang
            var pagedData = await unitOfWork.EmergencyBloodRequestRepository.Search(
                filter: filter,
                includeProperties: "BloodIssue,BloodIssue.BloodStorage,User,User.BloodGroup",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.EmergencyBloodRequestRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<EmergencyBloodRequestViewModel>>(pagedData);

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

        public async Task<EmergencyBloodRequestViewModel> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return unitOfWork.Mapper.Map<EmergencyBloodRequestViewModel>(await unitOfWork.EmergencyBloodRequestRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: [x => x.BloodIssue, x => x.BloodIssue.BloodStorage, x => x.BloodIssue.BloodStorage.BloodComponent, x => x.BloodIssue.BloodStorage.BloodGroup!,
                x => x.User,x => x.User.BloodGroup!, x=> x.User]));

        }
        private async Task CreateBloodIssue()
        {

        }

        public async Task<bool> UpdateAsync(Guid id,
            EmergencyBloodRequestUpdateModel model,
            CancellationToken cancellationToken = default)
        {
            var item = await unitOfWork.EmergencyBloodRequestRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: [x => x.BloodIssue, x => x.BloodIssue.BloodStorage]) ?? throw new InvalidOperationException("Không tìm thấy yêu cầu máu khẩn cấp");

            unitOfWork.Mapper.Map(model, item);
            switch (model.Status)
            {
                case BloodEmergencyStatusEnum.Pending:
                    // Do Nothing
                    break;
                case BloodEmergencyStatusEnum.Reject:
                    break;
                case BloodEmergencyStatusEnum.Approved:
                    //Create Issue, Pending Approval
                    var bloodStorage = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == model.BloodStorageId);
                    var res = await bloodIssueService.CreateAsync(new Models.BloodIssues.BloodIssueCreateModel()
                    {
                        BloodStorageId = model.BloodStorageId,
                        Reason = "Emergency",
                        ReceiverName = "",
                        Volume = item.Volume,
                        Status = BloodIssueStatusEnum.Approved
                    });


                    item.BloodIssueId = res.Id;
                    await unitOfWork.SaveChangesAsync();
                    break;
                case BloodEmergencyStatusEnum.Done:
                    // Issue Done, Trigger When Blood Isse Status Updated To Exported
                    break;
            }
            unitOfWork.EmergencyBloodRequestRepository.Update(item);
            return await unitOfWork.SaveChangesAsync();
        }
    }
}
