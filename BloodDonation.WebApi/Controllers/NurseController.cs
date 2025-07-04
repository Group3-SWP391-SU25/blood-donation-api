using BloodDonation.Application.Services.Interfaces;
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
        //[HttpPost("send-blood-request")]
        //public async Task<IActionResult> SendBloodRequest([FromBody] BloodRequestCreateModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        await nurseService.SendBloodRequestAsync(model);
        //        return CreatedAtAction(nameof(SendBloodRequest), new { id = model.Id }, model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });

        //    }
        //}
    }
}
