using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-issues")]
    [ApiController]
    public class BloodIssueController : ControllerBase
    {
        private readonly IBloodIssueService bloodIssueService;
        private readonly IClaimsService ClaimsService;
        public BloodIssueController(IBloodIssueService bloodIssueService, IClaimsService claimsService)
        {
            this.bloodIssueService = bloodIssueService;
            ClaimsService = claimsService;
        }
        //[Authorize(Roles = "SUPERVISOR")]
        [HttpPost]
        public async Task<IActionResult> CreateBloodIssue([FromQuery] Guid EmergencyBloodRequestId, [FromBody] BloodIssueCreateModel reqDto)
        {
            try
            {
                var result = await bloodIssueService.CreateBloodIssueAsync(EmergencyBloodRequestId, reqDto);
                if (result)
                {
                    return Created();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles = "SUPERVISOR")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodIssue(Guid id, [FromBody] BloodIssueCreateModel reqDto)
        {
            try
            {
                var result = await bloodIssueService.UpdateBloodIssueAsync(id, reqDto);
                if (result)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
