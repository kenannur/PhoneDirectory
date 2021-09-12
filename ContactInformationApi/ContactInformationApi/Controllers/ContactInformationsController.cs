using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactInformationApi.Data.Repository;
using ContactInformationApi.Models.Request;
using ContactInformationApi.Models.Response;
using ContactInformationApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactInformationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactInformationsController : ControllerBase
    {
        private readonly IContactInformationRepository _repository;
        private readonly IMapper _mapper;

        public ContactInformationsController(IContactInformationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddContactInformationRequest request, CancellationToken cancellationToken)
        {
            if (request.Type == ContactType.Unknown)
            {
                return BadRequest("Type cannot be 0");
            }
            var entity = _mapper.Map<ContactInformation>(request);
            entity.Id = Guid.NewGuid();
            entity = await _repository.AddAsync(entity, cancellationToken);
            return Ok(_mapper.Map<AddContactInformationResponse>(entity));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);
            if (entity is null)
            {
                return NotFound(id);
            }
            await _repository.DeleteAsync(entity, cancellationToken);
            return Ok();
        }
    }
}
