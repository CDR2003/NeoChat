namespace ChatGrainInterfaces;

public interface IRoomManager : IGrainWithIntegerKey
{
    Task<List<string>> GetRoomNames();
    
    Task AddRoom( string roomName );
    
    Task RemoveRoom( string roomName );
}