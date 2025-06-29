using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using BloodDonation.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers;

[ApiController]

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(
    [FromQuery] int? pageSize,
    [FromQuery] string search = "",
    [FromQuery] int pageIndex = 0,
    [FromQuery] bool? isDeleted = false)
    {
        var result = await userService.GetAsync(search: search,
            pageSize: pageSize,
            pageIndex: pageIndex,
            cancellationToken: default,
            isDeleted: isDeleted);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] UserCreateModel model,
        [FromServices] IValidator<UserCreateModel> validator)
    {
        var validatorRes = validator.Validate(model);
        if (!validatorRes.IsValid)
        {
            return BadRequest(validatorRes.Errors);
        }

        var result = await userService.CreateUserAsync(model, default);
        return result is not null
            ? Ok(result)
            : throw new Exception("Created Failed");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] Guid id,
    [FromBody] UserUpdateModel model)
    {
        var result = await userService.UpdateAsync(id, model, default);
        return result
            ? NoContent()
            : throw new Exception("Update Failed");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await userService.GetById(id, default);
        return result is not null
            ? Ok(result)
            : throw new Exception($"Not found Id: {id}");
    }
    [HttpPut("{id}/unban")]
    public async Task<IActionResult> Unban([FromRoute] Guid id)
    {
        var result = await userService.ActiveAsync(id, UserStatusEnum.Active.ToString(), default);
        return result
            ? Ok("Unban sucessfully")
            : throw new Exception("Unban người dùng Failed");
    }

    [HttpPut("{id}/ban")]
    public async Task<IActionResult> BanAsync([FromRoute] Guid id)
    {
        var result = await userService.ActiveAsync(id, UserStatusEnum.InActive.ToString(), default);
        return result
            ? Ok("Ban sucessfully")
            : throw new Exception("Unban người dùng Failed");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Del([FromRoute] Guid id)
    {
        var result = await userService.RemoveAsync(id, default);
        return result
            ? Ok("Delêt Successfully")
            : throw new Exception("Delete Failed");
    }
}
