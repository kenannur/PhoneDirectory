using System;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.HttpClients;
using AggregatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactInformationsController : ControllerBase
    {
        private readonly IContactInformationHttpClient _contactInformationHttpClient;

        public ContactInformationsController(IContactInformationHttpClient contactInformationHttpClient)
        {
            _contactInformationHttpClient = contactInformationHttpClient;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddContactInformationRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactInformationHttpClient.PostAsync(request, cancellationToken);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactInformationHttpClient.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound(id);
            }
            return Ok();
        }
    }
}
