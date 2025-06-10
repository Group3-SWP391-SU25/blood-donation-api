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
            CancellationToken cancellationToken = default)
        {
            var result = await bloodStorageService.SearchAsync(pageIndex, pageSize, search, status, cancellationToken);
            return Ok(result);
        }
    }
}
