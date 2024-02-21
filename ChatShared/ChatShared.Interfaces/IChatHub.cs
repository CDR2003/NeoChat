namespace ChatShared.Interfaces;

public interface IChatHub
{
    Task SetNickname( string nickname );
    
    Task<List<string>> GetRooms();
    
    Task JoinRoom( string roomName );

    Task LeaveRoom();

    Task SendMessage( string message );
}