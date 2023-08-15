using ContactBookApp.Domain.Dtos.Requests;
using ContactBookApp.Domain.Dtos.Responses;

namespace ContactBookApp.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequestDto);
        Task<StandardResponse<IEnumerable<UserResponseDto>>> GetAllUserAsync();
        Task<StandardResponse<UserResponseDto>> GetUserByIdAsync(int id);
        Task<StandardResponse<UserResponseDto>> GetUserByEmailAsync(string email);
        Task<StandardResponse<UserResponseDto>> DeleteUserByIdAsync(int id);
        Task<StandardResponse<UserResponseDto>> UpdateUserAsync(int id, UserRequestDto userRequestDto);
    }
}
