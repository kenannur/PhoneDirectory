using System;
using System.Text;
using ContactApi.Messaging.Producer.Settings;
using ContactApi.Shared.Extensions;
using RabbitMQ.Client;

namespace ContactApi.Messaging.Producer.Client
{
    public class QueueProducer : IQueueProducer
    {
        private readonly IRabbitMqSettings _settings;
        private IConnection _connection;

        public QueueProducer(IRabbitMqSettings settings)
        {
            _settings = settings;
            CreateConnection();
        }

        public bool DeleteContactInformations(Guid contactId)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(new
                {
                    ContactId = contactId
                }.ToJson());

                channel.BasicPublish(exchange: string.Empty, routingKey: _settings.QueueName, basicProperties: null, body: body);
                return true;
            }
            return false;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _settings.Hostname,
                    UserName = _settings.Username,
                    Password = _settings.Password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection is not null && _connection.IsOpen)
            {
                return true;
            }

            CreateConnection();
            return _connection is not null;
        }
    }
}
