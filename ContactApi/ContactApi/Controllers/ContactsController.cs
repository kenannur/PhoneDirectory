using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactApi.Data.Repository;
using ContactApi.Models.Request;
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

        public ContactsController(IContactRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _repository.GetContactWithDetailsAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound(id);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddContactRequest request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            contact.Id = Guid.NewGuid();
            contact = await _repository.AddAsync(contact, cancellationToken);
            return Ok(contact);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateContactRequest request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<Contact>(request);
            contact = await _repository.UpdateAsync(contact, cancellationToken);
            return Ok(contact);
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
            return Ok();
        }
    }
}
