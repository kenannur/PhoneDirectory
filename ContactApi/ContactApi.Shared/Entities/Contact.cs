namespace ContactApi.Shared.Entities
{
    public class Contact : EntityBase
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Firm { get; set; }
    }
}
