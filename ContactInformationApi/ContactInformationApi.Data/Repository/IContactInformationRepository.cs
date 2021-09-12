using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Data.Repository
{
    public interface IContactInformationRepository : IRepository<ContactInformation>
    {
        Task<IEnumerable<ContactInformation>> GetContactInformationsAsync(Guid contactId);

        Task<IEnumerable<ContactInformation>> GetLocationInformationsAsync();

        int GetPhoneNumbersCountAt(string location);

        void DeleteContactInformations(Guid contactId);
    }
}
