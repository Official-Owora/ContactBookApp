using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ContactBookApp.Application.Services.Interfaces;
using ContactBookApp.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ContactBookApp.Application.Services.Implementations
{
    public class ImageService : IImageService
    {
        public IConfiguration Configuration { get; }
        private CloudinarySettings _cloudinarySettings;
        private Cloudinary _cloudinary;
        public ImageService(IConfiguration configuration)
        {
            Configuration = configuration;
            _cloudinarySettings = new();
            var jwtSettings = Configuration.GetSection("CloudinarySettings");
            _cloudinarySettings.CloudName = jwtSettings["CloudName"];
            _cloudinarySettings.ApiKey = jwtSettings["ApiKey"];
            _cloudinarySettings.ApiSecret = jwtSettings["ApiSecret"];
            Account account = new Account(_cloudinarySettings.CloudName, _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        
        public string AddImageForUser(string userId, IFormFile file)
        {
            var uploadResultParams = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResultParams = _cloudinary.Upload(uploadParams);

                }
            }
            string url = uploadResultParams.Url.ToString();
            string publicId = uploadResultParams.PublicId;
            return url;
        }
    }
}
