using ChatShared.Data;
using ChatShared.Interfaces;

using C = System.Console;

namespace ChatClient.Console;

public class ChatReceiver : IChatReceiver
{
    public Task ReceiveMessage( ChatMessage message )
    {
        C.WriteLine( $"[{message.SentTime}] {message.Sender}: {message.Text}" );
        return Task.CompletedTask;
    }
}