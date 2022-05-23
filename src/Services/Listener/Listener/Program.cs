using Listener.Consumer;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("-------------------\n Listener service is up ! \n-------------------\n Press any key to exit...\n\n");

var consumer = new SendMessageConsumer(new ConnectionFactory()
    {
        HostName = config?["EventBus:HostName"],
        UserName = config?["EventBus:UserName"],
        Password = config?["EventBus:Password"]
    });

while (true) 
{
      System.Console.ReadLine();                
};