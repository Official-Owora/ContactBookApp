using Microsoft.AspNetCore.Http;

namespace ContactBookApp.Application.Services.Interfaces
{
    public interface IImageService
    {
        string AddImageForUser(string userId, IFormFile file);
    }
}
