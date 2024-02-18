using ChatShared.Data;

namespace ChatGrainInterfaces;

public interface IUser : IGrainWithStringKey
{
    Task SetNickname( string nickname );
    
    Task JoinRoom( string roomName );
    
    Task LeaveRoom();
    
    Task SendMessage( string message );
    
    Task OnMessageReceived( ChatMessage message );
}