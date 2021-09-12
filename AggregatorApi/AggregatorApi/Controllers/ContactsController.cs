using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AggregatorApi.HttpClients;
using AggregatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactHttpClient _contactHttpClient;
        private readonly IContactInformationHttpClient _contactInformationHttpClient;

        public ContactsController(IContactHttpClient contactHttpClient, IContactInformationHttpClient contactInformationHttpClient)
        {
            _contactHttpClient = contactHttpClient;
            _contactInformationHttpClient = contactInformationHttpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _contactHttpClient.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}/WithDetail")]
        public async Task<IActionResult> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
        {
            var contact = await _contactHttpClient.GetAsync(id, cancellationToken);
            if (contact is null)
            {
                return NotFound(id);
            }
            var contactInformations = await _contactInformationHttpClient.GetAsync(id, cancellationToken);
            contact.Informations = contactInformations.ToList();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddContactRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactHttpClient.PostAsync(request, cancellationToken);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var result = await _contactHttpClient.PutAsync(request, cancellationToken);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _contactHttpClient.DeleteAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound(id);
            }
            return Ok();
        }
    }
}
