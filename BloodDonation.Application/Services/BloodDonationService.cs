using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;

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

            if (bloodDonation.Status != BloodDonationStatusEnum.InProgress)
            {
                return false; //Cannot change status from current status
            }
            if (status == BloodDonationStatusEnum.Donated)
            {
                var bloodComponent = await unitOfWork.BloodComponentRepository.GetByCondition(b => b.Id == BloodComponentType.WholeBlood.Id);

                await unitOfWork.BloodStorageRepository.CreateAsync(new BloodStorage
                {
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
                            <p style='margin-top: 15px;'>
                                🧪 <strong>Kết quả xét nghiệm máu chi tiết</strong> sẽ được gửi đến bạn trong vòng <strong>3 đến 5 ngày tới</strong>.
                            </p>
                            <br>
                            <p style='color: gray;'>— BloodLink - Trung tâm tiếp nhận máu</p>
                        </div>";

                    await SendEmailAsync(user.Email, subject, body);
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
    }
}
