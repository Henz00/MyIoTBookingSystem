using Microsoft.AspNetCore.Mvc;

namespace MyIoTBookingSystem.Api.Controllers
{
    public class BookingController : ControllerBase
    {
        public async Task<IActionResult> MethodName()
        {
            DateOnly arrivalDate_booking1 = new DateOnly(2026, 11, 13);
            DateOnly departureDate_booking1 = new DateOnly(2026, 11, 15);

            DateOnly arrivalDate_booking2 = new DateOnly(2026, 11, 15);
            DateOnly departureDate_booking2 = new DateOnly(2026, 11, 17);

            bool overlaps = arrivalDate_booking2 <= departureDate_booking1 && 
                            departureDate_booking2 >= arrivalDate_booking1;

            return Ok();
        }
    }
}
