using BloodDonation.Application.Models.BloodIssues;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [ApiController]
    [Route("api/blood-issues")]
    public class BloodIssuesController : ControllerBase
    {
        private readonly IBloodIssueService bloodIssueService;
        public BloodIssuesController(IBloodIssueService bloodIssueService)
        {
            this.bloodIssueService = bloodIssueService;

        }
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] string? search = "",
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] BloodIssueStatusEnum? status = null,
            CancellationToken cancellationToken = default)
        {
            return Ok(await bloodIssueService.GetAll(search: search, pageIndex: pageIndex,
                pageSize: pageSize, status: status,
                cancellationToken: cancellationToken));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await bloodIssueService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BloodIssueCreateModel model)
        {
            var result = await bloodIssueService.CreateAsync(model);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] BloodIssueUpdateModel model)
        {
            await bloodIssueService.UpdateAsync(id, model);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Del([FromRoute] Guid id)
        {
            await bloodIssueService.DeleteAsync(id);
            return NoContent();
        }


    }
}
