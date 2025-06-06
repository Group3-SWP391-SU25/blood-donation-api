using BloodDonation.Application.Models.Auth;
using BloodDonation.Application.Models.Users;
using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] AuthRequestModel loginModel)
    {
        var result = await authService.LoginAsync(loginModel,
            cancellationToken: default);

        return Ok(result is not null
            ? result
            : throw new Exception("invalid email/password"));
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserInfo()
    {
        await Task.CompletedTask;
        // Assuming you have a method to get the user info based on the token
        return Ok("user info done");
    }
    //[HttpPost("sign-up")]
    //public async Task<IActionResult> SignUp([FromBody] UserCreateModel registerModel)
    //{
    //    var result = await authService.CreateUserAsync(model: registerModel,
    //        cancellationToken: default);

    //    return Ok(result is not null
    //        ? result
    //        : throw new Exception("create failed"));
    //}
}
