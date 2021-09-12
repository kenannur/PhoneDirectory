using System;
using System.Collections.Generic;
using Bogus;
using ContactApi.Shared.Entities;

namespace ContactApi.Data.Faker
{
    public static class FakeDataGenerator
    {
        private const string locale = "tr";

        public static List<Contact> Prepare(int count = 50)
        {
            Randomizer.Seed = new Random(8675309);

            var contactFaker = new Faker<Contact>(locale)
                .RuleFor(x => x.Id, Guid.NewGuid)
                .RuleFor(x => x.Name, x => x.Person.FirstName)
                .RuleFor(x => x.LastName, x => x.Person.LastName)
                .RuleFor(x => x.Firm, x => x.Person.Company.Name);

            return contactFaker.Generate(count);
        }
    }
}
