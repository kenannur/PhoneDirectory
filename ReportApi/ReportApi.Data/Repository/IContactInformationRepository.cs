using System.Collections.Generic;
using ReportApi.Shared.Entities;

namespace ReportApi.Data.Repository
{
    public interface IContactInformationRepository : IRepository<ContactInformation>
    {
        IEnumerable<ContactInformation> GetLocationInformations();

        int GetPhoneNumbersCountAt(string location);
    }
}
