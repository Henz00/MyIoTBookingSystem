using MyIoTBookingSystem.Api.Enums;
using MyIoTBookingSystem.Api.Models;

namespace MyIoTBookingSystem.Api.DTOs.Responses.Bookings
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public string HomeName { get; set; } = string.Empty;
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public BookingStatus Status { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal AgreedDailyPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingPaymentArrangement PaymentArrangement { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }
    }
}