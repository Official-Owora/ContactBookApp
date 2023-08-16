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
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactService> _logger;
        private readonly IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, ILogger<ContactService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<StandardResponse<ContactResponseDto>> CreateContactAsync(ContactRequestDto contactRequestDto)
        {
            if (contactRequestDto == null)
            {
                _logger.LogError("Contact details cannot be null");
                return StandardResponse<ContactResponseDto>.Failed("Contact Request is null");
            }
            _logger.LogInformation($"Trying to create a user: {DateTime.Now}");
            var contact = _mapper.Map<Contact>(contactRequestDto);
            _logger.LogInformation($"Contact successfully created: {DateTime.Now}");
            //save to database
            _unitOfWork.ContactRepository.CreateAsync(contact);
            await _unitOfWork.SaveAsync();
            _logger.LogInformation($"Contact {contact.Id} successfully saved");
            //Mapping a the saved data to frontend
            var contactToReturn = _mapper.Map<ContactResponseDto>(contact);
            return StandardResponse<ContactResponseDto>.Success($"Successfully created a Contact {contact.Id}", contactToReturn, 201);
        }
        public async Task<StandardResponse<(IEnumerable<ContactResponseDto>, MetaData)>> GetAllContactAsync(ContactRequestInputParameter parameter)
        {
            var result = await _unitOfWork.ContactRepository.GetAllContactAsync(parameter);
            var contactToReturn = _mapper.Map<IEnumerable<ContactResponseDto>>(result);
            return StandardResponse<(IEnumerable<ContactResponseDto>, MetaData)>.Success("All contacts successfully retrieved",(contactToReturn, result.MetaData), 200);
        }
        public async Task<StandardResponse<ContactResponseDto>> GetContactByIdAsync(int id)
        {
            var getContactById = await _unitOfWork.ContactRepository.GetContactByIdAsync(id);
            var contactToReturn = _mapper.Map<ContactResponseDto>(getContactById);
            return StandardResponse<ContactResponseDto>.Success($"Successfully retrieved a contact {getContactById.Id}", contactToReturn, 200);
        }
        public async Task<StandardResponse<ContactResponseDto>> GetContactByEmailAsync(string email)
        {
            var getByEmail = await _unitOfWork.ContactRepository.GetContactByEmailAsync(email);
            var contactToReturn = _mapper.Map<ContactResponseDto>(getByEmail);
            return StandardResponse<ContactResponseDto>.Success($"Successfully retrieved a contact {getByEmail.Id}", contactToReturn , 200);
        }
        public async Task<StandardResponse<ContactResponseDto>> DeleteContactAsync(int id)
        {
            _logger.LogInformation($"Checking if the user with Id {id} exists");
            var contactToBeDeleted = await _unitOfWork.ContactRepository.GetContactByIdAsync(id);
            if(contactToBeDeleted == null)
            {
                _logger.LogError("Contact not found");
                return StandardResponse<ContactResponseDto>.Failed("Contact does not exist");
            }
            _unitOfWork.ContactRepository.Delete(contactToBeDeleted);
            await _unitOfWork.SaveAsync();
            var contactToReturn = _mapper.Map<ContactResponseDto>(contactToBeDeleted);
            return StandardResponse<ContactResponseDto>.Success($"Successfully deleted a contact {contactToBeDeleted.FirstName } {contactToBeDeleted.LastName}", contactToReturn, 200);
                
        }
       /* public async Task<StandardResponse<List<ContactResponseDto>>> SearchContactsByKeywordAsync(string keyword)
        {
            var searchResults = await _unitOfWork.ContactRepository.SearchContactsByKeywordAsync(keyword);

            if (searchResults == null || searchResults.Count == 0)
            {
                return StandardResponse<List<ContactResponseDto>>.Failed($"No contacts found with keyword '{keyword}'", 404);
            }

            var matchingContacts = searchResults
                .Where(contact =>
                    contact.FirstName.Contains(keyword) ||
                    contact.LastName.Contains(keyword) ||
                    contact.Email.Contains(keyword))
                .ToList();

            if (matchingContacts.Count == 0)
            {
                return StandardResponse<List<ContactResponseDto>>.Failed($"No contacts found with keyword '{keyword}'", matchingContacts, 404);
            }

            var contactsToReturn = _mapper.Map<List<ContactResponseDto>>(matchingContacts);
            return StandardResponse<List<ContactResponseDto>>.Success($"Successfully retrieved contacts with keyword '{keyword}'", contactsToReturn, 200);
        }*/

        public async Task<StandardResponse<ContactResponseDto>> UpdateContactAsync(int id, ContactRequestDto contactRequestDto)
        {
            var checkContactExists = await _unitOfWork.ContactRepository.GetContactByIdAsync(id);
            if (checkContactExists == null)
            {
                _logger.LogError("Contact does not exist");
                return StandardResponse<ContactResponseDto>.Failed("Contact does not exist");
            }
            var contact = _mapper.Map<Contact>(contactRequestDto);
            _unitOfWork.ContactRepository.Update(contact);
            await _unitOfWork.SaveAsync();
            var contactUpdated = _mapper.Map<ContactResponseDto>(contact);
            return StandardResponse<ContactResponseDto>.Success($"Successfully updated a contact {contact.FirstName } {contact.LastName}", contactUpdated, 200);
        }
    }
}
