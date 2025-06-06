using BloodDonation.Application.Models.HealthCheckForms;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/health-check-form")]
    [ApiController]
    public class HealthCheckFormController : ControllerBase
    {
        private readonly IHealthCheckFormService healthCheckFormService;
        public HealthCheckFormController(IHealthCheckFormService healthCheckFormService)
        {
            this.healthCheckFormService = healthCheckFormService;
        }
        [HttpPost()]
        public async Task<IActionResult> CreateHealthCheckForm([FromBody] HealthCheckFormCreateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var createdForm = await healthCheckFormService.CreateAsync(model);
                return Ok(createdForm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHealthCheckForm(Guid id, [FromBody] HealthCheckFormUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedForm = await healthCheckFormService.UpdateAsync(id, model);
                if (updatedForm == null)
                    return NotFound(new { message = "Biểu mẫu kiểm tra sức khỏe không tồn tại." });
                return Ok(updatedForm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHealthCheckFormById(Guid id)
        {
            try
            {
                var healthCheckForm = await healthCheckFormService.GetByIdAsync(id);
                if (healthCheckForm == null)
                    return NotFound(new { message = "Biểu mẫu kiểm tra sức khỏe không tồn tại." });
                return Ok(healthCheckForm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
    }
}
