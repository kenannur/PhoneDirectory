using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Repository
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task AddRangeAsync(IEnumerable<Contact> contacts);
    }
}
