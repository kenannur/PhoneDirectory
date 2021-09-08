using System;
using System.Threading;
using System.Threading.Tasks;
using ContactApi.Data.Context;
using ContactApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactApi.Data.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext context)
            : base(context)
        { }

        public async Task<Contact> GetContactWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Context.Contacts
                                .Include(x => x.Informations)
                                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
