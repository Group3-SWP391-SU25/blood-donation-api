using BloodDonation.Application.Models.BloodDonations;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BloodDonation.Domain.Enums;

namespace BloodDonation.Application.Services
{
    public class NurseService : INurseService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmailSettings emailSettings;

        public NurseService(IUnitOfWork unitOfWork, EmailSettings emailSettings)
        {
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }
        public async Task<object> SearchMemberAsync(
            string? searchKey,
            int pageIndex,
            int pageSize,
            Guid? bloodGroupId,
            string? address)
        {
            Expression<Func<User, bool>> filter = u =>
                (string.IsNullOrEmpty(searchKey) || u.FullName.Contains(searchKey) || u.Email.Contains(searchKey)) &&
                (!bloodGroupId.HasValue || u.BloodGroupId == bloodGroupId.Value) &&
                (string.IsNullOrEmpty(address) || u.Addresss.Contains(address)) &&
                !u.IsDeleted && u.RoleId == Guid.Parse("859a4997-1ffa-4915-b50e-9a99e4147b85");

            var users = await unitOfWork.UserRepository.Search(
                filter: filter,
                includeProperties: "BloodGroup,Role,BloodRequests,BloodRequests.BloodDonation",
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            // Tổng số bản ghi
            int totalRecords = await unitOfWork.UserRepository.Count(filter);

            // Tính toán phân trang
            int actualPageSize = pageSize;
            int actualPageIndex = pageIndex;
            int totalPages = (int)Math.Ceiling((double)totalRecords / actualPageSize);

            // Ánh xạ sang ViewModel
            var mappedData = unitOfWork.Mapper.Map<IEnumerable<MemberViewModel>>(users);

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

        // <summary>
        /// <summary>
        /// Sends an email to users who have not donated blood in the last 90 days, calling for blood donation.
        /// Except users who have donated blood in the last 90 days or have no email address or have not donated blood before.
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task SendCallForDonationEmailAsync(List<Guid> userIds)
        {
            var users = await unitOfWork.UserRepository.Search(u =>
                userIds.Contains(u.Id) && !u.IsDeleted && !string.IsNullOrEmpty(u.Email), includeProperties: "BloodRequests,BloodRequests.BloodDonation", pageIndex: 1, pageSize: 999);

            //Check time donation
            var lastDonationThreshold = DateTime.Now.AddDays(-90); //90 days ago

            foreach (var user in users)
            {
                //Get last donation date
                var lastDonationDate = user.BloodRequests?
                    .SelectMany(r => r.BloodDonation != null ? new[] { r.BloodDonation } : [])
                    .Where(d => d.Status == BloodDonationStatusEnum.Donated || d.Status == BloodDonationStatusEnum.Checked)
                    .OrderByDescending(d => d.DonationDate)
                    .FirstOrDefault()?.DonationDate;

                // if last donation date is null or older than threshold, skip sending email
                if (lastDonationDate == null || lastDonationDate >= lastDonationThreshold)
                    continue;
                var htmlBody = $@"
            <div style='font-family: Arial, sans-serif; padding: 20px;'>
                <h2 style='color: #e74c3c;'>🩸 LỜI KÊU GỌI HIẾN MÁU</h2>
                <p>Xin chào <strong>{user.FullName}</strong>,</p>
                <p>Hiện nay, ngân hàng máu của chúng tôi đang trong tình trạng khan hiếm.</p>
                <p>Chúng tôi rất mong nhận được sự giúp đỡ quý báu từ bạn - người từng tham gia hiến máu và góp phần cứu sống nhiều người.</p>
                <p>❤️ <strong>Hãy cùng chúng tôi tiếp tục lan tỏa sự sống.</strong></p>
                <hr style='margin: 20px 0;' />
                <p><strong>📧 Email:</strong> support@bloodlink.vn</p>
                <p><strong>📞 Hotline:</strong> 1900-1234</p>
                <p style='margin-top: 30px;'>Trân trọng,<br><em>Đội ngũ BloodLink</em></p>
            </div>";

                await SendEmailAsync(user.Email, "🩸 Lời kêu gọi hiến máu từ BloodLink", htmlBody);
            }
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
