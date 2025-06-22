using BloodDonation.Application.Models.BloodStorage;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
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
            CancellationToken cancellationToken = default)
        {
            var result = await bloodStorageService.SearchAsync(pageIndex, pageSize, search, status, cancellationToken);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BloodStorageCreateModel model)
        {
            var createResult = await bloodStorageService.CreateAsync(model);
            return Ok(createResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del([FromRoute] Guid id)
        {
            await bloodStorageService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BloodStorageUpdateModel updateModel)
        {
            await bloodStorageService.UpdateAsync(id, updateModel);
            return NoContent();
        }
    }
}
