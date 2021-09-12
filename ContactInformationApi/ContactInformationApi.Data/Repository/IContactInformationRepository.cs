using System.Collections.Generic;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Data.Repository
{
    public interface IContactInformationRepository : IRepository<ContactInformation>
    {
        IEnumerable<ContactInformation> GetLocationInformations();

        int GetPhoneNumbersCountAt(string location);
    }
}
