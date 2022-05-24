using EventBus.RabbitMQ.Constants;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("-------------------\n Listener service is up ! \n-------------------\n Press any key to exit...\n\n");

var factory = new ConnectionFactory()
{
    HostName = config["EventBus:HostName"],
    UserName = config["EventBus:UserName"],
    Password = config["EventBus:Password"],
    DispatchConsumersAsync = true
};

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.QueueDeclare(queue: EventBusConstants.SendMessageQueue,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.Received += Listener.Consumer.SendMessageConsumer.HandleReceived;

channel.BasicConsume(queue: EventBusConstants.SendMessageQueue,
                     autoAck: true,
                     consumer: consumer);

System.Console.ReadLine();