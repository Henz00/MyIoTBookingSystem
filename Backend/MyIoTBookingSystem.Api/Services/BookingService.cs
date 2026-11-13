using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            if (customer is null)
                throw new ArgumentNullException();

            if (customer.Email is null || customer.FullName is null)
                throw new ArgumentNullException();

            Home? home = await context.Homes.FindAsync(dto.HomeId);

            if (home is null)
                throw new ArgumentNullException();

            if (home.DailyPrice is null || !home.IsPublished || !home.IsBookable)
                throw new ArgumentNullException();

            decimal dailyBookingPrice = home.DailyPrice.Value;

            int daysBooked = dto.DepartureDate.DayNumber - dto.ArrivalDate.DayNumber;

            Booking newBooking = new()
            {
                CustomerId = customer.Id,
                Customer = customer,
                CreatedByUserId = customer.Id,
                CustomerEmail = customer.Email,
                CustomerName = customer.FullName,
                CustomerPhone = customer.PhoneNumber,
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedByUser = customer,
                AgreedDailyPrice = dailyBookingPrice,
                TotalPrice = dailyBookingPrice * daysBooked,
                ArrivalDate = dto.ArrivalDate,
                DepartureDate = dto.DepartureDate,
                PaymentArrangement = Enums.BookingPaymentArrangement.OnlinePayment,
                Home = home,
                HomeId = home.HomeId,
                Status = Enums.BookingStatus.PendingPayment 
            };

            await using var transaction = await context.Database.BeginTransactionAsync();

            bool available = await CheckHomeAvailability(home.HomeId, dto.ArrivalDate, dto.DepartureDate);

            if (!available)
                throw new ArgumentException();

            context.Bookings.Add(newBooking);

            await context.SaveChangesAsync();

            await transaction.CommitAsync();

            return new BookingDto(){
                BookingId = newBooking.BookingId,
                HomeName = home.Name,
                ArrivalDate = newBooking.ArrivalDate,
                DepartureDate = newBooking.DepartureDate,
                Status = newBooking.Status,
                CustomerEmail = customer.Email,
                CustomerName = customer.FullName,
                CustomerPhone = customer.PhoneNumber,
                AgreedDailyPrice = newBooking.AgreedDailyPrice,
                TotalPrice = newBooking.TotalPrice,
                PaymentArrangement = newBooking.PaymentArrangement,
                CreatedAt = newBooking.CreatedAt,
            };
        }

        public Task<AdminBookingDto> CreateAdminBookingAsync(string adminUserId, CreateAdminBookingDto dto)
        {
            return null!; // temporary
        }

        private async Task<bool> CheckHomeAvailability(int homeId, DateOnly arrivalDate, DateOnly departureDate)
        {
            bool hasOverlap = await context.Bookings.AnyAsync(booking => 
                booking.HomeId == homeId &&
                (booking.Status == Enums.BookingStatus.PendingPayment ||
                 booking.Status == Enums.BookingStatus.Confirmed) &&
                arrivalDate <= booking.DepartureDate &&
                departureDate >= booking.ArrivalDate
            );

            return !hasOverlap;
        }
    }
}
