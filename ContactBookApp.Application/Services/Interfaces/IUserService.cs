using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Dtos.Requests;
using ContactBookApp.Domain.Dtos.Responses;
using Microsoft.AspNetCore.Http;

namespace ContactBookApp.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequestDto);
        Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData pagingData)>> GetAllUserAsync(UserRequestInputParameter parameter);
        Task<StandardResponse<UserResponseDto>> GetUserByIdAsync(string id);
        Task<StandardResponse<UserResponseDto>> GetUserByEmailAsync(string email);
        Task<StandardResponse<UserResponseDto>> DeleteUserAsync(string id);
        Task<StandardResponse<UserResponseDto>> UpdateUserAsync(string id, UserRequestDto userRequestDto);
        Task<StandardResponse<(bool, string)>> UploadProfileImageAsync(string userId, IFormFile file);
    }
}
