using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.Models;
using AggregatorApi.Models.Response;

namespace AggregatorApi.HttpClients
{
    public interface IContactHttpClient
    {
        Task<string> GetAllAsync(CancellationToken cancellationToken);

        Task<ContactResponse> GetAsync(Guid id, CancellationToken cancellationToken);

        Task<string> PostAsync(AddContactRequest request, CancellationToken cancellationToken);

        Task<string> PutAsync(UpdateContactRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Guid>> SeedAsync(CancellationToken cancellationToken);
    }
}
