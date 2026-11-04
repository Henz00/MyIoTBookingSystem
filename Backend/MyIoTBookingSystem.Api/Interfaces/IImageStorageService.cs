using Microsoft.AspNetCore.Mvc;


namespace MyIoTBookingSystem.Api.Interfaces
{
    public interface IImageStorageService
    {
        string SaveImage(IFormFile image);
        void DeleteImage(string imageReference);
    }
}