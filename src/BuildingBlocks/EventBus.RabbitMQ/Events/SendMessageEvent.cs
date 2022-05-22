namespace EventBus.RabbitMQ.Events;
public class SendMessageEvent
{
    public SendMessageEvent(string msg, string email)
    {
        Message = msg;
        Email = email;
        CreatedOn = DateTime.UtcNow;
    }

    public string Message { get; set; }

    public string Email { get; set; }

    public DateTime CreatedOn { get; set; }
}
