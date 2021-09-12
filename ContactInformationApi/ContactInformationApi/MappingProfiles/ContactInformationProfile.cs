using AutoMapper;
using ContactInformationApi.Models.Request;
using ContactInformationApi.Models.Response;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.MappingProfiles
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
