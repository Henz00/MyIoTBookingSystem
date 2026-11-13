using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Responses.Auth
{
    public class UserInfoDto
    {
        [MaxLength(150)]
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
