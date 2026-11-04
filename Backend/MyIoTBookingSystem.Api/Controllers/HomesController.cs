using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using MyIoTBookingSystem.Api.DTOs.Homes;

namespace MyIoTBookingSystem.Api.Controllers
{
    // best practice here, have both attributes in one square bracket, or one attribute for each square bracket?
    [ApiController, Route("api/[controller]")]
    public class HomesController(ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetHomes()
        {
            List<HomeDto> homeData = await context.Homes
                .Where(home => home.IsPublished)
                .Select(home => new HomeDto
                {
                    HomeId = home.HomeId,
                    AreaSquareMeters = home.AreaSquareMeters,
                    Name = home.Name,
                    City = home.City,
                    DailyPrice = home.DailyPrice,
                    DepartmentName = home.Department.Name,
                    DepartmentId = home.Department.DepartmentId,
                    Description = home.Description,
                    Images = home.Images.Select(image => new HomeImageDto { Url = image.Url }).ToList(),
                    RoomCount = home.RoomCount,
                    StreetName = home.StreetName,
                    StreetNumber = home.StreetNumber,
                    ZipNumber = home.ZipNumber,
                    IsBookable = home.IsBookable,
                    IsPublished = home.IsPublished
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(homeData);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHomeById(int id)
        {
            HomeDto? home = await context.Homes
                .Where(home => home.HomeId == id)
                .Where(home => home.IsPublished)
                .Select(home => new HomeDto
                {
                    HomeId = home.HomeId,
                    AreaSquareMeters = home.AreaSquareMeters,
                    Name = home.Name,
                    City = home.City,
                    DailyPrice = home.DailyPrice,
                    DepartmentName = home.Department.Name,
                    DepartmentId = home.Department.DepartmentId,
                    Description = home.Description,
                    Images = home.Images
                        .Select(image => new HomeImageDto { Url = image.Url })
                        .ToList(),
                    RoomCount = home.RoomCount,
                    StreetName = home.StreetName,
                    StreetNumber = home.StreetNumber,
                    ZipNumber = home.ZipNumber,
                    IsBookable = home.IsBookable,
                    IsPublished = home.IsPublished
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (home is null)
                return NotFound();

            return Ok(home);
        }

        [HttpPost, Authorize(Roles="Administrator")]
        public async Task<IActionResult> CreateHome(CreateHomeDto data)
        {
            bool departmentExists = await context.Departments
                .AnyAsync(department => department.DepartmentId == data.DepartmentId);
            
            if (!departmentExists)
                return BadRequest();

            Home home = new Home()
            {
                AreaSquareMeters = data.AreaSquareMeters,
                Name = data.Name,
                City = data.City,
                DailyPrice = data.DailyPrice,
                DepartmentId = data.DepartmentId,
                Description = data.Description,
                RoomCount = data.RoomCount,
                StreetName = data.StreetName,
                StreetNumber = data.StreetNumber,
                ZipNumber = data.ZipNumber
            };

            context.Homes.Add(home);

            await context.SaveChangesAsync();

            HomeDto homeData = new()
            {
                AreaSquareMeters = home.AreaSquareMeters,
                Name = home.Name,
                City = home.City,
                DailyPrice = home.DailyPrice,
                DepartmentId = home.DepartmentId,
                Description = home.Description,
                RoomCount = home.RoomCount,
                StreetName = home.StreetName,
                StreetNumber = home.StreetNumber,
                ZipNumber = home.ZipNumber
            };

            // update to point at administrator version of GetHomeById
            return CreatedAtAction(nameof(GetHomeById),
                new { id = home.HomeId },
                homeData
            );
        }

        [HttpPut("{id}"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> UpdateHome(int id, UpdateHomeDto updateHome)
        {
            Home? home = await context.Homes
                .Include(home => home.Images)
                .FirstOrDefaultAsync(home => home.HomeId == id);

            if (home is null)
                return NotFound();

            Department? department = await context.Departments.FindAsync(updateHome.DepartmentId);

            if (department is null)
                return BadRequest();

            home.Name = updateHome.Name;
            home.Description = updateHome.Description;
            home.ZipNumber = updateHome.ZipNumber;
            home.City = updateHome.City;
            home.StreetName = updateHome.StreetName;
            home.StreetNumber = updateHome.StreetNumber;
            home.DepartmentId = updateHome.DepartmentId;
            home.DailyPrice = updateHome.DailyPrice;
            home.RoomCount = updateHome.RoomCount;
            home.AreaSquareMeters = updateHome.AreaSquareMeters;

            await context.SaveChangesAsync();

            HomeDto homeData = new()
            {
                HomeId = id,
                Name = home.Name,
                Description = home.Description,
                ZipNumber = home.ZipNumber,
                City = home.City,
                StreetName = home.StreetName,
                StreetNumber = home.StreetNumber,
                DepartmentId = home.DepartmentId,
                DailyPrice = home.DailyPrice,
                RoomCount = home.RoomCount,
                AreaSquareMeters = home.AreaSquareMeters,
                DepartmentName = department.Name,
                Images = home.Images
                    .Select(image => new HomeImageDto
                    {
                        Url = image.Url
                    })
                    .ToList(),
                IsPublished = home.IsPublished,
                IsBookable = home.IsBookable
                    
            };

            return Ok(homeData);
        }

        [HttpDelete("{id}"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            Home? home = await context.Homes.FindAsync(id);

            if (home is null)
                return NotFound();

            context.Homes.Remove(home);

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("admin"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> AdminGetHomes()
        {
            List<HomeDto> homeData = await context.Homes
                .Select(home => new HomeDto
                {
                    HomeId = home.HomeId,
                    AreaSquareMeters = home.AreaSquareMeters,
                    Name = home.Name,
                    City = home.City,
                    DailyPrice = home.DailyPrice,
                    DepartmentName = home.Department.Name,
                    DepartmentId = home.Department.DepartmentId,
                    Description = home.Description,
                    Images = home.Images.Select(image => new HomeImageDto { Url = image.Url }).ToList(),
                    RoomCount = home.RoomCount,
                    StreetName = home.StreetName,
                    StreetNumber = home.StreetNumber,
                    ZipNumber = home.ZipNumber,
                    IsBookable = home.IsBookable,
                    IsPublished = home.IsPublished
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(homeData);
        }

        [HttpPatch("{id}/publish"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> PublishHome(int id, UpdateHomePublishedDto update)
        {
            Home? home = await context.Homes.FindAsync(id);

            if (home is null)
                return NotFound();

            if (home.DailyPrice is null)
                return BadRequest();

            home.IsPublished = update.IsPublished;
            
            if (!update.IsPublished)
                home.IsBookable = false;

            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}/bookable"), Authorize(Roles="Administrator")]
        public async Task<IActionResult> MakeHomeBookable(int id, UpdateHomeBookableDto update)
        {
            Home? home = await context.Homes.FindAsync(id);
            
            if (home is null)
                return NotFound();

            if (!home.IsPublished && update.IsBookable)
                return BadRequest();

            home.IsBookable = update.IsBookable;

            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}
