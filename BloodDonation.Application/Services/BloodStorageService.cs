using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using Hangfire;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;

namespace BloodDonation.Application.Services
{
    public class BloodStorageService : IBloodStorageService
    {
        private readonly IUnitOfWork unitOfWork;
        private IBackgroundJobClient backgroundJobClient;
        private readonly EmailSettings emailSettings;
        public BloodStorageService(IUnitOfWork unitOfWork,
            IBackgroundJobClient backgroundJobClient,
            EmailSettings emailSettings)
        {
            this.backgroundJobClient = backgroundJobClient;
            this.unitOfWork = unitOfWork;
            this.emailSettings = emailSettings;
        }
        public async Task NotiExpiredAlertBackground(Guid bloodStorageId)
        {
            var roleNurse = await unitOfWork.RoleRepository.FirstOrDefaultAsync(x => x.Name == "NURSE")
                ?? throw new InvalidOperationException();
            var allNurse = (await unitOfWork.UserRepository.WhereAsync(x => x.RoleId == roleNurse.Id && x.Status == UserStatusEnum.Active.ToString()))?
                .Select(x => x.Email);
            var item = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == bloodStorageId) ??
              throw new InvalidOperationException("Không tìm thấy máu trong kho " + bloodStorageId);
            if (item.Status == BloodStorageStatusEnum.Expired && item.Status == BloodStorageStatusEnum.Exported)
            {
                return;
            }
            string expiredAlert = $@"
                <div style='font-family: Arial, sans-serif; margin-top: 30px; padding: 15px; background-color: #fff3cd; border: 1px solid #ffeeba; border-radius: 5px;'>
                    <h3 style='color: #856404;'>⚠️ Cảnh báo: Máu sắp hết hạn</h3>
                    <p>Mã túi máu: <strong>{item.Id}</strong></p>
                    <p>Ngày hết hạn: <strong>{item.ExpiredDate:dd/MM/yyyy}</strong></p>
                    <p style='margin-top: 10px;'>Vui lòng kiểm tra và xử lý kịp thời.</p>
                </div>";
            using var client = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
            {
                Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings.FromEmail, emailSettings.FromName),
                Subject = "Cảnh báo máu hết hạn",
                Body = expiredAlert,
                IsBodyHtml = true
            };

            if (allNurse?.Count() == 0) return;
            foreach (var nurse in allNurse)
            {
                mailMessage.To.Add(nurse);
                await client.SendMailAsync(mailMessage);
            }

        }
        public async Task CheckExpired(Guid bloodStorageId)
        {
            var item = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == bloodStorageId) ??
                throw new InvalidOperationException("Không tìm thấy máu trong kho " + bloodStorageId);
            if (item.Status != BloodStorageStatusEnum.Exported && item.Status != BloodStorageStatusEnum.Expired)
            {
                if (item.ExpiredDate >= DateTime.Now)
                {
                    item.Status = BloodStorageStatusEnum.Expired;
                    unitOfWork.BloodStorageRepository.Update(item);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }
        public async Task<BloodStorageViewModel> CreateAsync(BloodStorageCreateModel createModel,
            CancellationToken cancellationToken = default)
        {
            var bloodDonation = await unitOfWork.BloodDonationRepository.FirstOrDefaultAsync(x => x.Id == createModel.BloodDonationId,
                cancellationToken: cancellationToken,
                includes: [x => x.BloodCheck!, x => x.BloodDonationRequest, x => x.BloodDonationRequest.User]) ?? throw new InvalidOperationException("Not found Blood Donation");

            var bloodComponent = await unitOfWork.BloodComponentRepository.FirstOrDefaultAsync(x => x.Id == createModel.BloodComponentId);
            var bloodStorage = new BloodStorage()
            {
                BloodComponentId = createModel.BloodComponentId,
                BloodGroupId = bloodDonation.BloodDonationRequest.User.BloodGroupId,
                Status = BloodStorageStatusEnum.UnChecked,
                Volume = bloodDonation.Volume,
                BloodDonationId = bloodDonation.Id,
                ExpiredDate = DateTime.Now.AddDays(bloodComponent?.ShelfLifeInDay ?? 0)
            };
            await unitOfWork.BloodStorageRepository.CreateAsync(bloodStorage);
            await unitOfWork.SaveChangesAsync();
            backgroundJobClient.Schedule(() => CheckExpired(bloodStorage.Id), bloodStorage.ExpiredDate);
            backgroundJobClient.Schedule(() => NotiExpiredAlertBackground(bloodStorage.Id), bloodStorage.ExpiredDate.AddDays(-2));
            return unitOfWork.Mapper.Map<BloodStorageViewModel>(bloodStorage);

        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var item = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: x => x.BloodIssue!);
            if (item != null)
            {
                if (item.BloodIssue != null)
                {
                    throw new InvalidOperationException("Already Had Blood Issue, should not delete");
                }
                item.IsDeleted = true;
                await unitOfWork.SaveChangesAsync();
                return true;

            }
            else throw new InvalidOperationException("Not found Blood Storage");
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

        public async Task<BloodStorageViewModel> UpdateAsync(Guid id, BloodStorageUpdateModel updateModel, CancellationToken cancellationToken = default)
        {
            var bloodStorage = await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken,
                includes: [x => x.BloodDonate, x => x.BloodComponent, x => x.BloodIssue!, x => x.BloodGroup!]);
            if (bloodStorage is not null)
            {
                unitOfWork.Mapper.Map(updateModel, bloodStorage);
                unitOfWork.BloodStorageRepository.Update(bloodStorage);
                if (await unitOfWork.SaveChangesAsync())
                {
                    return unitOfWork.Mapper.Map<BloodStorageViewModel>(await unitOfWork.BloodStorageRepository.FirstOrDefaultAsync(x => x.Id == id,
                        includes: [x => x.BloodGroup!, x => x.BloodComponent, x => x.BloodIssue!]));
                }
                else throw new InvalidOperationException("Save Changes Failed");

            }
            else throw new InvalidOperationException("Not found Blood Storage with Id " + id);

        }
    }
}
