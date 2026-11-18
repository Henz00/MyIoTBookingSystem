using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.DTOs.Requests.Bookings;
using MyIoTBookingSystem.Api.DTOs.Responses.Bookings;
using MyIoTBookingSystem.Api.Interfaces;
using MyIoTBookingSystem.Api.Models;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController, Route("api/bookings")]
    public class BookingController(ApplicationDbContext context, IBookingService bookingService, ILogger<BookingController> logger) : ControllerBase
    {
        [HttpGet, Authorize]
        public async Task<IActionResult> GetBooking(int id)
        {
            Booking? booking = await context.Bookings.FindAsync(id);

            if(booking is null)
                return NotFound();

            return Ok(new BookingDto 
                {
                    BookingId = booking.BookingId,
                    HomeName = booking.Home.Name,
                    CustomerName = booking.CustomerName,
                    CustomerEmail = booking.CustomerEmail,
                    CustomerPhone = booking.CustomerPhone,
                    AgreedDailyPrice = booking.AgreedDailyPrice,
                    TotalPrice = booking.TotalPrice,
                    ArrivalDate = booking.ArrivalDate,
                    DepartureDate = booking.DepartureDate,
                    PaymentArrangement = booking.PaymentArrangement,
                    Status = booking.Status,
                    CancellationReason = booking.CancellationReason,
                    CancelledAt = booking.CancelledAt,
                    CreatedAt = booking.CreatedAt
                });
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateBooking(string customerId, CreateCustomerBookingDto dto)
        {
            BookingDto booking;
            try
            {
                booking = await bookingService.CreateCustomerBookingAsync(customerId, dto);
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "Failed to create booking for customer {customerId}.", customerId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(booking);
        }
    }
}
