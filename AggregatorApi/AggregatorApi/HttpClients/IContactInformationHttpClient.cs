using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.Models;
using AggregatorApi.Models.Response;

namespace AggregatorApi.HttpClients
{
    public interface IContactInformationHttpClient
    {
        Task<IEnumerable<ContactInformationsResponse>> GetAsync(Guid contactId, CancellationToken cancellationToken);

        Task<string> PostAsync(AddContactInformationRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task<string> CreateReportAsync(string id, CancellationToken cancellationToken);

        Task<IEnumerable<Guid>> SeedAsync(IEnumerable<Guid> contactIds, CancellationToken cancellationToken);
    }
}
