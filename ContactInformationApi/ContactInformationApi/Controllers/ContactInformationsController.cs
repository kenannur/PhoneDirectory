using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactInformationApi.Data.Faker;
using ContactInformationApi.Data.Repository;
using ContactInformationApi.Models;
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

        [HttpGet("{contactId}")]
        public async Task<IActionResult> Get(Guid contactId, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetContactInformationsAsync(contactId);
            return Ok(entities);
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

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReportAsync(CancellationToken cancellationToken)
        {
            var locationInformations = await _repository.GetLocationInformationsAsync();

            var orderedLocationGroups = locationInformations.GroupBy(x => x.Value)
                                                            .OrderByDescending(x => x.Count())
                                                            .ToList();

            var orderedLocations = orderedLocationGroups.Select(x => x.Key);

            var registeredPeoples = orderedLocationGroups.Select(x => new RegisteredPeopleInfo
            {
                Location = x.Key,
                Count = x.Count()
            });

            var registeredPhones = orderedLocationGroups.Select(x => new RegisteredPhoneInfo
            {
                Location = x.Key,
                Count = _repository.GetPhoneNumbersCountAt(x.Key)
            });

            var report = new Report
            {
                LocationInformationFromMostToLeast = orderedLocations.ToList(),
                NumberOfPeopleRegisteredAt = registeredPeoples.ToList(),
                NumberOfPhoneRegisteredAt = registeredPhones.ToList()
            };

            return Ok(report);
        }

        [HttpPost("SeedFakeData")]
        public async Task<IActionResult> SeedFakeDataAsync([FromBody] SeedFakeDataRequest request)
        {
            var fakeContactInformas = FakeDataGenerator.PrepareForList(request.ContactIds);
            await _repository.AddRangeAsync(fakeContactInformas);
            return Ok(fakeContactInformas.Select(x => x.Id));
        }
    }
}
