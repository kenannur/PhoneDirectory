using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactApi.Data.Repository;
using ContactApi.Messaging.Producer.Client;
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
        private readonly IQueueProducer _queueProducer;

        public ContactInformationsController(IRepository<ContactInformation> repository, IMapper mapper, IQueueProducer queueProducer)
        {
            _repository = repository;
            _mapper = mapper;
            _queueProducer = queueProducer;
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

        [HttpPost("CreateReport")]
        public IActionResult CreateReport()
        {
            var reportRequestId = Guid.NewGuid().ToString();
            if (_queueProducer.SendReportRequest(reportRequestId))
            {
                return Ok($"Your report request queued. Report Name = {reportRequestId}");
            }
            return StatusCode(500, "Could not connect to ReportQueue. Please try again later");
        }
    }
}
