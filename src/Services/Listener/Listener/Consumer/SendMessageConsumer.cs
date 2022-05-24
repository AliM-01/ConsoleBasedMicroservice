using EventBus.RabbitMQ.Constants;
using EventBus.RabbitMQ.Events;
using RabbitMQ.Client.Events;

namespace Listener.Consumer;

public static class SendMessageConsumer
{
    public static async Task HandleReceived(object model, BasicDeliverEventArgs ea)
    {
        if (ea.RoutingKey.Equals(EventBusConstants.SendMessageQueue))
        {
            string message = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

            var sendMessageEvent = System.Text.Json.JsonSerializer.Deserialize<SendMessageEvent>(message);

            await Console.Out.WriteLineAsync($"{sendMessageEvent?.Message} was sent to {sendMessageEvent?.Email}");
        }
    }
}
