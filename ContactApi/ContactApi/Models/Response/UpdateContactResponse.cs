using System;

namespace ContactApi.Models.Response
{
    public class UpdateContactResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
