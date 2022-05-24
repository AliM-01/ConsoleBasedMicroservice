using EventBus.RabbitMQ.Events;
using MassTransit;

namespace Listener.Consumers;
public class SendMessageConsumer : IConsumer<SendMessageEvent>
{
    public async Task Consume(ConsumeContext<SendMessageEvent> context)
    {
        await Console.Out.WriteLineAsync($"{context.Message.Message} was sent to {context.Message.Email}");
    }
}
