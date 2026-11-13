using MyIoTBookingSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Requests.Bookings
{
    public class CreateAdminBookingDto
    {
        [Required, MaxLength(450)]
        public string CustomerId { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int HomeId { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }

        [EnumDataType(typeof(BookingPaymentArrangement))]
        public BookingPaymentArrangement PaymentArrangement { get; set; }

        [MaxLength(2000)]
        public string? ComplimentaryReason { get; set; }
    }
}
