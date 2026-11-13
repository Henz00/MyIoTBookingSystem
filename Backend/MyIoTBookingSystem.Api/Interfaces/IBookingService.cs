using MyIoTBookingSystem.Api.DTOs.Requests.Bookings;
using MyIoTBookingSystem.Api.DTOs.Responses.Bookings;

namespace MyIoTBookingSystem.Api.Interfaces
{
    public interface IBookingService
    {
        Task<BookingDto> CreateCustomerBookingAsync(string userId, CreateCustomerBookingDto dto);
        Task<AdminBookingDto> CreateAdminBookingAsync(string adminUserId, CreateAdminBookingDto dto);
    }
}
