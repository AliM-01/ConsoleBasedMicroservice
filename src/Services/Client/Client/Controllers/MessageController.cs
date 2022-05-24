using EventBus.RabbitMQ.Constants;
using EventBus.RabbitMQ.Producers;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;
[Route("api/msg")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly ISendMessageProducer _producer;

    public MessageController(ISendMessageProducer producer)
    {
        _producer = producer;
    }

    [HttpPost]
    public IActionResult SendMessage([FromQuery] string msg, [FromQuery] string email)
    {
        var publishModel = new EventBus.RabbitMQ.Events.SendMessageEvent(msg, email);

        _producer.PublishSendMessage(EventBusConstants.SendMessageQueue, publishModel);

        return Ok();
    }
}
