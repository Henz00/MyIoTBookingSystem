using MyIoTBookingSystem.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Requests.Bookings
{
    public class CreateCustomerBookingDto
    {
        [Range(1,int.MaxValue)]
        public int HomeId { get; set; }
        public DateOnly ArrivalDate { get; set; }
        public DateOnly DepartureDate { get; set; }
    }
}
