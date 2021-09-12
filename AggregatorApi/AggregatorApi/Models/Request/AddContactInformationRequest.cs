using System;
using System.ComponentModel.DataAnnotations;

namespace AggregatorApi.Models
{
    public class AddContactInformationRequest
    {
        [Required]
        public Guid ContactId { get; set; }

        [Required]
        public ContactType Type { get; set; }

        [Required]
        public string Value { get; set; }
    }

    public enum ContactType
    {
        Unknown = 0,
        PhoneNumber = 1,
        EmailAddress = 2,
        Location = 3
    }
}
