using System;
using System.ComponentModel.DataAnnotations;

namespace AggregatorApi.Models
{
    public class UpdateContactRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
