using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Faker
{
    public static class FakeDataGenerator
    {
        private const int contactCount = 10;
        private const int contactInformationCount = 2;
        private const string locale = "tr";

        public static (List<Contact>, List<ContactInformation>) Prepare()
        {
            Randomizer.Seed = new Random(8675309);

            var fakeContacts = GenerateFakeContacts();
            List<ContactInformation> fakeContactInformations = new();
            foreach (var contact in fakeContacts)
            {
                fakeContactInformations.AddRange(GenerateFakeContactInformations(contact.Id));
            }

            return (fakeContacts, fakeContactInformations);
        }

        private static List<Contact> GenerateFakeContacts()
        {
            var contactFaker = new Faker<Contact>(locale)
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.Name, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Firm, x => x.Person.Company.Name);

            return contactFaker.Generate(contactCount);
        }

        private static List<ContactInformation> GenerateFakeContactInformations(Guid contactId)
        {
            var contactInformationFaker = new Faker<ContactInformation>(locale)
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.ContactId, contactId)
                .RuleFor(x => x.Type, x => x.PickRandom<ContactType>())
                .RuleFor(x => x.Value, (f, c) =>
                {
                    if (c.Type == ContactType.PhoneNumber)
                    {
                        return f.Person.Phone;
                    }
                    else if (c.Type == ContactType.EmailAddress)
                    {
                        return f.Person.Email;
                    }
                    else if (c.Type == ContactType.Location)
                    {
                        return f.Person.Address.City;
                    }
                    else
                    {
                        return null;
                    }
                });

            return contactInformationFaker
                .Generate(contactInformationCount)
                .Where(x => x.Type != ContactType.Unknown)
                .ToList();
        }
    }
}
