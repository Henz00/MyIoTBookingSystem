using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Departments
{
    public class CreateDepartmentDto
    {
        [Required, MaxLength(150)]
        public required string Name { get; set; } 
    }
}
