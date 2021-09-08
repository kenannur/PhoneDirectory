using System;
using System.Threading;
using System.Threading.Tasks;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Repository
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<Contact> GetContactWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
