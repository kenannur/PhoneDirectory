using System;

namespace ContactApi.Models.Response
{
    public class GetContactResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
