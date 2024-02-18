using ChatGrainInterfaces;

namespace ChatGrains;

public class RoomManagerGrain : Grain, IRoomManager
{
    private readonly HashSet<string> _roomNames = new();
    
    public Task<List<string>> GetRoomNames()
    {
        return Task.FromResult( _roomNames.ToList() );
    }

    public Task AddRoom( string roomName )
    {
        if( _roomNames.Add( roomName ) == false )
        {
            throw new InvalidOperationException( $"Room {roomName} already exists" );
        }

        return Task.CompletedTask;
    }

    public Task RemoveRoom( string roomName )
    {
        if( _roomNames.Remove( roomName ) == false )
        {
            throw new InvalidOperationException( $"Room {roomName} does not exist" );
        }
        
        return Task.CompletedTask;
    }
}