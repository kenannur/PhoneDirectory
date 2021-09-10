using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContactApi.Controllers;
using ContactApi.Data.Faker;
using ContactApi.Data.Repository;
using ContactApi.MappingProfiles;
using ContactApi.Models.Request;
using ContactApi.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContactApi.Test
{
    public class ContactsController_Test
    {
        private readonly Mock<IContactRepository> _mockOfContactRepository;
        private readonly IMapper _mapper;

        public ContactsController_Test()
        {
            var contactProfile = new ContactProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(contactProfile));
            _mapper = new Mapper(configuration);

            _mockOfContactRepository = new Mock<IContactRepository>();
        }

        [Fact]
        public async Task ContacstController_GetAllAsync()
        {
            CancellationTokenSource cts = new();

            _mockOfContactRepository.Setup(x => x.GetAllAsync(cts.Token))
                                    .Returns(GetFakeContacts());

            var contactsController = new ContactsController(_mockOfContactRepository.Object, _mapper);
            var actionResult = await contactsController.GetAllAsync(cts.Token);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public async Task ContacstController_GetAsync()
        {
            CancellationTokenSource cts = new();
            Guid validId = new("05eef6d2-4459-49d4-ae4a-9eede80a3337");
            Guid invalidId = new("05eef6d2-4459-49d4-ae4a-9eede80a3336");
            _mockOfContactRepository.Setup(x => x.GetAsync(validId, cts.Token))
                                    .Returns(GetFakeContact());

            _mockOfContactRepository.Setup(x => x.GetAsync(invalidId, cts.Token))
                                    .Returns(GetNullContact());

            var contactsController = new ContactsController(_mockOfContactRepository.Object, _mapper);
            var validActionResult = await contactsController.GetAsync(validId, cts.Token);
            Assert.IsType<OkObjectResult>(validActionResult);

            var invalidActionResult = await contactsController.GetAsync(invalidId, cts.Token);
            Assert.IsType<NotFoundObjectResult>(invalidActionResult);
            Assert.Equal((invalidActionResult as NotFoundObjectResult).Value, invalidId);
        }

        [Fact]
        public async Task ContacstController_PostAsync()
        {
            CancellationTokenSource cts = new();

            _mockOfContactRepository.Setup(x => x.AddAsync(It.IsAny<Contact>(), cts.Token))
                                    .Returns(GetFakeContact());

            var contactsController = new ContactsController(_mockOfContactRepository.Object, _mapper);
            var actionResult = await contactsController.PostAsync(GetContactRequest(), cts.Token);
            Assert.IsType<OkObjectResult>(actionResult);
        }

        private static Task<IEnumerable<Contact>> GetFakeContacts()
            => Task.FromResult(FakeDataGenerator.Prepare().Item1.AsEnumerable());
        
        private static Task<Contact> GetFakeContact()
            => Task.FromResult(FakeDataGenerator.Prepare().Item1.FirstOrDefault());
        
        private static Task<Contact> GetNullContact()
            => Task.FromResult<Contact>(null);

        private static AddContactRequest GetContactRequest() => new AddContactRequest
        {
            Name = "nnn",
            LastName = "lll",
            Firm = "fff",
        };
        
    }
}
