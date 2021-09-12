using System;
using System.Collections.Generic;

namespace AggregatorApi.Models.Response
{
    public class ContactResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        public List<ContactInformationsResponse> Informations { get; set; }
    }
}
