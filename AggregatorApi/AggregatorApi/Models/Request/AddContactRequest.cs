using System.ComponentModel.DataAnnotations;

namespace AggregatorApi.Models
{
    public class AddContactRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
