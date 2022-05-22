using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Constants;
using EventBus.RabbitMQ.Producers;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRabbitMQConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = builder.Configuration["EventBus:HostName"]
    };

    if (!string.IsNullOrEmpty(builder.Configuration["EventBus:UserName"]))
    {
        factory.UserName = builder.Configuration["EventBus:UserName"];
        factory.Password = builder.Configuration["EventBus:Password"];
    }

    return new RabbitMQConnection(factory);
});

builder.Services.AddSingleton<ISendMessageProducer, SendMessageProducer>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/send", ([FromQuery] string msg, [FromQuery] string email, [FromServices] ISendMessageProducer producer) =>
{
    var publishModel = new EventBus.RabbitMQ.Events.SendMessageEvent(msg, email);

    producer.PublishSendMessage(EventBusConstants.SendMessageQueue, publishModel);

    return Results.Ok;
})
.WithName("SendMsg");

app.Run();