using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using MyIoTBookingSystem.Api.Models;
using Microsoft.AspNetCore.Authorization;
using MyIoTBookingSystem.Api.DTOs.Responses.Departments;
using MyIoTBookingSystem.Api.DTOs.Requests.Departments;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class DepartmentsController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            List<DepartmentDto> departmentData = await context.Departments
                .Select(department => new DepartmentDto
                {
                    DepartmentId = department.DepartmentId,
                    Name = department.Name
                })
                .ToListAsync();

            return Ok(departmentData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            Department? departmentData = await context.Departments.FindAsync(id);

            if (departmentData is null)
                return NotFound();

            DepartmentDto department = new()
            {
                DepartmentId = departmentData.DepartmentId,
                Name = departmentData.Name
            };

            return Ok(department);
        }

        [HttpPost, Authorize(Roles="Administrator")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto data)
        {
            Department department = new()
            {
                Name = data.Name
            };

            context.Departments.Add(department);

            await context.SaveChangesAsync();

            DepartmentDto departmentData = new()
                { DepartmentId = department.DepartmentId, Name = department.Name }; 

            return CreatedAtAction(
                nameof(GetDepartmentById),
                new { id = department.DepartmentId},
                departmentData
            );
        }

        [HttpPut("{id}"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto data)
        {
            Department? department = await context.Departments.FindAsync(id);

            if (department is null)
                return NotFound();

            department.Name = data.Name;

            await context.SaveChangesAsync();

            DepartmentDto departmentData = new()
                { DepartmentId =  id, Name = department.Name };

            return Ok(departmentData);
        }

        [HttpDelete("{id}"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            Department? department = await context.Departments.FindAsync(id);

            if (department is null)
                return NotFound();

            var homes = await context.Homes.AnyAsync(home => home.DepartmentId == id);

            if (homes)
                return Conflict();

            context.Departments.Remove(department);

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
