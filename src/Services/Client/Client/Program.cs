using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Producers;
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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();