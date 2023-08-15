using ContactBookApp.Domain.Dtos.Requests;
using Microsoft.AspNetCore.Identity;

namespace ContactBookApp.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserRequestDto userRequestDto);
        Task<bool> ValidateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
    }
}
