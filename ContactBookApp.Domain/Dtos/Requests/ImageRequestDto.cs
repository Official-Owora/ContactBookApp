using Microsoft.AspNetCore.Http;

namespace ContactBookApp.Domain.Dtos.Requests
{
    public class ImageRequestDto
    {
        public IFormFile File { get; set; }
    }
}
