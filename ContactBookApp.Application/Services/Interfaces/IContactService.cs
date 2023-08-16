using ContactBook.Shared.RequestParameter.Common;
using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Domain.Dtos.Requests;
using ContactBookApp.Domain.Dtos.Responses;

namespace ContactBookApp.Application.Services.Interfaces
{
    public interface IContactService
    {
        Task<StandardResponse<ContactResponseDto>> CreateContactAsync(ContactRequestDto contactRequestDto);
        Task<StandardResponse<(IEnumerable<ContactResponseDto>, MetaData pagingData)>> GetAllContactAsync(ContactRequestInputParameter parameter);
        Task<StandardResponse<ContactResponseDto>> GetContactByIdAsync(string id);
        Task<StandardResponse<ContactResponseDto>> GetContactByEmailAsync(string email);
        Task<StandardResponse<ContactResponseDto>> DeleteContactAsync(string id);
        Task<StandardResponse<ContactResponseDto>> UpdateContactAsync(string id, ContactRequestDto contactRequestDto);
    }
}
