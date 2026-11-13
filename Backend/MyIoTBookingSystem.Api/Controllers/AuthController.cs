using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.DTOs.Requests.Auth;
using MyIoTBookingSystem.Api.DTOs.Responses.Auth;
using MyIoTBookingSystem.Api.Models;
using System.Security.Claims;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            await transaction.CommitAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAccount(LoginDto userData)
        {
            ApplicationUser? user = await userManager.FindByNameAsync(userData.Email);

            if (user is null)
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid email or password");

            var result = await signInManager.PasswordSignInAsync(user, userData.Password, false, false);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status401Unauthorized, "Invalid email or password");

            return Ok();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserInformation()
        {
            var user = await userManager.GetUserAsync(User);
            
            if (user is null)
                return Unauthorized();

            var roles = await userManager.GetRolesAsync(user);

            UserInfoDto userData = new()
            {
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Roles = [.. roles]
            };

            return Ok(userData);
        }

    }
}
