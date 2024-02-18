using ChatShared.Data;

namespace ChatShared.Interfaces;

public interface IChatReceiver
{
    Task ReceiveMessage( ChatMessage message );
}