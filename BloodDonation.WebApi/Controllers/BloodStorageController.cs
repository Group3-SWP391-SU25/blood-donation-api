using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using BloodDonation.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-storage")]
    [ApiController]
    public class BloodStorageController : ControllerBase
    {
        private readonly IBloodStorageService bloodStorageService;
        private readonly IClaimsService ClaimsService;
        public BloodStorageController(IBloodStorageService bloodStorageService, IClaimsService claimsService)
        {
            this.bloodStorageService = bloodStorageService;
            ClaimsService = claimsService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? search = "",
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] BloodStorageStatusEnum? status = null,
            [FromQuery] Guid? bloodGroupId = null,
            [FromQuery] Guid? componentId = null,
            CancellationToken cancellationToken = default)
        {
            var result = await bloodStorageService.SearchAsync(bloodGroupId, componentId, pageIndex, pageSize, search, status, cancellationToken);
            return Ok(result);
        }
        [HttpGet("available-bloods")]
        public async Task<IActionResult> GetAvailableBloods(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] BloodStorageStatusEnum? status = null,
            [FromQuery] Guid? BloodComponentId = null,
            [FromQuery] Guid? BloodGroupId = null,
            [FromQuery] int? volume = null,
            CancellationToken cancellationToken = default)
        {
            var result = await bloodStorageService.GetAvailableBloods(
                pageIndex,
                pageSize,
                status,
                BloodComponentId,
                BloodGroupId,
                volume,
                cancellationToken);
            return Ok(result);
        }
        [HttpPost("blood-preparation/{id}")]
        public async Task<IActionResult> PrepareBlood(Guid id, [FromBody] BloodStorageCreateModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await bloodStorageService.PrepareBloodAsync(id, dto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
        [HttpGet("volume-summary")]
        public async Task<IActionResult> VolumeSummary(
            [FromQuery] Guid? bloodGroupId = null,
            [FromQuery] Guid? componentId = null)
        {
            try
            {
                var summary = await bloodStorageService.VolumeSummary(bloodGroupId, componentId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
    }
}
