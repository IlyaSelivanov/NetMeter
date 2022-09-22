using System.Text;
using System.Text.Json;
using NMeter.Api.Settings.Models;
using RabbitMQ.Client;

namespace NMeter.Api.Settings.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly ILogger<MessageBusClient> _logger;
        private readonly IConfiguration _configuration;
        private readonly string EXCHANGE_NAME = "trigger";
        private IConnection _connection;
        private IModel _channel;

        public MessageBusClient(ILogger<MessageBusClient> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            InitConnection();
        }

        public void PublishPlanExecution(PlanExecution planExecution)
        {
            var message = JsonSerializer.Serialize(planExecution, options: new JsonSerializerOptions()
                {
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                });
            if (_connection.IsOpen)
            {
                _logger.LogInformation("--> RqbbitMQ connection is open, sending a message.");
                SendMessage(message);
            }
        }

        public void Dispose()
        {
            _logger.LogInformation("--> Disposing MessageBusClient.");
            if (_connection.IsOpen && _channel != null)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void InitConnection()
        {
            _logger.LogInformation("--> Trying establish RabbitMQ connection.");

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _connection.ConnectionShutdown += RabbitMQ_ShutDownConnection;

                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: EXCHANGE_NAME, type: ExchangeType.Fanout);

                _logger.LogInformation("--> RabbitMQ connection established.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Couldn't connect to RabbitMQ bus: {ex.Message}");
            }
        }

        private void RabbitMQ_ShutDownConnection(object sender, ShutdownEventArgs e)
        {
            _logger.LogInformation("--> RabbitMQ connection is being shutting down.");
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel?.BasicPublish(exchange: EXCHANGE_NAME,
                routingKey: "",
                basicProperties: null,
                body: body);
        }
    }
}