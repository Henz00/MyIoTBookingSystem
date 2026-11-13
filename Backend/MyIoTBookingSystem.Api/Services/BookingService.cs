using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.DTOs.Requests.Bookings;
using MyIoTBookingSystem.Api.DTOs.Responses.Bookings;
using MyIoTBookingSystem.Api.Interfaces;
using MyIoTBookingSystem.Api.Models;

namespace MyIoTBookingSystem.Api.Services
{
    public class BookingService(ApplicationDbContext context) : IBookingService
    {
        public async Task<BookingDto> CreateCustomerBookingAsync(string userId, CreateCustomerBookingDto dto)
        {
            ApplicationUser? customer = await context.Users.FindAsync(userId);

            if (customer == null)
                return null!;

            return null!;
        }
        public Task<AdminBookingDto> CreateAdminBookingAsync(string adminUserId, CreateAdminBookingDto dto)
        {
            return null!;
        }
    }
}
