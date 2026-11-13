using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Requests.Auth
{
    public class RegisterDto
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress, Required]
        public string Email {  get; set; } = string.Empty;
        
        [Required, MinLength(8, ErrorMessage="Password length must be minimum 8 characters long")]
        public string Password {  get; set; } = string.Empty;

        [Compare("Password"), Required]
        public string ConfirmPassword {  get; set; } = string.Empty;

        [Required, Phone, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
