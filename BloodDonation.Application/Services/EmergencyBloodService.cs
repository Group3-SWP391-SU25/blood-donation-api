using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;

namespace BloodDonation.Application.Services
{
    public class EmergencyBloodService : IEmergencyBloodService
    {
        private readonly IClaimsService claimsService;
        private readonly IUnitOfWork unitOfWork;
        public EmergencyBloodService(IClaimsService claimsService,
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.claimsService = claimsService;
        }
        public async Task<EmergencyBloodViewModel> CreateAsync(EmergencyBloodCreateModel requestModel,
            CancellationToken cancellationToken = default)
        {
            var currentUserId = claimsService.CurrentUser;
            var currentUser = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == currentUserId,
                cancellationToken, [x => x.Role]);
            if (currentUser == null)
            {
                throw new InvalidOperationException("Không tìm thấy người dùng hiện tại");
            }
            else
            {
                if (currentUser.Role.Name == RoleNames.NURSE)
                {
                    var emerBlood = unitOfWork.Mapper.Map<EmergencyBloodRequest>(requestModel);
                    emerBlood.UserId = currentUserId;
                    emerBlood.Status = EmergencyBloodRequestEnum.Pending;

                    //Make by Bân, Generate unique code for the emergency blood request
                    var existingRequest = await unitOfWork.EmergencyBloodRepository.Search(x => x.Code != null && x.Code.StartsWith("EMR"));

                    int maxNumericCode = 0;
                    foreach (var code in existingRequest)
                    {
                        var numericPart = code.Code!.Substring(3); // Remove "EMR"
                        if (int.TryParse(numericPart, out int num))
                        {
                            maxNumericCode = Math.Max(maxNumericCode, num);
                        }
                    }

                    var nextCode = $"EMR{(maxNumericCode + 1).ToString("D5")}"; // EMR + next number
                    emerBlood.Code = nextCode; //set the generated code

                    await unitOfWork.EmergencyBloodRepository.CreateAsync(emerBlood);

                    if (await unitOfWork.SaveChangesAsync())
                    {
                        return await GetByIdAsync(emerBlood.Id, cancellationToken);
                    }
                    else
                    {
                        throw new InvalidOperationException("Không thể tạo yêu cầu xuất máu");
                    }
                }
                else throw new InvalidOperationException("NURSE mới được tạo yêu cầu xuất máu");
            }
        }

        public async Task<bool> DelAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var emerBlood = await unitOfWork.EmergencyBloodRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
            if (emerBlood == null) throw new InvalidOperationException("Không tìm thấy yêu cầu xuất máu với id: " + id);

            unitOfWork.EmergencyBloodRepository.SoftRemove(emerBlood);
            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<object> GetAllAsync(int pageIndex = 0,
            int pageSize = 10,
            string? searchTerm = null,
            EmergencyBloodRequestEnum? status = null,
            CancellationToken cancellationToken = default)
        {

            Expression<Func<EmergencyBloodRequest, bool>> filter = b =>
                (!status.HasValue || b.Status == status);
            if (!string.IsNullOrEmpty(searchTerm))
                filter.And(x => x.Address.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));

            var pagedData = await unitOfWork.EmergencyBloodRepository.Search(

                filter: filter,
                includeProperties: "BloodComponent,BloodGroup",
                orderBy: q => q.OrderByDescending(b => b.CreatedDate),
                pageIndex: pageIndex,
                pageSize: pageSize);

            // Tổng số bản ghi
            int totalRecords = await unitOfWork.EmergencyBloodRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<List<EmergencyBloodViewModel>>(pagedData);

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

