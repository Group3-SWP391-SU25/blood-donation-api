﻿using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/blood-components")]
    [ApiController]
    public class BloodComponentController : ControllerBase
    {
        private readonly IBloodComponentService bloodComponentService;
        private readonly IClaimsService ClaimsService;
        public BloodComponentController(IBloodComponentService bloodComponentService, IClaimsService claimsService)
        {
            this.bloodComponentService = bloodComponentService;
            ClaimsService = claimsService;
        }
        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] string? search = "")
        {
            try
            {
                var result = await bloodComponentService.GetAllAsync(search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi hệ thống!", detail = ex.Message });
            }
        }
    }
}
