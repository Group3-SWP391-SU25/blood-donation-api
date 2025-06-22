using BloodDonation.Application.Models.BloodEmergencyRequests;
using BloodDonation.Application.Models.EmergencyBloodRequests;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    [Route("api/emergency-blood-request")]
    public class EmergencyBloodController : ControllerBase
    {
        private readonly IEmergencyBloodService emergencyBloodService;
        public EmergencyBloodController(IEmergencyBloodService emergencyBloodService)
        {
            this.emergencyBloodService = emergencyBloodService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmergencyBloodRequestCreateModel createModel)
        {
            var res = await emergencyBloodService.CreateAsync(createModel);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? search = "",
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] BloodEmergencyStatusEnum? status = null,
            CancellationToken cancellationToken = default)
        {
            return Ok(await emergencyBloodService.GetAll(pageIndex,
                pageSize,
                search,
                status,
                cancellationToken));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Del([FromRoute] Guid id)
        {
            await emergencyBloodService.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await emergencyBloodService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] EmergencyBloodRequestUpdateModel model)
        {
            await emergencyBloodService.UpdateAsync(id, model, default);
            return NoContent();
        }
    }
}
