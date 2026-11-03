namespace MyIoTBookingSystem.Api.Models
{
    public class Home
    {
        public int HomeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string ZipNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public decimal? DailyPrice { get; set; }
        public bool IsPublished { get; set; }
        public bool IsBookable { get; set; }
        public int RoomCount { get; set; }
        public int AreaSquareMeters { get; set; }
        public ICollection<HomeImage> Images { get; set; } = new List<HomeImage>();
    }
}
