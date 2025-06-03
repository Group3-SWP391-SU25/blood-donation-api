using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-donation-requests")]
    [ApiController]
    public class BloodDonationRequestController : ControllerBase
    {
        private readonly IBloodDonationRequestService bloodDonationRequestService;
        public BloodDonationRequestController(IBloodDonationRequestService bloodDonationRequestService)
        {
            this.bloodDonationRequestService = bloodDonationRequestService;
        }
        [HttpGet]
        public IActionResult Search(
            [FromQuery] int? pageSize = null,
            [FromQuery] string search = "",
            [FromQuery] int pageIndex = 0)
        {
            var result = bloodDonationRequestService.SearchAsync(
                pageSize: pageSize,
                search: search,
                pageIndex: pageIndex,
                cancellationToken: default).Result;

            return result != null
                ? Ok(result)
                : NotFound("No blood donation requests found.");
        }
    }
}
