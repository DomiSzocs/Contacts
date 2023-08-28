using AutoMapper;
using backend.Data;
using backend.Data.DTOs.Request;
using backend.Data.DTOs.Response;

namespace backend.Profiles
{
    public class ContactProfile : Profile
    {

        public ContactProfile()
        {
            CreateMap<Contact, ContacDetailResponseDTO>();

            CreateMap<Contact, ContactShortResponseDTO>();

            CreateMap<ContactDetailRequestDTO, Contact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
 