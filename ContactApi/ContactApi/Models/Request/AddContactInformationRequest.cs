using System;
using System.ComponentModel.DataAnnotations;
using ContactApi.Shared.Entities;

namespace ContactApi.Models.Request
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
}
