using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    [Route("api/emergency-blood-requests")]
    public class EmergencyBloodRequestsController : ControllerBase
    {
        private IEmergencyBloodService emergencyBloodService;
        public EmergencyBloodRequestsController(IEmergencyBloodService emergencyBloodService)
        {
            this.emergencyBloodService = emergencyBloodService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmergencyBloodCreateModel model)
        {
            var createResult = await emergencyBloodService.CreateAsync(model);
            return Ok(createResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id,
            [FromBody] EmergencyBloodUpdateModel model)
        {
            await emergencyBloodService.UpdateAsync(id, model, default);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Del(Guid id)
        {
            await emergencyBloodService.DelAsync(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] EmergencyBloodRequestEnum? status = null)
        {
            var res = await emergencyBloodService.GetAllAsync(searchTerm: search,
                pageSize: pageSize,
                pageIndex: pageIndex,
                status: status);
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var res = await emergencyBloodService.GetByIdAsync(id);
            return Ok(res);
        }
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateRangeFilter range)
        {
            try
            {
                var summary = await emergencyBloodService.GetSummaryAsync(range);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống", detail = ex.Message });
            }
        }
    }
}
