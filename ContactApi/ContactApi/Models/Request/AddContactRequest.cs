using System.ComponentModel.DataAnnotations;

namespace ContactApi.Models.Request
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
