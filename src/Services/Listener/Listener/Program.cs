using Listener.Consumer;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("-------------------\n Listener service is up ! \n-------------------\n Press any key to exit...\n\n");

var consumer = new SendMessageConsumer(GetConnectionFactory(config));

await Task.Run(() => Console.Read());

static ConnectionFactory GetConnectionFactory(IConfigurationRoot? config)
{
    var factory = new ConnectionFactory()
    {
        HostName = config?["EventBus:HostName"]
    };

    if (!string.IsNullOrEmpty(config?["EventBus:UserName"]))
    {
        factory.UserName = config?["EventBus:UserName"];
        factory.Password = config?["EventBus:Password"];
    }

    return factory;
}