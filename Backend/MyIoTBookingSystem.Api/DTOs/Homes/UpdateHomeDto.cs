using MyIoTBookingSystem.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Homes
{
    public class UpdateHomeDto
    {
        [Required, MaxLength(150)]
        public required string Name { get; set; }

        [MaxLength(1500)]
        public string? Description { get; set; }

        [Required, MaxLength(10)]
        public required string ZipNumber { get; set; }
        
        [Required, MaxLength(100)]
        public required string City { get; set; }

        [Required, MaxLength(150)]
        public required string StreetName { get; set; }
        
        [Required, MaxLength(16)]
        public required string StreetNumber { get; set; }

        [Range(1, int.MaxValue)]
        public int DepartmentId { get; set; }

        [Range(typeof(decimal), "0.01", "999999999")]
        public decimal? DailyPrice { get; set; }


        [Range(1, int.MaxValue)]
        public int RoomCount { get; set; }

        [Range(1, int.MaxValue)]
        public int AreaSquareMeters { get; set; }
    }
}
