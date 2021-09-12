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
    public class ContactInformationHttpClient : IContactInformationHttpClient
    {
        private readonly HttpClient _httpClient;
        private const string requestBaseUri = "ContactInformations";

        public ContactInformationHttpClient(HttpClient httpClient, IExternalApiSettings externalApiSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(externalApiSettings.ContactInformationApi);
        }

        public async Task<IEnumerable<ContactInformationsResponse>> GetAsync(Guid contactId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{requestBaseUri}/{contactId}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var resultStr = await response.Content.ReadAsStringAsync(cancellationToken);
                var result = resultStr.ToObject<IEnumerable<ContactInformationsResponse>>();
                return result;
            }
            return null;
        }

        public async Task<string> PostAsync(AddContactInformationRequest request, CancellationToken cancellationToken)
        {
            var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync(requestBaseUri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"{requestBaseUri}/{id}", cancellationToken);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> CreateReportAsync(string id, CancellationToken cancellationToken)
        {
            var request = new { Id = id };
            var content = new StringContent(request.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await _httpClient.PostAsync($"{requestBaseUri}/CreateReport", content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync(cancellationToken);
                return result;
            }
            return null;
        }
    }
}
