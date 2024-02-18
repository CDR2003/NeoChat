namespace ChatGrainInterfaces;

public interface IUser : IGrainWithStringKey
{
    Task JoinRoom( string roomName );
    
    Task LeaveRoom( string roomName );
    
    Task SendMessage( string message );
}