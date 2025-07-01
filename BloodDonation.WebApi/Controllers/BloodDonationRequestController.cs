using BloodDonation.Application.Models.BloodDonationRequests;
using BloodDonation.Application.Services;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using BloodDonation.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-donation-requests")]
    [ApiController]
    public class BloodDonationRequestController : ControllerBase
    {
        private readonly IBloodDonationRequestService bloodDonationRequestService;
        private readonly IClaimsService ClaimsService;
        public BloodDonationRequestController(IBloodDonationRequestService bloodDonationRequestService, IClaimsService claimsService)
        {
            this.bloodDonationRequestService = bloodDonationRequestService;
            ClaimsService = claimsService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(
                                                [FromQuery] BloodDonationRequestStatus? status,
                                                [FromQuery] TimeSlotEnum? timeSlot,
                                                [FromQuery] DateOnly? dateRequest,
                                                [FromQuery] string? search = "",
                                                [FromQuery] int pageIndex = 1,
                                                [FromQuery] int pageSize = 10,
                                                CancellationToken cancellationToken = default)
        {
            var result = await bloodDonationRequestService.SearchAsync(pageIndex, pageSize, status, search, timeSlot, dateRequest);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var request = await bloodDonationRequestService.GetByIdAsync(id);
                if (request == null)
                {
                    return NotFound(new { message = "Yêu cầu hiến máu không tồn tại." });
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BloodDonationRequestCreateModel dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Set the UserId from the claims
            dto.UserId = ClaimsService.CurrentUser;
            var createdRequest = await bloodDonationRequestService.CreateAsync(dto);
            return Ok(createdRequest);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] BloodDonationRequestUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                model.Id = id; // Set the Id from the route parameter
                var result = await bloodDonationRequestService.UpdatePartialAsync(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await bloodDonationRequestService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromQuery] BloodDonationRequestStatus status, [FromQuery] string? rejectNote)
        {
            try
            {
                var request = await bloodDonationRequestService.GetByIdAsync(id);
                if (request == null)
                {
                    return NotFound(new { message = "Yêu cầu hiến máu không tồn tại." });
                }
                var updatedRequest = await bloodDonationRequestService.UpdateStatusAsync(id, rejectNote,status);
                return Ok(updatedRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("user-requests")]
        public async Task<IActionResult> GetUserRequests([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = ClaimsService.CurrentUser;

                var requests = await bloodDonationRequestService.GetByUserIdAsync(userId, pageIndex, pageSize, cancellationToken);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateRangeFilter range)
        {
            try
            {
                var summary = await bloodDonationRequestService.GetSummaryAsync(range);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
    }
}
