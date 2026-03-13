using RabbitMQ.Client;
using System.Text;

namespace OrderService.Services
{
    public class RabbitMQPublisher
    {
        private readonly string _hostname;
        private readonly string _queueName;

        public RabbitMQPublisher(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
        }

        public void Publish(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = "guest",
                Password = "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: _queueName,
                basicProperties: null,
                body: body
            );

            Console.WriteLine($"Message published to queue: {_queueName}");
        }
    }
}