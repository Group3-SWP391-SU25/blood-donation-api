using BloodDonation.Application.Models.HealthCheckForms;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
using BloodDonation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonation.Application.Services
{
    public class HealthCheckFormService : IHealthCheckFormService
    {
        private readonly IUnitOfWork unitOfWork;
        public HealthCheckFormService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<object> CreateAsync(HealthCheckFormCreateModel model, CancellationToken cancellationToken = default)
        {
            // Validate the model
            var request = await unitOfWork.BloodDonationRequestRepository.GetByCondition(b => b.Id == model.BloodDonateRequestId);
            if (request == null)
                throw new ArgumentException("Yêu cầu hiến máu không tồn tại");

            // Check date request and current date

            // Comment for test
            //if (request.DonatedDateRequest.Date != DateTime.Today)
            //    throw new ArgumentException("Không thể tạo kiểm tra sức khỏe cho yêu cầu hiến máu không phải ngày hôm nay.");


            var healthCheckFormExists = await unitOfWork.HealthCheckFormRepository.GetByCondition(
                h => h.BloodDonateRequestId == model.BloodDonateRequestId);
            if (healthCheckFormExists != null)
                throw new ArgumentException("Biểu mẫu kiểm tra sức khỏe đã tồn tại cho yêu cầu hiến máu này");

            // Map the model to the entity
            var healthCheckForm = unitOfWork.Mapper.Map<HealthCheckForm>(model);

            // Add the entity to the repository
            await unitOfWork.HealthCheckFormRepository.CreateAsync(healthCheckForm, cancellationToken);

            if(model.IsApproved == true)
            {
                await ApproveBloodDonationAsync(request, model, cancellationToken);
            }
            else
            {
                //If not approved, set the request status to rejected
                request.Status = BloodDonationRequestStatus.Rejected;
                request.ReasonReject = model.ReasonForRejection ?? "Không đủ điều kiện hiến máu";
                unitOfWork.BloodDonationRequestRepository.Update(request);
            }

            // Save changes to the database
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var existingForm = await unitOfWork.HealthCheckFormRepository.GetByCondition(
                h => h.BloodDonateRequestId == model.BloodDonateRequestId);
            // Return the created form with request
            return unitOfWork.Mapper.Map<HealthCheckFormViewModel>(existingForm);
        }

        private async Task ApproveBloodDonationAsync(BloodDonation.Domain.Entities.BloodDonationRequest request, HealthCheckFormCreateModel model, CancellationToken cancellationToken)
        {
            const double MinWeight = 45.0;
            const double MinHemoglobin = 120;
            const int MinAge = 18;
            const int MaxAge = 60;

            if (model.Weight < MinWeight)
                throw new ArgumentException($"Cân nặng phải từ {MinWeight} kg trở lên mới được phép hiến máu.");
            if (model.Hemoglobin < MinHemoglobin)
                throw new ArgumentException($"Huyết sắc tố phải từ {MinHemoglobin} g/l trở lên mới được phép hiến máu.");
            if (model.Age < MinAge || model.Age > MaxAge)
                throw new ArgumentException($"Tuổi phải trong khoảng {MinAge}-{MaxAge} mới được phép hiến máu.");
            if (model.IsInfectiousDisease)
                throw new ArgumentException("Không đủ điều kiện hiến máu do mắc bệnh truyền nhiễm.");
            if (model.IsPregnant)
                throw new ArgumentException("Phụ nữ mang thai không đủ điều kiện hiến máu.");
            if (model.HasChronicDisease)
                throw new ArgumentException("Không đủ điều kiện hiến máu do có bệnh mãn tính.");
            if (model.IsUsedAlcoholRecently)
                throw new ArgumentException("Không đủ điều kiện hiến máu do đã sử dụng rượu gần đây.");
            if (model.HasUnsafeSexualBehaviourOrSameSexSexualContact)
                throw new ArgumentException("Không đủ điều kiện hiến máu do hành vi tình dục không an toàn.");
            if (model.VolumeBloodDonated >= 350 && model.Hemoglobin < 125)
                throw new ArgumentException("Không đủ điều kiện hiến máu do huyết sắc tố không đạt trên 125 g/l nếu hiến từ 350ml trở lên.");

            // Đủ điều kiện => tạo bản ghi hiến máu
            request.Status = BloodDonationRequestStatus.Approved;

            var existingDonations = await unitOfWork.BloodDonationRepository
                .Search(x => x.Code != null && x.Code.StartsWith("BD"));

            int maxNumericCode = 0;
            foreach (var code in existingDonations)
            {
                var numericPart = code.Code!.Substring(2);
                if (int.TryParse(numericPart, out int num))
                    maxNumericCode = Math.Max(maxNumericCode, num);
            }

            var nextCode = $"BD{(maxNumericCode + 1).ToString("D5")}";

            await unitOfWork.BloodDonationRepository.CreateAsync(new BloodDonation.Domain.Entities.BloodDonation
            {
                Code = nextCode,
                BloodType = request.BloodType,
                DonationDate = DateTime.Now,
                Volume = model.VolumeBloodDonated,
                Description = "Hiến máu từ yêu cầu hiến máu",
                BloodDonationRequestId = request.Id
            }, cancellationToken);
        }

        public async Task<object> UpdateAsync(Guid id, HealthCheckFormUpdateModel model, CancellationToken cancellationToken = default)
        {
            // Validate the model
            var healthCheckForm = await unitOfWork.HealthCheckFormRepository.GetByCondition(h => h.Id == id);
            if (healthCheckForm == null)
                throw new ArgumentException("Biểu mẫu kiểm tra sức khỏe không tồn tại");

            // Map the model to the entity
            unitOfWork.Mapper.Map(model, healthCheckForm);

            // Update the entity in the repository
            unitOfWork.HealthCheckFormRepository.Update(healthCheckForm);

            // Save changes to the database
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return unitOfWork.Mapper.Map<HealthCheckFormViewModel>(healthCheckForm);
        }
        public async Task<HealthCheckFormViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Retrieve the health check form by ID
            var healthCheckForm = await unitOfWork.HealthCheckFormRepository.GetByCondition(
                h => h.Id == id);

            // If not found, return null
            if (healthCheckForm == null)
                return null;

            // Map the entity to the view model
            return unitOfWork.Mapper.Map<HealthCheckFormViewModel>(healthCheckForm);
        }
    }
}
