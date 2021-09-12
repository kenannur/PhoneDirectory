namespace AggregatorApi.Settings
{
    public interface IExternalApiSettings
    {
        string ContactApi { get; set; }
        string ContactInformationApi { get; set; }
    }

    public class ExternalApiSettings : IExternalApiSettings
    {
        public string ContactApi { get; set; }
        public string ContactInformationApi { get; set; }
    }
}
