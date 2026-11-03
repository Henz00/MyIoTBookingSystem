using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet("check"), Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Test()
        {
            Dictionary<string, string> data = new()
            {
                { "message", "You have administrator access" }
            };

            return Ok(data);
        }
    }
}
