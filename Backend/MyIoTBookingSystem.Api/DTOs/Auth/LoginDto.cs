using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Auth
{
    public class LoginDto
    {
        [Required, MaxLength(150)]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
