using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyIoTBookingSystem.Api.Data;
using MyIoTBookingSystem.Api.DTOs.Homes;
using MyIoTBookingSystem.Api.Interfaces;
using MyIoTBookingSystem.Api.Models;
using System.Reflection.Metadata.Ecma335;

namespace MyIoTBookingSystem.Api.Controllers
{
    [ApiController, Route("api/homes/{homeId:int}/images"), Authorize(Roles="Administrator")]
    public class HomeImagesController(ApplicationDbContext context, IImageStorageService imageStorage, ILogger<HomeImagesController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage(int homeId, IFormFile image)
        {
            bool exists = await context.Homes
                .AnyAsync(home => home.HomeId == homeId);

            if (!exists)
                return NotFound();

            string reference = "";

            try
            {
                if (image is null || image.Length == 0)
                    return BadRequest();

                reference = imageStorage.SaveImage(image);
            } 
            catch (ArgumentException exception)
            {
                return BadRequest();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to save uploaded image for home {HomeId}", homeId);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            HomeImage homeImage = new() { HomeId = homeId, Url = reference };

            try
            {
                context.HomeImages.Add(homeImage);

                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to save image record for home {HomeId}", homeId);

                try
                {
                    imageStorage.DeleteImage(reference);
                }
                catch (Exception cleanupException)
                {
                    logger.LogError(
                        cleanupException,
                        "Failed to clean up uploaded image {ImageReference}",
                        reference);
                }

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(
                StatusCodes.Status201Created, 
                new HomeImageDto 
                { 
                    HomeImageId = homeImage.HomeImageId,
                    Url = homeImage.Url 
                });
        }

        [HttpDelete("{homeImageId:int}")]
        public async Task<IActionResult> DeleteImage(int homeId, int homeImageId)
        {
            HomeImage? image = await context.HomeImages
                .FirstOrDefaultAsync(image =>
                    image.HomeImageId == homeImageId &&
                    image.HomeId == homeId);

            if (image is null)
                return NotFound();

            try
            {
                context.HomeImages.Remove(image);

                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to remove image with id: {homeImageId} from database", homeImageId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            
            try
            {
                imageStorage.DeleteImage(image.Url);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Failed to delete image at: {imageUrl}", image.Url);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    
    }
}
