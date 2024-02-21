using ChatShared.Data;

namespace ChatGrainInterfaces;

public interface IUserNotificationCenter : IGrainWithIntegerKey
{
    Task Subscribe( IUserObserver observer );
    
    Task Unsubscribe( IUserObserver observer );
    
    Task NotifySendMessage( string connectionId, ChatMessage message );
}