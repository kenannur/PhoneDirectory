using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactInformationApi.Data.Repository;
using ContactInformationApi.Messaging.Consumer.Models;
using ContactInformationApi.Messaging.Consumer.Settings;
using ContactInformationApi.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ContactInformationApi.Messaging.Consumer.Client
{
    public class QueueConsumer : BackgroundService
    {
        private readonly IRabbitMqSettings _settings;
        private readonly IServiceScopeFactory _ssFactory;
        private IModel _channel;
        private IConnection _connection;

        public QueueConsumer(IRabbitMqSettings settings, IServiceScopeFactory ssFactory)
        {
            _settings = settings;
            _ssFactory = ssFactory;
            InitializeRabbitMqListener();
        }

        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.Hostname,
                UserName = _settings.Username,
                Password = _settings.Password
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _settings.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (obj, eventArgs) =>
            {
                var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var request = body.ToObject<DeleteContactInformationRequest>();

                ConsumeEvent(request);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(_settings.QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private void ConsumeEvent(DeleteContactInformationRequest request)
        {
            using var scope = _ssFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<IContactInformationRepository>();

            repository.DeleteContactInformations(request.ContactId);
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
