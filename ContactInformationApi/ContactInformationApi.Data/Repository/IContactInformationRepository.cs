using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Data.Repository
{
    public interface IContactInformationRepository : IRepository<ContactInformation>
    {
        Task<IEnumerable<ContactInformation>> GetContactInformationsAsync(Guid contactId);

        IEnumerable<ContactInformation> GetLocationInformations();

        int GetPhoneNumbersCountAt(string location);
    }
}
