using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Application.Utilities;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;


namespace BloodDonation.Application.Services
{
    public class BloodDonationRequestService : IBloodDonationRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmailSettings emailSettings;

        public BloodDonationRequestService(IUnitOfWork unitOfWork, EmailSettings emailSettings)
        {
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }

        public async Task<object> SearchAsync(int? pageIndex = 1, int? pageSize = 10,
                                              BloodDonationRequestStatus? status = null, string? keyword = null, TimeSlotEnum? timeSlot = null, DateOnly? dateRequest = null)
        {
            Expression<Func<BloodDonationRequest, bool>> filter = b =>
                (!status.HasValue || b.Status == status.Value) &&
                (!timeSlot.HasValue || b.TimeSlot == timeSlot.Value) &&
                (!dateRequest.HasValue || b.DonatedDateRequest.Date == dateRequest.Value.ToDateTime(TimeOnly.MinValue).Date) &&
                (b.Status != BloodDonationRequestStatus.Cancelled) &&
                (string.IsNullOrEmpty(keyword) ||
                    (b.User.FullName != null && b.User.FullName.Contains(keyword)) ||
                    (b.User.PhoneNo != null && b.User.PhoneNo.Contains(keyword)) ||
                    (b.User.IdentityId != null && b.User.IdentityId.Contains(keyword)) ||
                    (b.User.Email != null && b.User.Email.Contains(keyword)) ||
                    (b.Code != null && b.Code.Contains(keyword)));

            var pagedData = await unitOfWork.BloodDonationRequestRepository.Search(
                filter: filter,
                includeProperties: "User,HealthCheckForm,BloodDonation",
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

            // Validate the date is not in the past
            if (model.DonatedDateRequest.Date < DateTime.Today)
            {
                throw new ArgumentException("Ngày yêu cầu không được nhỏ hơn hôm nay");
            }

            // Kiểm tra khoảng cách 60 ngày kể từ lần hiến máu gần nhất
            var lastDonation = (await unitOfWork.BloodDonationRepository
                .Search(
                    b => b.BloodDonationRequest.UserId == model.UserId && (b.Status == BloodDonationStatusEnum.Donated || b.Status == BloodDonationStatusEnum.Checked),
                    orderBy: q => q.OrderByDescending(x => x.DonationDate),
                    includeProperties: "BloodDonationRequest"))
                .FirstOrDefault();

            if (lastDonation != null && (DateTime.Today - lastDonation.DonationDate!.Value).TotalDays < 90)
            {
                //comment for testing purpose
                throw new ArgumentException("Không được tạo yêu cầu hiến máu mới trong vòng 90 ngày kể từ lần hiến máu gần nhất.");
            }

            // 2. Tìm mã Code lớn nhất hiện có (định dạng BDR00001)
            var existingRequest = await unitOfWork.BloodDonationRequestRepository.Search(x => x.Code != null && x.Code.StartsWith("BDR"));

            int maxNumericCode = 0;
            foreach (var code in existingRequest)
            {
                var numericPart = code.Code!.Substring(3); // Remove "BDR"
                if (int.TryParse(numericPart, out int num))
                {
                    maxNumericCode = Math.Max(maxNumericCode, num);
                }
            }

            var nextCode = $"BDR{(maxNumericCode + 1).ToString("D5")}"; // BDR00001, BDR00002, ...

            var bloodDonationRequest = unitOfWork.Mapper.Map<Domain.Entities.BloodDonationRequest>(model);
            bloodDonationRequest.Code = nextCode; // Set the new code

            await unitOfWork.BloodDonationRequestRepository.CreateAsync(bloodDonationRequest, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var timeSlotDisplay = ToFriendlyString(model.TimeSlot);

            await SendEmailAsync(user.Email!, "Xác nhận đăng ký hiến máu thành công",
            $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
                <h2 style='color:#e74c3c;'> Xác nhận đăng ký hiến máu</h2>
                <p>Xin chào <strong>{user.FullName}</strong>,</p>

                <p>Chúng tôi xin xác nhận bạn đã đăng ký thành công buổi hiến máu với thông tin như sau:</p>

                <ul style='line-height: 1.6;'>
                    <li><strong>Mã đăng ký:</strong> {nextCode}</li>
                    <li><strong>Ngày hiến máu:</strong> {model.DonatedDateRequest:dd/MM/yyyy}</li>
                    <li><strong>Khung giờ:</strong> {timeSlotDisplay}</li>
                </ul>

                <p>Cảm ơn bạn đã tham gia đóng góp cho cộng đồng. Mỗi giọt máu cho đi là một hy vọng được trao gửi.</p>

                <p style='color:gray; font-size: 0.9em;'>— BloodLink - Trung tâm Tiếp nhận và Hỗ trợ Hiến máu</p>
            </div>");


            var bloodDonationRequestWithUser = await unitOfWork
                .BloodDonationRequestRepository
                .GetByCondition(b => b.Id == bloodDonationRequest.Id, includeProperties: "User,HealthCheckForm,BloodDonation");
            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(bloodDonationRequestWithUser);
        }
        public async Task<BloodDonationRequestViewModel?> UpdatePartialAsync(BloodDonationRequestUpdateModel model, CancellationToken cancellationToken = default)
        {
            var entity = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.Id.ToString() == model.Id, includeProperties: "User,HealthCheckForm,BloodDonation");

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
                .GetByCondition(b => b.Id.ToString() == id, includeProperties: "User,HealthCheckForm,BloodDonation");

            if (entity == null)
                return null;

            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(entity);
        }
        public async Task<BloodDonationRequestViewModel?> UpdateStatusAsync(string id, string? rejectNote, BloodDonationRequestStatus status, CancellationToken cancellationToken = default)
        {
            // Constants for validation
            const double MinWeight = 45.0;
            const double MinHemoglobin = 120;
            const int MinAge = 18;
            const int MaxAge = 60;

            var entity = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.Id.ToString() == id, includeProperties: "User,HealthCheckForm,BloodDonation");

            if (entity == null)
                throw new KeyNotFoundException($"Không tìm thấy yêu cầu hiến máu với ID {id}");

            //add logic for accepted and rejected status
            if (status == BloodDonationRequestStatus.Rejected || status == BloodDonationRequestStatus.Approved)
            {
                if (entity.HealthCheckForm == null)
                    throw new InvalidOperationException("Yêu cầu kiểm tra sức khỏe chưa được tạo.");

            }

            //if reject status, check reject note
            if (status == BloodDonationRequestStatus.Rejected)
            {
                if (rejectNote == null || rejectNote.Length < 2)
                    throw new ArgumentException("Lý do từ chối phải có ít nhất 2 ký tự.");
                entity.ReasonReject = rejectNote;
            }

            //if approved status, check health form
            if (status == BloodDonationRequestStatus.Approved)
            {
                var healthForm = entity.HealthCheckForm!;

                if (healthForm.Weight < MinWeight)
                    throw new ArgumentException($"Cân nặng phải từ {MinWeight} kg trở lên.");
                if (healthForm.Hemoglobin < MinHemoglobin)
                    throw new ArgumentException($"Huyết sắc tố phải từ {MinHemoglobin} g/l trở lên.");
                if (healthForm.Age < MinAge || healthForm.Age > MaxAge)
                    throw new ArgumentException($"Tuổi phải trong khoảng {MinAge}-{MaxAge}.");
                if (healthForm.IsInfectiousDisease)
                    throw new ArgumentException("Không đủ điều kiện do mắc bệnh truyền nhiễm.");
                if (healthForm.IsPregnant)
                    throw new ArgumentException("Phụ nữ mang thai không đủ điều kiện hiến máu.");
                if (healthForm.HasChronicDisease)
                    throw new ArgumentException("Không đủ điều kiện do có bệnh mãn tính.");
                if (healthForm.IsUsedAlcoholRecently)
                    throw new ArgumentException("Không đủ điều kiện do đã sử dụng rượu gần đây.");
                if (healthForm.HasUnsafeSexualBehaviourOrSameSexSexualContact)
                    throw new ArgumentException("Không đủ điều kiện do hành vi tình dục không an toàn.");

                if (healthForm.VolumeBloodDonated >= 350)
                {
                    if (healthForm.Hemoglobin < 125)
                        throw new ArgumentException("Không đủ điều kiện do huyết sắc tố không đạt trên 125 g/l nếu hiến từ 350ml trở lên.");
                }

                //Create a new BloodDonation record if the request is approved
                //Find the next available code for BloodDonation
                var existingBloodDonation = await unitOfWork.BloodDonationRepository.Search(x => x.Code != null && x.Code.StartsWith("BD"));

                int maxNumericCode = 0;
                foreach (var code in existingBloodDonation)
                {
                    var numericPart = code.Code!.Substring(2); // Remove "BD"
                    if (int.TryParse(numericPart, out int num))
                    {
                        maxNumericCode = Math.Max(maxNumericCode, num);
                    }
                }

                var nextCode = $"BD{(maxNumericCode + 1).ToString("D5")}"; // BD00001, BD00002, ...
                await unitOfWork.BloodDonationRepository.CreateAsync(new BloodDonation.Domain.Entities.BloodDonation
                {
                    Code = nextCode,
                    BloodType = entity.BloodType,
                    DonationDate = DateTime.Now,
                    Volume = healthForm.VolumeBloodDonated,
                    Description = "Hiến máu từ yêu cầu hiến máu",
                    BloodDonationRequestId = entity.Id
                }, cancellationToken);
            }

            entity.Status = status;
            unitOfWork.BloodDonationRequestRepository.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Gửi email nếu status là Cancelled
            if (status == BloodDonationRequestStatus.Cancelled)
            {
                var timeSlotDisplay = ToFriendlyString(entity.TimeSlot); // dùng method chuyển TimeSlotEnum về chữ

                await SendEmailAsync(entity.User.Email!, "Huỷ đăng ký hiến máu",
                $@"<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
                    <h2 style='color:#e74c3c;'>Đăng ký hiến máu đã được huỷ</h2>
                    <p>Xin chào <strong>{entity.User.FullName}</strong>,</p>

                    <p>Yêu cầu hiến máu của bạn đã được huỷ với các thông tin sau:</p>

                    <ul style='line-height: 1.6;'>
                        <li><strong>Mã yêu cầu:</strong> {entity.Code}</li>
                        <li><strong>Ngày đã đăng ký:</strong> {entity.DonatedDateRequest:dd/MM/yyyy}</li>
                        <li><strong>Khung giờ:</strong> {timeSlotDisplay}</li>
                    </ul>

                    <p>Nếu đây là sự nhầm lẫn hoặc bạn muốn đặt lại lịch, vui lòng liên hệ với chúng tôi để được hỗ trợ.</p>

                    <p style='color:gray; font-size: 0.9em;'>— BloodLink - Trung tâm Tiếp nhận và Hỗ trợ Hiến máu</p>
                </div>");
            }
            return unitOfWork.Mapper.Map<BloodDonationRequestViewModel>(entity);
        }
        public async Task<List<BloodDonationRequestViewModel>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var requests = await unitOfWork.BloodDonationRequestRepository
                .GetByCondition(b => b.UserId == userId && !b.IsDeleted, includeProperties: "User,HealthCheckForm,BloodDonation");

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
        public async Task CancelExpiredPendingRequestsAsync()
        {
            var today = DateTime.Today;

            var expiredRequests = await unitOfWork.BloodDonationRequestRepository
                .Search(r => r.Status == BloodDonationRequestStatus.Pending && r.DonatedDateRequest.Date < today);
            foreach (var request in expiredRequests)
            {
                request.Status = BloodDonationRequestStatus.Cancelled;
            }
            unitOfWork.BloodDonationRequestRepository.UpdateRange((List<BloodDonationRequest>)expiredRequests);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<object> GetSummaryAsync(DateRangeFilter range)
        {
            DateTime today = DateTime.Today;
            DateTime startDate = today;
            DateTime endDate = today.AddDays(1); // exclusive

            switch (range)
            {
                case DateRangeFilter.Today:
                    // startDate, endDate đã đúng
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
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                    break;
            }

            Expression<Func<BloodDonationRequest, bool>> baseFilter = b =>
                b.DonatedDateRequest >= startDate &&
                b.DonatedDateRequest < endDate &&
                b.Status != BloodDonationRequestStatus.Cancelled;

            var total = await unitOfWork.BloodDonationRequestRepository.Count(baseFilter);
            var pending = await unitOfWork.BloodDonationRequestRepository.Count(baseFilter.And(b => b.Status == BloodDonationRequestStatus.Pending));
            var approved = await unitOfWork.BloodDonationRequestRepository.Count(baseFilter.And(b => b.Status == BloodDonationRequestStatus.Approved));
            var rejected = await unitOfWork.BloodDonationRequestRepository.Count(baseFilter.And(b => b.Status == BloodDonationRequestStatus.Rejected));
            var donated = await unitOfWork.BloodDonationRequestRepository.Count(baseFilter.And(b => b.Status == BloodDonationRequestStatus.Completed));

            return new
            {
                Total = total,
                Pending = pending,
                Approved = approved,
                Rejected = rejected,
                Donated = donated
            };
        }
        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var client = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail, emailSettings.FromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
        }
        public static string ToFriendlyString(TimeSlotEnum slot)
        {
            switch (slot)
            {
                case TimeSlotEnum.Slot7hto7h30: return "07:00 - 07:30";
                case TimeSlotEnum.Slot7h30to8h: return "07:30 - 08:00";
                case TimeSlotEnum.Slot8hto8h30: return "08:00 - 08:30";
                case TimeSlotEnum.Slot8h30to9h: return "08:30 - 09:00";
                case TimeSlotEnum.Slot9hto9h30: return "09:00 - 09:30";
                case TimeSlotEnum.Slot0h30to10h: return "09:30 - 10:00";
                case TimeSlotEnum.Slot10hto10h30: return "10:00 - 10:30";
                case TimeSlotEnum.Slot13h30to14h: return "13:30 - 14:00";
                case TimeSlotEnum.Slot14hto14h30: return "14:00 - 14:30";
                case TimeSlotEnum.Slot14h30to15h: return "14:30 - 15:00";
                case TimeSlotEnum.Slot15hto15h30: return "15:00 - 15:30";
                case TimeSlotEnum.Slot15h30to16h: return "15:30 - 16:00";
                default: return "Không rõ";
            }
        }

    }
}
