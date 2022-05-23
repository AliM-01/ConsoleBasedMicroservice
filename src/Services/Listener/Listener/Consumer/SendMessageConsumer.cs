using System.Text;
using System.Text.Json;
using EventBus.RabbitMQ.Constants;
using EventBus.RabbitMQ.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Listener.Consumer;

public class SendMessageConsumer
{
    private readonly ConnectionFactory _factory;

    public SendMessageConsumer(ConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task Consume()
    {
        using (var connection = _factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: EventBusConstants.SendMessageQueue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                var result = channel.BasicGet(EventBusConstants.SendMessageQueue, true);
                if (result != null)
                {
                    string message = Encoding.UTF8.GetString(result.Body.Span);
                    var sendMessageEvent = JsonSerializer.Deserialize<SendMessageEvent>(message);

                    await Console.Out.WriteLineAsync($"{sendMessageEvent?.Message} was sent to {sendMessageEvent?.Email}");
                }
            }
        }
    }
}
