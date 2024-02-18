using Orleans;

namespace ChatShared.Data;

[GenerateSerializer]
public class ChatMessage
{
    [Id( 0 )]
    public string Text { get; set; } = "";

    [Id( 1 )]
    public string Sender { get; set; } = "";

    [Id( 2 )]
    public DateTime SentTime { get; set; }

    public ChatMessage()
    {
    }
    
    public ChatMessage( string text, string sender, DateTime sentTime )
    {
        Text = text;
        Sender = sender;
        SentTime = sentTime;
    }

    public override string ToString()
    {
        return $"[{SentTime}] {Sender}: {Text}";
    }
}