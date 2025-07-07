using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using BloodDonation.Application.Utilities;

namespace BloodDonation.Application.Services
{
    public class BloodDonationService : IBloodDonationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmailSettings emailSettings;

        public BloodDonationService(IUnitOfWork unitOfWork, EmailSettings emailSettings)
        {
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }
        public async Task<object> SearchAsync(string? searchKey, int? pageIndex, int? pageSize, BloodDonationStatusEnum? status)
        {
            // Biểu thức lọc
            Expression<Func<BloodDonation.Domain.Entities.BloodDonation, bool>> filter = b =>
                (!status.HasValue || b.Status == status) &&
                (string.IsNullOrWhiteSpace(searchKey) ||
                 (b.BloodDonationRequest.User.FullName.Contains(searchKey) ||
                  b.BloodDonationRequest.User.PhoneNo.Contains(searchKey) ||
                  b.BloodDonationRequest.User.IdentityId.Contains(searchKey) ||
                  b.BloodDonationRequest.User.Email.Contains(searchKey) ||
                  b.BloodDonationRequest.Code!.Contains(searchKey)));
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

            if (bloodDonation.Status != BloodDonationStatusEnum.InProgress)
            {
                return false; //Cannot change status from current status
            }
            if(status == BloodDonationStatusEnum.Donated)
            {
                // Create BloodStorage record when status is Donated
                // Generate new code for BloodStorage
                var existingStorange = await unitOfWork.BloodStorageRepository.Search(x => x.Code != null && x.Code.StartsWith("BS"));

                int maxNumericCode = 0;
                foreach (var code in existingStorange)
                {
                    var numericPart = code.Code!.Substring(2); // Remove "BS"
                    if (int.TryParse(numericPart, out int num))
                    {
                        maxNumericCode = Math.Max(maxNumericCode, num);
                    }
                }

                var nextCode = $"BS{(maxNumericCode + 1).ToString("D5")}"; // BS00001, BS00002, ...
                var bloodComponent = await unitOfWork.BloodComponentRepository.GetByCondition(b => b.Id == BloodComponentType.WholeBlood.Id);

                await unitOfWork.BloodStorageRepository.CreateAsync(new BloodStorage
                {
                    Code = nextCode, // Assign the generated code
                    Volume = bloodDonation.Volume,
                    BloodDonationId = bloodDonation.Id,
                    ExpiredDate = DateTime.Now.AddDays(bloodComponent.ShelfLifeInDay)
                });
                // Send email notification to user
                var request = await unitOfWork.BloodDonationRequestRepository.GetByCondition(b => b.Id == bloodDonation.BloodDonationRequestId, includeProperties: "User");
                if (request?.User != null)
                {
                    var user = request.User;

                    string subject = "Cảm ơn bạn đã hiến máu!";
                    string body = $@"
                        <div style='font-family: Arial, sans-serif;'>
                            <h2 style='color: #e74c3c;'>❤️ Xin chân thành cảm ơn, {user.FullName}!</h2>
                            <p>Bạn đã hiến thành công <strong>{bloodDonation.Volume}ml</strong> máu vào ngày <strong>{bloodDonation.DonationDate:dd/MM/yyyy}</strong>.</p>
                            <p>Nghĩa cử cao đẹp của bạn sẽ giúp cứu sống nhiều người.</p>

                            <hr style='border: none; border-top: 1px solid #ccc;'>

                            <p>🧪 <strong>Kết quả xét nghiệm máu chi tiết</strong> sẽ được gửi đến bạn trong vòng <strong>3 đến 5 ngày tới</strong>.</p>

                            <p>📅 <strong>Thời gian hiến máu tiếp theo</strong>: Bạn có thể hiến máu lần tiếp theo sau ngày 
                               <strong>{bloodDonation.DonationDate!.Value.AddDays(60):dd/MM/yyyy}</strong>.</p>

                            <p>💡 <strong>Hướng dẫn phục hồi sau hiến máu:</strong></p>
                            <ul>
                                <li>Uống nhiều nước và nghỉ ngơi đầy đủ trong 24 giờ.</li>
                                <li>Ăn các thực phẩm giàu sắt như thịt đỏ, rau xanh, trứng, đậu,...</li>
                                <li>Tránh vận động mạnh trong ngày đầu tiên sau hiến máu.</li>
                                <li>Nếu cảm thấy chóng mặt, hãy ngồi hoặc nằm nghỉ ngay.</li>
                            </ul>

                            <p style='margin-top: 15px;'>
                                ☎️ Nếu bạn có bất kỳ phản ứng bất thường nào hoặc cần tư vấn thêm, vui lòng liên hệ trung tâm hiến máu qua hotline <strong>1900 123 456</strong>.
                            </p>

                            <br>
                            <p style='color: gray;'>— BloodLink - Trung tâm tiếp nhận máu</p>
                        </div>";

                    await SendEmailAsync(user.Email, subject, body);

                    //Update request status to Completed
                    request.Status = BloodDonationRequestStatus.Completed;
                    unitOfWork.BloodDonationRequestRepository.Update(request);
                }
            }

            // Update status
            bloodDonation.Status = status;

            //Save changes
            unitOfWork.BloodDonationRepository.Update(bloodDonation);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
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

        public async Task SendReminderEmailsAsync()
        {
            var targetDate = DateTime.Today.AddDays(-90); // 60 ngày trước

            var donations = await unitOfWork.BloodDonationRepository.Search(
                    b => b.DonationDate!.Value.Date == targetDate && (b.Status == BloodDonationStatusEnum.Donated || b.Status == BloodDonationStatusEnum.Checked),
                    orderBy: q => q.OrderByDescending(x => x.DonationDate),
                    includeProperties: "BloodDonationRequest.User,BloodDonationRequest");

            // Lọc trùng theo UserId (chỉ 1 email / user)
            var distinctDonations = donations
                .Where(d => d.BloodDonationRequest?.User != null)
                .GroupBy(d => d.BloodDonationRequest!.UserId)
                .Select(g => g.First());

            foreach (var donation in distinctDonations)
            {
                var user = donation.BloodDonationRequest?.User!;
                if (string.IsNullOrWhiteSpace(user.Email)) continue;


                string subject = "⏰ Đã đến lúc bạn có thể hiến máu trở lại!";
                string body = $@"
                    <div style='font-family: Arial, sans-serif;'>
                        <h2 style='color: #e74c3c;'>Chào {user.FullName},</h2>
                        <p>Hôm nay là tròn <strong>60 ngày</strong> kể từ lần hiến máu gần nhất của bạn vào ngày <strong>{donation.DonationDate:dd/MM/yyyy}</strong>.</p>
                        <p>Bạn đã đủ điều kiện để tiếp tục hành trình giúp đỡ cộng đồng bằng cách <strong>hiến máu lần nữa</strong>.</p>

                        <p>
                            👉 <a href='https://bloodlink.vn/dang-ky'>Đăng ký hiến máu ngay</a>
                        </p>

                        <p style='margin-top: 20px;'>💖 Cảm ơn bạn vì nghĩa cử cao đẹp!</p>
                        <p style='color: gray;'>— BloodLink - Trung tâm tiếp nhận máu</p>
                    </div>";

                await SendEmailAsync(user.Email, subject, body);
            }
        }

        public async Task<object> GetDonationSummaryAsync(DateRangeFilter range)
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

            Expression<Func<BloodDonation.Domain.Entities.BloodDonation, bool>> baseFilter = b =>
                b.DonationDate >= startDate &&
                b.DonationDate < endDate;

            var total = await unitOfWork.BloodDonationRepository.Count(baseFilter);
            var inProgress = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.InProgress));
            var donated = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.Donated));
            var cancelled = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.Cancelled));
            var checkedCount = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.Checked));

            return new
            {
                Total = total,
                InProgress = inProgress,
                Donated = donated,
                Cancelled = cancelled,
                Checked = checkedCount
            };
        }public async Task<object> GetSupervisorSummaryAsync(DateRangeFilter range)
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

            Expression<Func<BloodDonation.Domain.Entities.BloodDonation, bool>> baseFilter = b =>
                b.DonationDate >= startDate &&
                b.DonationDate < endDate &&
                b.Status != BloodDonationStatusEnum.Cancelled;

            var total = await unitOfWork.BloodDonationRepository.Count(baseFilter);
            var inProgress = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.InProgress));
            var donated = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.Donated));
            var checkedCount = await unitOfWork.BloodDonationRepository.Count(baseFilter.And(b => b.Status == BloodDonationStatusEnum.Checked));

            return new
            {
                Total = total,
                InProgress = inProgress,
                Donated = donated,
                Checked = checkedCount
            };
        }


    }
}
