using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ContactApi.Shared.Entities
{
    public class Contact : EntityBase
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual List<ContactInformation> Informations { get; set; }
    }
}
