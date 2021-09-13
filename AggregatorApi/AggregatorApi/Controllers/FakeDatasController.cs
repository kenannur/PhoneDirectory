using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.HttpClients;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FakeDatasController : ControllerBase
    {
        private readonly IContactHttpClient _contactHttpClient;
        private readonly IContactInformationHttpClient _contactInformationHttpClient;

        public FakeDatasController(IContactHttpClient contactHttpClient, IContactInformationHttpClient contactInformationHttpClient)
        {
            _contactHttpClient = contactHttpClient;
            _contactInformationHttpClient = contactInformationHttpClient;
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> SeedAsync(CancellationToken cancellationToken)
        {
            var fakeContactIds = await _contactHttpClient.SeedAsync(cancellationToken);
            _ = await _contactInformationHttpClient.SeedAsync(fakeContactIds, cancellationToken);

            return Ok();
        }
    }
}
