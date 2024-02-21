using ChatShared.Data;
using Orleans.Concurrency;

namespace ChatGrainInterfaces;

public interface IUser : IGrainWithStringKey
{
    Task<string> GetNickname();
    
    Task SetNickname( string nickname );
    
    Task JoinRoom( string roomName );
    
    Task LeaveRoom();
    
    Task SendMessage( string message );
    
    [OneWay]
    Task OnMessageReceived( ChatMessage message );
}