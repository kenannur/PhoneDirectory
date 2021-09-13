using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.Models;
using AggregatorApi.Models.Response;
using AggregatorApi.Settings;
using AggregatorApi.Shared.Extensions;

namespace AggregatorApi.HttpClients
{
    public class ContactHttpClient : IContactHttpClient
    {
        private readonly HttpClient _httpClient;

        public ContactHttpClient(HttpClient httpClient, IExternalApiSettings externalApiSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(externalApiSettings.ContactApi);
        }

        public async Task<string> GetAllAsync(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync("Contacts", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result;
            }
            return null;
        }

        public async Task<ContactResponse> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"Contacts/{id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result.ToObject<ContactResponse>();
            }
            return null;
        }

        public async Task<string> PostAsync(AddContactRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("Contacts", content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result;
            }
            return null;
        }

        public async Task<string> PutAsync(UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PutAsync("Contacts", content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"Contacts/{id}", cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Guid>> SeedAsync(CancellationToken cancellationToken)
        {
            var content = new StringContent(new
            {
                Count = 50
            }.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync("Contacts/SeedFakeData", content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var resultStr = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = resultStr.ToObject<IEnumerable<Guid>>();
                return result;
            }
            return null;
        }
    }
}
