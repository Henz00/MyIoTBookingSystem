using Microsoft.AspNetCore.Mvc;

namespace MyIoTBookingSystem.Api.Controllers
{
    public class PingController : Controller
    {
        [HttpGet("data")]
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
