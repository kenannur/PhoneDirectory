using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactApi.Data.Faker;
using ContactApi.Data.Repository;
using ContactApi.Messaging.Producer.Client;
using ContactApi.Models.Request;
using ContactApi.Models.Response;
using ContactApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;
        private readonly IMapper _mapper;
        private readonly IQueueProducer _queueProducer;

        public ContactsController(IContactRepository repository, IMapper mapper, IQueueProducer queueProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _queueProducer = queueProducer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            return Ok(_mapper.Map<List<GetContactResponse>>(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound(id);
            }
            return Ok(_mapper.Map<GetContactResponse>(result));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddContactRequest request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            contact.Id = Guid.NewGuid();
            contact = await _repository.AddAsync(contact, cancellationToken);
            return Ok(_mapper.Map<AddContactResponse>(contact));
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            contact = await _repository.UpdateAsync(contact, cancellationToken);
            return Ok(_mapper.Map<UpdateContactResponse>(contact));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var contact = await _repository.GetAsync(id, cancellationToken);
            if (contact is null)
            {
                return NotFound(id);
            }
            await _repository.DeleteAsync(contact, cancellationToken);
            _queueProducer.DeleteContactInformations(id);
            return Ok();
        }

        [HttpPost("SeedFakeData")]
        public async Task<IActionResult> SeedFakeDataAsync([FromBody] SeedFakeDataRequest request)
        {
            var fakeContacts = FakeDataGenerator.Prepare(request.Count);
            await _repository.AddRangeAsync(fakeContacts);

            return Ok(fakeContacts.Select(x => x.Id));
        }
    }
}
