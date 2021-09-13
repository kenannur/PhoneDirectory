using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApi.Data.Context;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context)
            : base(context)
        { }

        public async Task AddRangeAsync(IEnumerable<Contact> contacts)
        {
            await Context.Contacts.AddRangeAsync(contacts);
            await Context.SaveChangesAsync();
        }
    }
}
