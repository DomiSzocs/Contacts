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
            CreateMap<Contact, ContactResponseDTO>();

            CreateMap<ContactRequestDTO, Contact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
 