        public async Task<EmergencyBloodViewModel> GetByIdAsync(Guid id,
            CancellationToken cancellationToken = default)
        {
            var emerBlood = await unitOfWork.EmergencyBloodRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken,
                [x => x.BloodComponent, x => x.BloodGroup]) ?? throw new InvalidOperationException("Không tìm thấy yêu cầu xuất máu với Id: " + id);
            return unitOfWork.Mapper.Map<EmergencyBloodViewModel>(emerBlood);

        }

        public async Task<bool> UpdateAsync(Guid id,
            EmergencyBloodUpdateModel model,
            CancellationToken cancellationToken = default)
        {
            var currentUser = await unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == claimsService.CurrentUser,
                cancellationToken,
                [x => x.Role]);
            var emerBlood = await unitOfWork.EmergencyBloodRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                [x => x.BloodComponent, x => x.BloodGroup])
                ?? throw new InvalidOperationException("Không tìm thấy yêu cầu xuất máu với Id " + id);
            var currentState = emerBlood.Status;
            if(model.Status == null) 
            {
                model.Status = currentState;
            }
            if (currentUser.Role.Name == RoleNames.NURSE)
            {
                if (emerBlood.Status == Domain.Enums.EmergencyBloodRequestEnum.Pending)
                {
                    if (model.Status != Domain.Enums.EmergencyBloodRequestEnum.Cancel &&
                        model.Status != Domain.Enums.EmergencyBloodRequestEnum.Pending)
                    {
                        throw new InvalidOperationException("Nurse chỉ có quyền huỷ yêu cầu xuất máu hoặc cập nhật thông tin");
                    }
                }
                else if (model.Status != emerBlood.Status)
                {
                    throw new InvalidOperationException("Nurse không được cập nhật trạng thái hiện tại của yêu cầu xuất máu");
                }
            }
            else
            {
                if (model.Status < emerBlood.Status)
                {
                    throw new InvalidOperationException($"Không thể cập nhật trạng thái trước đó! Status đơn hiện tại là " +
                        $"{Enum.GetName(typeof(EmergencyBloodRequestEnum), emerBlood.Status)}, Trạng thái muốn cập nhật {Enum.GetName(typeof(EmergencyBloodRequestEnum), model.Status)}");
                }
                if (model.Status == EmergencyBloodRequestEnum.Reject)
                {
                    if (string.IsNullOrEmpty(model.ReasonReject)) throw new InvalidOperationException("Reject yêu cầu xuất máu cần lý do cụ thể");
                }
                if (emerBlood.Status == Domain.Enums.EmergencyBloodRequestEnum.Processing)
                {
                    if ((int)model.Status < (int)EmergencyBloodRequestEnum.Processing)
                    {
                        throw new InvalidOperationException("Đơn xuất máu đang được process, chỉ có thể cập nhật Huỷ/ Hoàn Thành");
                    }

                }
                else if (emerBlood.Status == Domain.Enums.EmergencyBloodRequestEnum.Cancel)
                {
                    if (model.Status != Domain.Enums.EmergencyBloodRequestEnum.Cancel)
                    {
                        throw new InvalidOperationException("Đơn xuất máu đã bị huỷ, không thể cập nhật status");
                    }
                }
            }



            unitOfWork.Mapper.Map(model, emerBlood);
            unitOfWork.EmergencyBloodRepository.Update(emerBlood);
            return await unitOfWork.SaveChangesAsync();


        }

        public async Task<object> GetSummaryAsync(DateRangeFilter range)
        {
            DateTime today = DateTime.Today;
            DateTime startDate;
            DateTime endDate;

            switch (range)
            {
                case DateRangeFilter.Today:
                    startDate = today;
                    endDate = today.AddDays(1);
                    break;
                case DateRangeFilter.ThisWeek:
                    int delta = DayOfWeek.Monday - today.DayOfWeek;
                    startDate = today.AddDays(delta);
                    endDate = startDate.AddDays(7);
                    break;
                case DateRangeFilter.ThisMonth:
                    startDate = new DateTime(today.Year, today.Month, 1);
                    endDate = startDate.AddMonths(1);
                    break;
                case DateRangeFilter.All:
                default:
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                    break;
            }

            Expression<Func<EmergencyBloodRequest, bool>> baseFilter = b =>
                b.CreatedDate >= startDate &&
                b.CreatedDate < endDate;
            var total = await unitOfWork.EmergencyBloodRepository.Count(baseFilter);
            var pending = await unitOfWork.EmergencyBloodRepository.Count(baseFilter.And(b => b.Status == EmergencyBloodRequestEnum.Pending));
            var processing = await unitOfWork.EmergencyBloodRepository.Count(baseFilter.And(b => b.Status == EmergencyBloodRequestEnum.Processing));
            var finish = await unitOfWork.EmergencyBloodRepository.Count(baseFilter.And(b => b.Status == EmergencyBloodRequestEnum.Finish));
            var rejected = await unitOfWork.EmergencyBloodRepository.Count(baseFilter.And(b => b.Status == EmergencyBloodRequestEnum.Reject));
            return new
            {
                Total = total,
                Pending = pending,
                Processing = processing,
                Finish = finish,
                Rejected = rejected
            };
        }

    }
}
