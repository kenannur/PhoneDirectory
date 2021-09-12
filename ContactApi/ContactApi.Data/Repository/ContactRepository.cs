using ContactApi.Data.Context;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context)
            : base(context)
        { }

    }
}
