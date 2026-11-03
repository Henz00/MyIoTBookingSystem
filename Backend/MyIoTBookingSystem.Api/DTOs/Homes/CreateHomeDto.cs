using MyIoTBookingSystem.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Homes
{
    public class CreateHomeDto
    {
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public required string ZipNumber { get; set; } = string.Empty;
        public required string City { get; set; } = string.Empty;
        public required string StreetName { get; set; } = string.Empty;
        public required string StreetNumber { get; set; } = string.Empty;
        public required int DepartmentId { get; set; }

        [Range(typeof(decimal), "0.01", "999999999")]
        public decimal? DailyPrice { get; set; }

        [Range(1, int.MaxValue)]
        public required int RoomCount { get; set; }

        [Range(1, int.MaxValue)]
        public required int AreaSquareMeters { get; set; }
    }
}
