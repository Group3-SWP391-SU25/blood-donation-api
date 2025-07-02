using BloodDonation.Application.Models.Blogs;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    //[Authorize(Roles = RoleNames.ADMIN)]
    [ApiController]
    [Route("api/blogs")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService blogService;
        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? search = null,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0)
        {
            return Ok(await blogService.GetAllAsync(pageSize, pageIndex, search));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await blogService.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogCreateModel model)
        {
            var res = await blogService.CreateAsync(model);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id,
            [FromBody] BlogCreateModel model)
        {
            var res = await blogService.UpdateAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Del([FromRoute] Guid id)
        {
            var res = await blogService.DelAsync(id);
            return NoContent();
        }
    }
}
