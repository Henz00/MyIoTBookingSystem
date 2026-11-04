using MyIoTBookingSystem.Api.Interfaces;
using SkiaSharp;

namespace MyIoTBookingSystem.Api.Services
{
    public class ImageStorageService(IWebHostEnvironment environment) : IImageStorageService
    {
        private const long maxFileSize = 5 * 1024 * 1024;
        private string imageDirectory = Path.Combine(environment.WebRootPath, "uploads", "homes");

        public string SaveImage(IFormFile image)
        {
            if (image.Length == 0)
                throw new ArgumentException("The uploaded image is empty.");

            if (image.Length > maxFileSize)
                throw new ArgumentException("The uploaded image is too large.");

           
            var processedImage = ProcessImage(image);
            
            using SKData data = processedImage.data;

            string imageName = $"{Guid.NewGuid()}.{GetFileExtension(processedImage.format)}";
            string path = Path.Combine(imageDirectory, imageName);
            Directory.CreateDirectory(imageDirectory);

            try
            {
                using FileStream stream = new(path, FileMode.CreateNew);
                data.SaveTo(stream);
            }
            catch
            {
                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
                catch
                {

                }

                throw;
            }

            string reference = Path.Combine("uploads", "homes", imageName).Replace('\\', '/');

            return reference;
        }

        public void DeleteImage(string imageReference)
        {
            string imagePath = Path.GetFullPath(
                Path.Combine(environment.WebRootPath, imageReference)
            );

            string normalizedImageDirectory = Path.GetFullPath(imageDirectory);

            if(!imagePath.StartsWith(
                normalizedImageDirectory + Path.DirectorySeparatorChar,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Invalid image reference.");
            }
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image doesn't exist.", imagePath);
            
            File.Delete(imagePath);
        }

        private static (SKData data, SKEncodedImageFormat format) ProcessImage(IFormFile image)
        {
            using var stream = image.OpenReadStream();
            using SKCodec codec = SKCodec.Create(stream);

            if (codec is null)
                throw new ArgumentException("File is not a valid image.");

            SKEncodedImageFormat format = GetImageFormat(codec);

            using SKBitmap bitmap = ValidateImage(codec);

            using SKImage newImage = SKImage.FromBitmap(bitmap);
            SKData? encodedData = newImage.Encode(format, 85);

            if (encodedData is null)
                throw new ArgumentException("Image could not be encoded.");

            return (encodedData, format);
        }

        private static SKEncodedImageFormat GetImageFormat(SKCodec codec)
        {
            if (codec.EncodedFormat is not (SKEncodedImageFormat.Jpeg or SKEncodedImageFormat.Png or SKEncodedImageFormat.Webp))
                throw new ArgumentException("File format is not supported");

            return codec.EncodedFormat;
        }

        private static SKBitmap ValidateImage(SKCodec codec)
        {
            const int maxWidth = 4096;
            const int maxHeight = 4096;
            const long maxPixels = 16_000_000;
            SKImageInfo info = codec.Info;

            if (info.Width <= 0 || info.Height <= 0)
                throw new ArgumentException("Invalid image dimensions");

            if (info.Width > maxWidth || info.Height > maxHeight || (long)info.Width * info.Height > maxPixels)
                    throw new ArgumentException("Image dimensions are too large");

            SKBitmap? bitmap = SKBitmap.Decode(codec);

            if (bitmap is null)
                throw new ArgumentException("File could not be decoded.");

            return bitmap;
        }

        private static string GetFileExtension(SKEncodedImageFormat format)
        {
            return format switch
            {
                SKEncodedImageFormat.Jpeg => ".jpg",
                SKEncodedImageFormat.Png => ".png",
                SKEncodedImageFormat.Webp => ".webp",
                _ => throw new ArgumentException("Unsupported image format.")
            };
        }
    }
}
