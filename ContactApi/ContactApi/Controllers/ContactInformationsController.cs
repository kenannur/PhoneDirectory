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
    public class ContactInformationsController : ControllerBase
    {
        private readonly IRepository<ContactInformation> _repository;
        private readonly IMapper _mapper;

        public ContactInformationsController(IRepository<ContactInformation> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddContactInformationRequest request, CancellationToken cancellationToken)
        {
            if (request.Type == InformationType.Unknown)
            {
                return BadRequest("Type cannot be 0");
            }
            var entity = _mapper.Map<ContactInformation>(request);
            entity.Id = Guid.NewGuid();
            entity = await _repository.AddAsync(entity, cancellationToken);
            return Ok(entity);
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
