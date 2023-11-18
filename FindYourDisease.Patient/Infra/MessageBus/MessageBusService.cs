using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace FindYourDisease.Patient.Infra.MessageBus;

public class MessageBusService : IMessageBusService
{
    private readonly ConnectionFactory _factory;
    public MessageBusService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
    }

    public void Publish<T>(string queue, T message) where T : class
    {
        using (var connection = _factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: queue,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queue,
                    basicProperties: null,
                    body: GetMessageAsByteArray(message));
            }
        }
    }

    private byte[] GetMessageAsByteArray<T>(T message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var json = JsonSerializer.Serialize(message, options);
        return Encoding.UTF8.GetBytes(json);
    }
}
