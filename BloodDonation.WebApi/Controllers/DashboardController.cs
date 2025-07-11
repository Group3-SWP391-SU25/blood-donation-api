using BloodDonation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonation.WebApi.Controllers
{
    [Route("api/dashboards")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("nurse")]
        public async Task<IActionResult> GetNurseDashboardAsync()
        {
            var dashboard = await dashboardService.GetNurseDashboardAsync();
            return Ok(dashboard);
        }
        [HttpGet("supervisor")]
        public async Task<IActionResult> GetSupervisorDashboardAsync()
        {
            var dashboard = await dashboardService.GetSupervisorDashboard();
            return Ok(dashboard);
        }
    }
}
