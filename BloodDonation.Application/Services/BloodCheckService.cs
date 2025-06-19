using BloodDonation.Application.Models.BloodChecks;
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
using System.Net.Mail;
using System.Net;

namespace BloodDonation.Application.Services
{
    public class BloodCheckService : IBloodCheckService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly EmailSettings emailSettings;

        public BloodCheckService(IUnitOfWork unitOfWork, EmailSettings emailSettings)
        {
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }

        public async Task<BloodCheckViewModel> CreateAsync(BloodCheckCreateModel model, CancellationToken cancellationToken = default)
        {
            // Validate blood donation exists and is in correct status
            var bloodDonation = await unitOfWork.BloodDonationRepository.GetByCondition(
                bd => bd.Id == model.BloodDonationId && bd.Status == BloodDonationStatusEnum.Donated,
                includeProperties: "BloodDonationRequest,BloodDonationRequest.User");

            if (bloodDonation == null)
                throw new ArgumentException("Hiến máu không tồn tại hoặc chưa hoàn thành.");

            // Check if blood check already exists for this donation
            var existingCheck = await unitOfWork.BloodCheckRepository.GetByCondition(
                bc => bc.BloodDonationId == model.BloodDonationId);

            if (existingCheck != null)
                throw new ArgumentException("Kết quả xét nghiệm đã tồn tại cho lần hiến máu này.");

            // Validate blood group exists
            var bloodGroup = await unitOfWork.BloodGroupRepository.GetByCondition(bg => bg.Id == model.BloodGroupId);
            if (bloodGroup == null)
                throw new ArgumentException("Nhóm máu không tồn tại.");

            // Validate blood indexes
            var validationErrors = BloodCheckUtils.ValidateAll(
                model.WBC, model.RBC, model.HGB, model.HCT,
                model.MCV, model.MCH, model.MCHC, model.PLT, model.MPV);

            bool isSafe = validationErrors.Count == 0;

            // Create blood check entity
            var bloodCheck = new BloodCheck
            {
                WBC = model.WBC,
                RBC = model.RBC,
                HGB = model.HGB,
                HCT = model.HCT,
                MCV = model.MCV,
                MCH = model.MCH,
                MCHC = model.MCHC,
                PLT = model.PLT,
                MPV = model.MPV,
                Description = model.Description,
                CheckedDate = DateTime.UtcNow,
                BloodGroupId = model.BloodGroupId,
                BloodDonationId = model.BloodDonationId
            };

            await unitOfWork.BloodCheckRepository.CreateAsync(bloodCheck, cancellationToken);

            // Update blood donation status to Checked
            bloodDonation.Status = BloodDonationStatusEnum.Checked;
            unitOfWork.BloodDonationRepository.Update(bloodDonation);

            // Update blood storage status based on validation results
            var bloodStorage = await unitOfWork.BloodStorageRepository.Search(
                bs => bs.BloodDonationId == model.BloodDonationId);

            foreach (var bloodStorageUpdate in bloodStorage)
            {
                bloodStorageUpdate.Status = isSafe ? BloodStorageStatusEnum.Safe : BloodStorageStatusEnum.Dangerous;
                bloodStorageUpdate.BloodGroupId = model.BloodGroupId; // Update blood group
                unitOfWork.BloodStorageRepository.Update(bloodStorageUpdate);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Send email notification with blood test results
            if (bloodDonation.BloodDonationRequest?.User != null)
            {
                await SendBloodTestResultEmail(bloodDonation.BloodDonationRequest.User, bloodCheck, bloodGroup, isSafe, validationErrors);
            }

            // Get the created blood check with related data
            var createdBloodCheck = await unitOfWork.BloodCheckRepository.GetByCondition(
                bc => bc.Id == bloodCheck.Id,
                includeProperties: "BloodGroup,BloodDonation,BloodDonation.BloodDonationRequest,BloodDonation.BloodDonationRequest.User");

            var result = unitOfWork.Mapper.Map<BloodCheckViewModel>(createdBloodCheck);
            result.IsSafe = isSafe;
            result.ValidationErrors = validationErrors;

            return result;
        }

        private async Task SendBloodTestResultEmail(User user, BloodCheck bloodCheck, BloodGroup bloodGroup, bool isSafe, List<string> validationErrors)
        {
            string subject = "🩸 Kết quả xét nghiệm máu - BloodLink";
            string statusIcon = isSafe ? "✅" : "⚠️";
            string statusText = isSafe ? "BÌNH THƯỜNG" : "CẦN THEO DÕI";
            string statusColor = isSafe ? "#28a745" : "#dc3545";

            string body = $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset='utf-8'>
            <style>
                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                .container {{ max-width: 800px; margin: 0 auto; padding: 20px; }}
                .header {{ background: linear-gradient(135deg, #e74c3c, #c0392b); color: white; padding: 20px; border-radius: 10px 10px 0 0; text-align: center; }}
                .content {{ background: #f8f9fa; padding: 20px; }}
                .result-table {{ width: 100%; border-collapse: collapse; margin: 20px 0; background: white; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
                .result-table th {{ background: #343a40; color: white; padding: 12px; text-align: left; }}
                .result-table td {{ padding: 12px; border-bottom: 1px solid #dee2e6; }}
                .normal {{ color: #28a745; font-weight: bold; }}
                .abnormal {{ color: #dc3545; font-weight: bold; }}
                .status-badge {{ display: inline-block; padding: 8px 16px; border-radius: 20px; color: white; font-weight: bold; margin: 10px 0; }}
                .footer {{ background: #343a40; color: white; padding: 15px; text-align: center; border-radius: 0 0 10px 10px; }}
                .warning-box {{ background: #fff3cd; border: 1px solid #ffeaa7; padding: 15px; border-radius: 5px; margin: 15px 0; }}
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>🩸 KẾT QUẢ XÉT NGHIỆM MÁU</h1>
                    <p>Xin chào <strong>{user.FullName}</strong></p>
                </div>
                
                <div class='content'>
                    <div style='text-align: center; margin: 20px 0;'>
                        <div class='status-badge' style='background-color: {statusColor};'>
                            {statusIcon} TÌNH TRẠNG: {statusText}
                        </div>
                    </div>
                    
                    <p><strong>📅 Ngày xét nghiệm:</strong> {bloodCheck.CheckedDate:dd/MM/yyyy HH:mm}</p>
                    <p><strong>🩸 Nhóm máu:</strong> {bloodGroup.Type}{bloodGroup.RhFactor}</p>
                    
                    <h3>📊 CHI TIẾT KẾT QUẢ XÉT NGHIỆM</h3>
                    <table class='result-table'>
                        <thead>
                            <tr>
                                <th>Chỉ số</th>
                                <th>Kết quả</th>
                                <th>Đơn vị</th>
                                <th>Giá trị bình thường</th>
                                <th>Tình trạng</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td><strong>WBC</strong><br><small>Bạch cầu</small></td>
                                <td>{bloodCheck.WBC:F1}</td>
                                <td>10³/μL</td>
                                <td>4.0 - 10.0</td>
                                <td class='{(bloodCheck.WBC >= 4.0 && bloodCheck.WBC <= 10.0 ? "normal" : "abnormal")}'>{(bloodCheck.WBC >= 4.0 && bloodCheck.WBC <= 10.0 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>RBC</strong><br><small>Hồng cầu</small></td>
                                <td>{bloodCheck.RBC:F1}</td>
                                <td>10⁶/μL</td>
                                <td>4.2 - 6.1</td>
                                <td class='{(bloodCheck.RBC >= 4.2 && bloodCheck.RBC <= 6.1 ? "normal" : "abnormal")}'>{(bloodCheck.RBC >= 4.2 && bloodCheck.RBC <= 6.1 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>HGB</strong><br><small>Hemoglobin</small></td>
                                <td>{bloodCheck.HGB:F1}</td>
                                <td>g/dL</td>
                                <td>12.5 - 17.5</td>
                                <td class='{(bloodCheck.HGB >= 12.5 && bloodCheck.HGB <= 17.5 ? "normal" : "abnormal")}'>{(bloodCheck.HGB >= 12.5 && bloodCheck.HGB <= 17.5 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>HCT</strong><br><small>Hematocrit</small></td>
                                <td>{bloodCheck.HCT:F1}</td>
                                <td>%</td>
                                <td>36 - 52</td>
                                <td class='{(bloodCheck.HCT >= 36 && bloodCheck.HCT <= 52 ? "normal" : "abnormal")}'>{(bloodCheck.HCT >= 36 && bloodCheck.HCT <= 52 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>MCV</strong><br><small>Thể tích hồng cầu trung bình</small></td>
                                <td>{bloodCheck.MCV:F1}</td>
                                <td>fL</td>
                                <td>80 - 100</td>
                                <td class='{(bloodCheck.MCV >= 80 && bloodCheck.MCV <= 100 ? "normal" : "abnormal")}'>{(bloodCheck.MCV >= 80 && bloodCheck.MCV <= 100 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>MCH</strong><br><small>Lượng Hb trung bình/hồng cầu</small></td>
                                <td>{bloodCheck.MCH:F1}</td>
                                <td>pg</td>
                                <td>27 - 33</td>
                                <td class='{(bloodCheck.MCH >= 27 && bloodCheck.MCH <= 33 ? "normal" : "abnormal")}'>{(bloodCheck.MCH >= 27 && bloodCheck.MCH <= 33 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>MCHC</strong><br><small>Nồng độ Hb trung bình</small></td>
                                <td>{bloodCheck.MCHC:F1}</td>
                                <td>g/dL</td>
                                <td>32 - 36</td>
                                <td class='{(bloodCheck.MCHC >= 32 && bloodCheck.MCHC <= 36 ? "normal" : "abnormal")}'>{(bloodCheck.MCHC >= 32 && bloodCheck.MCHC <= 36 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>PLT</strong><br><small>Tiểu cầu</small></td>
                                <td>{bloodCheck.PLT:F0}</td>
                                <td>10³/μL</td>
                                <td>150 - 450</td>
                                <td class='{(bloodCheck.PLT >= 150 && bloodCheck.PLT <= 450 ? "normal" : "abnormal")}'>{(bloodCheck.PLT >= 150 && bloodCheck.PLT <= 450 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                            <tr>
                                <td><strong>MPV</strong><br><small>Thể tích tiểu cầu trung bình</small></td>
                                <td>{bloodCheck.MPV:F1}</td>
                                <td>fL</td>
                                <td>7.5 - 11.5</td>
                                <td class='{(bloodCheck.MPV >= 7.5 && bloodCheck.MPV <= 11.5 ? "normal" : "abnormal")}'>{(bloodCheck.MPV >= 7.5 && bloodCheck.MPV <= 11.5 ? "✅ Bình thường" : "⚠️ Bất thường")}</td>
                            </tr>
                        </tbody>
                    </table>";

            if (!isSafe && validationErrors.Count > 0)
            {
                body += $@"
                    <div class='warning-box'>
                        <h4>⚠️ CÁC CHỈ SỐ CẦN LƯU Ý:</h4>
                        <ul>";

                foreach (var error in validationErrors)
                {
                    body += $"<li>{error}</li>";
                }

                body += @"
                        </ul>
                        <p><strong>Khuyến nghị:</strong> Bạn nên tham khảo ý kiến bác sĩ để được tư vấn chi tiết về các chỉ số bất thường.</p>
                    </div>";
            }
            else
            {
                body += $@"
                    <div style='background: #d4edda; border: 1px solid #c3e6cb; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                        <h4>✅ CHÚC MỪNG!</h4>
                        <p>Tất cả các chỉ số xét nghiệm máu của bạn đều trong giới hạn bình thường. Cảm ơn bạn đã hiến máu và góp phần cứu sống nhiều người!</p>
                    </div>";
            }

            if (!string.IsNullOrEmpty(bloodCheck.Description))
            {
                body += $@"
                    <div style='background: #e9ecef; padding: 15px; border-radius: 5px; margin: 15px 0;'>
                        <h4>📝 GHI CHÚ TỪ BÁC SĨ:</h4>
                        <p>{bloodCheck.Description}</p>
                    </div>";
            }

            body += $@"
                    <div style='margin-top: 30px; padding: 20px; background: #f8f9fa; border-radius: 5px;'>
                        <h4>📞 LIÊN HỆ HỖ TRỢ</h4>
                        <p>Nếu bạn có bất kỳ câu hỏi nào về kết quả xét nghiệm, vui lòng liên hệ:</p>
                        <ul>
                            <li>📧 Email: support@bloodlink.vn</li>
                            <li>📱 Hotline: 1900-1234</li>
                            <li>🏥 Địa chỉ: Trung tâm Hiến máu BloodLink</li>
                        </ul>
                    </div>
                </div>
                
                <div class='footer'>
                    <p>❤️ Cảm ơn bạn đã tin tưởng và sử dụng dịch vụ của BloodLink</p>
                    <p><small>Email này được gửi tự động, vui lòng không trả lời trực tiếp.</small></p>
                </div>
            </div>
        </body>
        </html>";

            await SendEmailAsync(user.Email, subject, body);
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

        public async Task<BloodCheckViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var bloodCheck = await unitOfWork.BloodCheckRepository.GetByCondition(
                bc => bc.Id == id,
                includeProperties: "BloodGroup,BloodDonation,BloodDonation.BloodDonationRequest,BloodDonation.BloodDonationRequest.User");

            if (bloodCheck == null)
                return null;

            var result = unitOfWork.Mapper.Map<BloodCheckViewModel>(bloodCheck);

            // Calculate if safe based on validation
            var validationErrors = BloodCheckUtils.ValidateAll(
                bloodCheck.WBC, bloodCheck.RBC, bloodCheck.HGB, bloodCheck.HCT,
                bloodCheck.MCV, bloodCheck.MCH, bloodCheck.MCHC, bloodCheck.PLT, bloodCheck.MPV);

            result.IsSafe = validationErrors.Count == 0;
            result.ValidationErrors = validationErrors;

            return result;
        }

        public async Task<BloodCheckViewModel?> GetByBloodDonationIdAsync(Guid bloodDonationId, CancellationToken cancellationToken = default)
        {
            var bloodCheck = await unitOfWork.BloodCheckRepository.GetByCondition(
                bc => bc.BloodDonationId == bloodDonationId,
                includeProperties: "BloodGroup,BloodDonation,BloodDonation.BloodDonationRequest,BloodDonation.BloodDonationRequest.User");

            if (bloodCheck == null)
                return null;

            var result = unitOfWork.Mapper.Map<BloodCheckViewModel>(bloodCheck);

            // Calculate if safe based on validation
            var validationErrors = BloodCheckUtils.ValidateAll(
                bloodCheck.WBC, bloodCheck.RBC, bloodCheck.HGB, bloodCheck.HCT,
                bloodCheck.MCV, bloodCheck.MCH, bloodCheck.MCHC, bloodCheck.PLT, bloodCheck.MPV);

            result.IsSafe = validationErrors.Count == 0;
            result.ValidationErrors = validationErrors;

            return result;
        }
    }
}
