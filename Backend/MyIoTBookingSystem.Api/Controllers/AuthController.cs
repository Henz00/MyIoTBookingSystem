using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.DTOs.Auth;
using MyIoTBookingSystem.Api.Models;
using System.Net;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        ApplicationDbContext context
    ) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNewUserAccount(RegisterDto register)
        {
            ApplicationUser newUser = new()
            {
                FullName = register.FullName,
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber
            };

            await using var transaction = await context.Database.BeginTransactionAsync();

            IdentityResult userRegistrationResult = await userManager.CreateAsync(newUser, register.Password);
            if (!userRegistrationResult.Succeeded)
            {
                await transaction.RollbackAsync();
                string errors = string.Join(", ", userRegistrationResult.Errors.Select(error => error.Description));
                return StatusCode(StatusCodes.Status400BadRequest, errors);
            }
                

            IdentityResult userRoleRegistrationResult = await userManager.AddToRoleAsync(newUser, "Customer");
            if (!userRoleRegistrationResult.Succeeded)
            {
                await transaction.RollbackAsync();
                string errors = string.Join(", ", userRoleRegistrationResult.Errors.Select(error => error.Description));
                return StatusCode(StatusCodes.Status500InternalServerError, errors);
            }
               
            await transaction.CommitAsync();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
