using EventBus.RabbitMQ.Constants;
using Listener.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("-------------------\n Listener service is up ! \n-------------------\n Press any key to exit...\n\n");

var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
{
    busConfig.Host(config.GetConnectionString("EventBus"));

    busConfig.ReceiveEndpoint(EventBusConstants.SendMessageQueue, e =>
    {
        e.Consumer<SendMessageConsumer>();
    });
});

await bus.StartAsync();

Console.ReadLine();