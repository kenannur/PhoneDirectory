namespace ContactApi.Messaging.Producer.Settings
{
    public interface IRabbitMqSettings
    {
        string Hostname { get; set; }
        string QueueName { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }

    public class RabbitMqSettings : IRabbitMqSettings
    {
        public string Hostname { get; set; }
        public string QueueName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
