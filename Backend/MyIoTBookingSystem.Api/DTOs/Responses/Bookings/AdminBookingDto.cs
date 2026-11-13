using MyIoTBookingSystem.Api.Enums;

namespace MyIoTBookingSystem.Api.DTOs.Responses.Bookings
{
    public class AdminBookingDto
    {
        public int BookingId { get; set; }
        public int HomeId { get; set; }
        public string HomeName { get; set; } = string.Empty;
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public BookingStatus Status { get; set; }
        public required string CustomerId { get; set; }
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal AgreedDailyPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public required string CreatedByUserName { get; set; }
        public DateTimeOffset? CancelledAt { get; set; }
        public string? CancelledByUserName { get; set; }
        public string? CancellationReason { get; set; }
        public BookingPaymentArrangement PaymentArrangement { get; set; }
    }
}
