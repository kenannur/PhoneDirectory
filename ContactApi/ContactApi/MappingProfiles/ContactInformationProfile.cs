using AutoMapper;
using ContactApi.Models.Request;
using ContactApi.Models.Response;
using ContactApi.Shared.Entities;

namespace ContactApi.MappingProfiles
{
    public class ContactInformationProfile : Profile
    {
        public ContactInformationProfile()
        {
            CreateMap<AddContactInformationRequest, ContactInformation>();
            CreateMap<ContactInformation, AddContactInformationResponse>();
        }
    }
}
