using Microsoft.AspNetCore.Identity;

namespace MyIoTBookingSystem.Api.Services
{
    public class RoleInitializationService(RoleManager<IdentityRole> roleManager)
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task InitializeAsync()
        {
            await AddRoleAsync("Administrator");
            await AddRoleAsync("Customer");
        }

        private async Task AddRoleAsync(string nameOfRole)
        {
            if (!await _roleManager.RoleExistsAsync(nameOfRole))
            {
                IdentityRole role = new()
                {
                    Name = nameOfRole 
                };

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (!result.Succeeded)
                {
                    string errors = string.Join(", ", result.Errors.Select(error => error.Description));
                    throw new InvalidOperationException($"Failed to initialize database role: {nameOfRole}\n Errors: {errors}");
                }

            }
        }
    }
}
