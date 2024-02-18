namespace ChatShared.Data;

public class ChatMessage
{
    public string Text { get; set; } = "";

    public string Sender { get; set; } = "";

    public DateTime SentTime { get; set; }
}