using System.Text;
using NMeter.App.Runner.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NMeter.App.Runner.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly ILogger<MessageBusSubscriber> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private readonly string EXCHANGE_NAME = "trigger";

        public MessageBusSubscriber(ILogger<MessageBusSubscriber> logger,
            IConfiguration configuration,
            IEventProcessor eventProcessor)
        {
            _logger = logger;
            _configuration = configuration;
            _eventProcessor = eventProcessor;

            InitRabbitMQ();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (moduleHandle, ea) =>
            {
                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                _eventProcessor.ProcessEvent(notificationMessage);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _logger.LogInformation("--> Disposing connection...");

            if (_channel != null && _channel.IsOpen)
            {
                _logger.LogInformation("--> Event received");

                _channel.Close();
                _connection?.Close();
            }
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);

            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: EXCHANGE_NAME,
                routingKey: "");

            _logger.LogInformation("--> Listening on the message bus...");



        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("--> Shutting down connection...");
        }
    }
}