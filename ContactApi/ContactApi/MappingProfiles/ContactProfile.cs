using AutoMapper;
using ContactApi.Models.Request;
using ContactApi.Models.Response;
using ContactApi.Shared.Entities;

namespace ContactApi.MappingProfiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<AddContactRequest, Contact>();
            CreateMap<UpdateContactRequest, Contact>();

            CreateMap<Contact, AddContactResponse>();
            CreateMap<Contact, UpdateContactResponse>();
            CreateMap<Contact, GetContactResponse>();
        }
    }
}
