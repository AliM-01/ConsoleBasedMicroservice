using EventBus.RabbitMQ.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;
[Route("api/msg")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IPublishEndpoint _publisher;

    public MessageController(IPublishEndpoint publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromQuery] string msg, [FromQuery] string email)
    {
        var publishModel = new SendMessageEvent(msg, email);

        await _publisher.Publish<SendMessageEvent>(publishModel);

        return Ok();
    }
}
