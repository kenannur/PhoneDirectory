using AutoMapper;
using ContactApi.Models.Request;
using ContactApi.Shared.Entities;

namespace ContactApi.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<AddContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>();
        }
    }
}
