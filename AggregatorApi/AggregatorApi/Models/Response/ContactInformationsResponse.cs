using System;

namespace AggregatorApi.Models.Response
{
    public class ContactInformationsResponse
    {
        public Guid Id { get; set; }

        public Guid ContactId { get; set; }

        public ContactType Type { get; set; }

        public string Value { get; set; }
    }
}
