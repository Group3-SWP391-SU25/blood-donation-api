using BloodDonation.Application.Models.Nurse;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/nurses")]
    [ApiController]
    public class NurseController : ControllerBase
    {
        private readonly INurseService nurseService;

        public NurseController(INurseService nurseService)
        {
            this.nurseService = nurseService;
        }

        [HttpGet("members")]
        public async Task<IActionResult> SearchMember(
            [FromQuery] string? search = "",
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10, 
            [FromQuery] Guid? bloodGroupId = null, 
            [FromQuery] string? address = null)
        {
            try
            {
                var result = await nurseService.SearchMemberAsync(search, pageIndex, pageSize, bloodGroupId, address);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendBloodCallEmail([FromBody] SendCallRequest request)
        {
            if (request.UserIds == null || !request.UserIds.Any())
                return BadRequest("Danh sách người nhận không được để trống.");

            try
            {
                await nurseService.SendCallForDonationEmailAsync(request.UserIds);
                return Ok(new { message = "Đã gửi lời kêu gọi hiến máu thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Gửi mail thất bại.", detail = ex.Message });
            }
        }

    }
}
