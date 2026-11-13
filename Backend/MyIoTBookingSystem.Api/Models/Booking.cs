using System.ComponentModel.DataAnnotations;
using MyIoTBookingSystem.Api.Enums;

namespace MyIoTBookingSystem.Api.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int HomeId { get; set; }
        public Home Home { get; set; } = null!;
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
        public BookingStatus Status { get; set; }
        public required string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; } = null!;
        public required string CustomerName { get; set; }
        public required string CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public decimal AgreedDailyPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public required string CreatedByUserId { get; set; }
        public ApplicationUser CreatedByUser { get; set; } = null!;
        public DateTimeOffset? CancelledAt { get; set; }
        public string? CancelledByUserId { get; set; }
        public ApplicationUser? CancelledByUser { get; set; }
        public string? CancellationReason { get; set; }
    }
}
