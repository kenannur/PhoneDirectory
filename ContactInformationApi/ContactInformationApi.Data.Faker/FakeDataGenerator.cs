using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Data.Faker
{
    public static class FakeDataGenerator
    {
        private const string locale = "tr";

        public static List<ContactInformation> PrepareForList(List<Guid> contactIds, int count = 3)
        {
            int seed = 8675309;
            Randomizer.Seed = new Random(seed);

            List<ContactInformation> allContactInformations = new List<ContactInformation>();
            foreach (var contactId in contactIds)
            {
                allContactInformations.AddRange(Prepare(contactId, count));
                seed++;
            }
            return allContactInformations;
        }

        public static List<ContactInformation> Prepare(Guid contactId, int count = 3)
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
                .Generate(count)
                .Where(x => x.Type != ContactType.Unknown)
                .ToList();
        }
    }
}
