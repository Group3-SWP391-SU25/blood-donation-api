using BloodDonation.Application.Models.BloodChecks;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-checks")]
    [ApiController]
    public class BloodCheckController : ControllerBase
    {
        private readonly IBloodCheckService bloodCheckService;

        public BloodCheckController(IBloodCheckService bloodCheckService)
        {
            this.bloodCheckService = bloodCheckService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await bloodCheckService.GetByIdAsync(id, cancellationToken);
                if (result == null)
                {
                    return NotFound(new { message = "Kết quả xét nghiệm không tồn tại." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }

        [HttpGet("by-donation/{bloodDonationId}")]
        public async Task<IActionResult> GetByBloodDonationId(Guid bloodDonationId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await bloodCheckService.GetByBloodDonationIdAsync(bloodDonationId, cancellationToken);
                if (result == null)
                {
                    return NotFound(new { message = "Không tìm thấy kết quả xét nghiệm cho lần hiến máu này." });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BloodCheckCreateModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await bloodCheckService.CreateAsync(model, cancellationToken);
                return Ok(new
                {
                    message = "Tạo kết quả xét nghiệm thành công!",
                    data = result,
                    isSafe = result.IsSafe,
                    validationErrors = result.ValidationErrors
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }
    }
}
