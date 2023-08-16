using AutoMapper;
using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Application.Services.Interfaces;
using ContactBookApp.Domain.Dtos.Requests;
using ContactBookApp.Domain.Dtos.Responses;
using ContactBookApp.Domain.Entities;
using ContactBookApp.Infrastructure.RepositoryManager;
using Microsoft.Extensions.Logging;

namespace ContactBookApp.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequestDto)
        {
            if (userRequestDto == null)
            {
                _logger.LogError($"User details cannot be null");
                return StandardResponse<UserResponseDto>.Failed($"User Request is null");
            }
            _logger.LogInformation($"Attempting to create a user: {DateTime.Now}");
            var newUser = _mapper.Map<User>( userRequestDto);
            _logger.LogInformation($"User successfully created : {DateTime.Now}");
            //save to the database
            _unitOfWork.UserRepository.CreateAsync(newUser);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"User {newUser.Id} successfully saved");
            var newUserToReturn = _mapper.Map<UserResponseDto>(newUser);
            return StandardResponse<UserResponseDto>.Success($"Successfully created a user {userRequestDto.FirstName } {userRequestDto.LastName}", newUserToReturn, 201);
        }
        public async Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>> GetAllUserAsync(UserRequestInputParameter parameter)
        {
            var result = await _unitOfWork.UserRepository.GetAllUsersAsync(parameter);
            var usersToReturn = _mapper.Map<IEnumerable<UserResponseDto>>(result);
            return StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>.Success("Successfully retrieved all users", (usersToReturn, result.MetaData), 200);
        }
        public async Task<StandardResponse<UserResponseDto>> GetUserByIdAsync(int id)
        {
            var userById = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<UserResponseDto>(userById);
            return StandardResponse<UserResponseDto>.Success("Successfully retrieved a user", userToReturn, 200);
        }
        public async Task<StandardResponse<UserResponseDto>> GetUserByEmailAsync(string email)
        {
            var userByEmail = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            var userToReturn = _mapper.Map<UserResponseDto>(userByEmail);
            return StandardResponse<UserResponseDto>.Success("Successfully retrieved a user", userToReturn, 200);
        }
        public async Task<StandardResponse<UserResponseDto>> DeleteUserAsync(int id)
        {
            _logger.LogInformation($"Checking if the user with Id {id} exists");
            var user = await _unitOfWork.UserRepository.Delete(id);
            if (user == null)
            {
                _logger.LogError("User not found");
                return StandardResponse<UserResponseDto>.Failed("User does not exist");
            }
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success($"Successfully deleted {user.FirstName} {user.LastName}", userToReturn, 200);
        }
                    
        public async Task<StandardResponse<UserResponseDto>> UpdateUserAsync(int id, UserRequestDto userRequestDto)
        {
            var userExistsInDatabase = await _unitOfWork.UserRepository.Delete(id);
            if (userExistsInDatabase == null)
            {
                _logger.LogError("user not found");
                return StandardResponse<UserResponseDto>.Failed("User not found");
            }
            var user = _mapper.Map<User>(userRequestDto);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            var userToReturn = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success($"Successfully updated a user: {user.FirstName } {user.LastName}", userToReturn, 200);
        }
       /* public async Task<StandardResponse<ContactResponseDto>> SearchContactsByKeywordAsync(string keyword)
        {
            var searchResults = await _unitOfWork.UserRepository.GetUserBySearchKeywordAsync(keyword);

            if (searchResults == null)
            {
                return StandardResponse<ContactResponseDto>.Failed($"No contacts found with keyword '{keyword}'", 404);
            }

            var matchingUsers = searchResults
                .Where(users =>
                    users.FirstName.Contains(keyword) ||
                    users.LastName.Contains(keyword) ||
                    users.Email.Contains(keyword))
                .ToList();*/
            /* public Task UploadProfileImage(FileStream fileStream)
             {
                 throw new NotImplementedException();
             }*/

        }
}
