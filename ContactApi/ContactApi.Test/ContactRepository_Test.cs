using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ContactApi.Data.Context;
using ContactApi.Data.Faker;
using ContactApi.Data.Repository;
using ContactApi.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ContactApi.Test
{
    public class ContactRepository_Test
    {
        public ContactRepository_Test()
        { }

        private static DbContextOptions<AppDbContext> GetDbContextOptions() =>
            new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "PhoneDirectory").Options;

        [Fact]
        public async Task ContactRepository_GetAllAsync_GetContactWithDetailsAsync()
        {
            var context = new AppDbContext(GetDbContextOptions());
            var (fakeContacts, _) = FakeDataGenerator.Prepare();
            await context.AddRangeAsync(fakeContacts);
            await context.SaveChangesAsync();

            var contactRepository = new ContactRepository(context);
            var result = await contactRepository.GetAllAsync();

            Assert.Equal(fakeContacts.Count, result.Count());

            var contactWithDetails = await contactRepository.GetContactWithDetailsAsync(fakeContacts.FirstOrDefault().Id);
            Assert.NotNull(contactWithDetails);
            Assert.NotNull(contactWithDetails.Informations);
        }

        [Fact]
        public async Task Repository_AddAsync_UpdateAsync_GetAsync_DeleteAsync()
        {
            CancellationTokenSource cts = new();

            using var context = new AppDbContext(GetDbContextOptions());
            var contactRepository = new ContactRepository(context);
            var id = Guid.NewGuid();
            var name = "Ali";
            var entity = new Contact
            {
                Id = id,
                Name = name,
                LastName = "Veli",
                Firm = "Apple"
            };
            var addedResult = await contactRepository.AddAsync(entity, cts.Token);
            Assert.True(addedResult is not null && entity.Name == addedResult.Name);

            var newFirm = "Microsoft";
            addedResult.Firm = newFirm;
            var updatedResult = await contactRepository.UpdateAsync(addedResult, cts.Token);
            Assert.True(updatedResult is not null && updatedResult.Firm == newFirm);

            var getResult = await contactRepository.GetAsync(entity.Id, cts.Token);
            Assert.True(getResult is not null && getResult.Name == name);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await contactRepository.DeleteAsync(null, cts.Token);
            });

            var deleteTask = contactRepository.DeleteAsync(getResult, cts.Token);
            await Task.WhenAll(deleteTask);
            Assert.True(deleteTask.IsCompleted);
        }
    }
}
