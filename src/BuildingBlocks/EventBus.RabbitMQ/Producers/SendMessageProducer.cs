using EventBus.RabbitMQ.Events;

namespace EventBus.RabbitMQ.Producers;

public interface ISendMessageProducer
{
    Task PublishSendMessage(string queueName, SendMessageEvent publishModel);
}

public class SendMessageProducer : ISendMessageProducer
{
    private readonly IRabbitMQConnection _connection;

    public SendMessageProducer(IRabbitMQConnection connection)
    {
        _connection = connection;
    }

    public Task PublishSendMessage(string queueName, SendMessageEvent publishModel)
    {
        using (var channel = _connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            string message = System.Text.Json.JsonSerializer.Serialize(publishModel);
            byte[] body = System.Text.Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.DeliveryMode = 2;

            channel.ConfirmSelect();
            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: true,
                    basicProperties: properties, body: body);
            channel.WaitForConfirmsOrDie();

            channel.ConfirmSelect();
        }

        return Task.CompletedTask;
    }

}
