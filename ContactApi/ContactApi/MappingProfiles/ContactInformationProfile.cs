using AutoMapper;
using ContactApi.Models.Request;
using ContactApi.Shared.Entities;

namespace ContactApi.MappingProfiles
{
    public class ContactInformationProfile : Profile
    {
        public ContactInformationProfile()
        {
            CreateMap<AddContactInformationRequest, ContactInformation>();
        }
    }
}
