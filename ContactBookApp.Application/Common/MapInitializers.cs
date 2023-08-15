using AutoMapper;
using ContactBookApp.Domain.Dtos.Requests;
using ContactBookApp.Domain.Dtos.Responses;
using ContactBookApp.Domain.Entities;

namespace ContactBookApp.Application.Common
{
    public class MapInitializers : Profile
    {
        public MapInitializers()
        {
            CreateMap<UserRequestDto, User>();
            CreateMap<User, UserResponseDto>();
            CreateMap<Contact, ContactResponseDto>();
            CreateMap<ContactRequestDto, Contact>();
        }
    }
}
