using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyIoTBookingSystem.Api.Controllers
{
    public class PingController : ControllerBase
    {
        [HttpGet("data")]
        [Authorize]
        public Dictionary<string, string> Index()
        {
            Dictionary<string, string> ping = new()
            {
                {"Data", "ping!"}
            };

            return ping;
        }
    }
}
