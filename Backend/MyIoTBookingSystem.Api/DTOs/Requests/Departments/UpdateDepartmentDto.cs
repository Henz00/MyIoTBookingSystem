using System.ComponentModel.DataAnnotations;

namespace MyIoTBookingSystem.Api.DTOs.Requests.Departments
{
    public class UpdateDepartmentDto
    {
        [Required, MaxLength(150)]
        public required string Name { get; set; }
    }
}
