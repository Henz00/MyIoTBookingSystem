namespace MyIoTBookingSystem.Api.Models
{
    public class HomeImage
    {
        public int HomeImageId { get; set; }
        public int HomeId { get; set; }
        public string URL { get; set; } = string.Empty;
    }
}
