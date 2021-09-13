using System;
using System.Collections.Generic;

namespace ContactInformationApi.Models.Request
{
    public class SeedFakeDataRequest
    {
        public List<Guid> ContactIds { get; set; }

        public int Count { get; set; }
    }
}
