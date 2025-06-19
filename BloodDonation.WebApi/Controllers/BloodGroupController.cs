using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-groups")]
    [ApiController]
    public class BloodGroupController : ControllerBase
    {
        private readonly IBloodGroupService bloodGroupService;

        public BloodGroupController(IBloodGroupService bloodGroupService)
        {
            this.bloodGroupService = bloodGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await bloodGroupService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }
    }
}
