using BloodDonation.Application.Models.HealthCheckForms;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Entities;
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
            
            var healthCheckFormExists = await unitOfWork.HealthCheckFormRepository.GetByCondition(
                h => h.BloodDonateRequestId == model.BloodDonateRequestId);
            if (healthCheckFormExists != null)
                throw new ArgumentException("Biểu mẫu kiểm tra sức khỏe đã tồn tại cho yêu cầu hiến máu này");

            // Map the model to the entity
            var healthCheckForm = unitOfWork.Mapper.Map<HealthCheckForm>(model);

            // Add the entity to the repository
            await unitOfWork.HealthCheckFormRepository.CreateAsync(healthCheckForm, cancellationToken);

            // Save changes to the database
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var existingForm = await unitOfWork.HealthCheckFormRepository.GetByCondition(
                h => h.BloodDonateRequestId == model.BloodDonateRequestId);
            // Return the created form with request
            return unitOfWork.Mapper.Map<HealthCheckFormViewModel>(existingForm);
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
