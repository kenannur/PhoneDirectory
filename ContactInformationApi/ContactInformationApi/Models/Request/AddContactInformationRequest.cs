using System;
using System.ComponentModel.DataAnnotations;
using ContactInformationApi.Shared.Entities;

namespace ContactInformationApi.Models.Request
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
