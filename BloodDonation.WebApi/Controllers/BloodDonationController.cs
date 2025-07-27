using BloodDonation.Application.Services;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-donations")]
    [ApiController]
    public class BloodDonationController : ControllerBase
    {
        private readonly IBloodDonationService bloodDonationService;
        private readonly IClaimsService ClaimsService;
        public BloodDonationController(IBloodDonationService bloodDonationService, IClaimsService claimsService)
        {
            this.bloodDonationService = bloodDonationService;
            ClaimsService = claimsService;
        }
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? search = "",
            [FromQuery] int? pageIndex = 1,
            [FromQuery] int? pageSize = 10,
            [FromQuery] BloodDonationStatusEnum? status = null,
            CancellationToken cancellationToken = default)
        {
            var result = await bloodDonationService.SearchAsync(search, pageIndex, pageSize, status);
            return Ok(result);
        }
        //Update Blood Donation Status
        [Authorize(Roles = "NURSE")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] Guid id,
            [FromQuery] BloodDonationStatusEnum status,
            CancellationToken cancellationToken = default)
        {
            var result = await bloodDonationService.UpdateStatusAsync(id, status, cancellationToken);
            if (result)
            {
                return Ok(new { message = "Cập nhật trạng thái hiến máu thành công." });
            }
            return NotFound();
        }
        [Authorize]
        [HttpGet("summary")]
        public async Task<IActionResult> GetDonationSummary([FromQuery] DateRangeFilter range)
        {
            try
            {
                var summary = await bloodDonationService.GetDonationSummaryAsync(range);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("supvisor-summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateRangeFilter range)
        {
            try
            {
                var summary = await bloodDonationService.GetSupervisorSummaryAsync(range);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
    }
}
