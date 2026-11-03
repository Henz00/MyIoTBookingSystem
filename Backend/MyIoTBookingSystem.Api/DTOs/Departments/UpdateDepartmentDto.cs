using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Departments
{
    public class UpdateDepartmentDto
    {
        [Required, MaxLength(150)]
        public required string Name { get; set; }
    }
}
