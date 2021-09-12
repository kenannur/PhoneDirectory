using System;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Models.Response
{
    public class AddContactInformationResponse
    {
        public Guid Id { get; set; }

        public Guid ContactId { get; set; }

        public ContactType Type { get; set; }

        public string Value { get; set; }
    }
}
