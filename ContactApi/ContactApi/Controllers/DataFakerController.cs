using System;
using System.Threading;
using System.Threading.Tasks;
using ContactApi.Data.Faker;
using ContactApi.Data.Repository;
using ContactApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ContactApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataFakerController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IRepository<ContactInformation> _repository;

        public DataFakerController(IContactRepository contactRepository, IRepository<ContactInformation> repository)
        {
            _contactRepository = contactRepository;
            _repository = repository;
        }

        [HttpPost("Seed")]
        public async Task<IActionResult> PostAsync(CancellationToken cancellationToken)
        {
            var (contacts, contactInfos) = FakeDataGenerator.Prepare();
            foreach (var item in contacts)
            {
                await _contactRepository.AddAsync(item, cancellationToken);
            }

            foreach (var item in contactInfos)
            {
                await _repository.AddAsync(item, cancellationToken);
            }

            return Ok();
        }
    }
}
