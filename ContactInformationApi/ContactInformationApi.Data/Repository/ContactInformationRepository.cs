using System.Collections.Generic;
using System.Linq;
using ContactInformationApi.Data.Context;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Data.Repository
{
    public class ContactInformationRepository : Repository<ContactInformation>, IContactInformationRepository
    {
        public ContactInformationRepository(AppDbContext context)
            : base(context)
        { }

        public IEnumerable<ContactInformation> GetLocationInformations()
        {
            return Context.ContactInformations
                .Where(x => x.Type == ContactType.Location)
                .ToList();
        }

        public int GetPhoneNumbersCountAt(string location)
        {
            var locationQuery = Context.ContactInformations
                .Where(x => x.Type == ContactType.Location && x.Value == location)
                .Select(x => x.ContactId);

            var phoneQuery = Context.ContactInformations
                .Where(x => x.Type == ContactType.PhoneNumber && locationQuery.Contains(x.ContactId));

            var result = phoneQuery.Count();
            return result;
        }
    }
}
