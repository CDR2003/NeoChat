using ChatGrainInterfaces;
using ChatShared.Data;

namespace ChatGrains;

public class UserGrain : Grain, IUser
{
    private string _nickname = "";
    
    private IRoom? _currentRoom = null;
    
    public Task SetNickname( string nickname )
    {
        _nickname = nickname;
        Console.WriteLine( $"Setting nickname for {this.GetPrimaryKeyString()} to '{nickname}'" );
        return Task.CompletedTask;
    }

    public async Task JoinRoom( string roomName )
    {
        if( _currentRoom != null )
        {
            throw new InvalidOperationException( "User is already in a room" );
        }
        
        var room = this.GrainFactory.GetGrain<IRoom>( roomName );
        await room.Join( this );
        
        _currentRoom = room;
    }

    public async Task LeaveRoom()
    {
        if( _currentRoom == null )
        {
            throw new InvalidOperationException( "User is not in a room" );
        }
        
        var room = _currentRoom;
        _currentRoom = null;
        
        await room.Leave( this );
    }

    public async Task SendMessage( string message )
    {
        if( _currentRoom == null )
        {
            throw new InvalidOperationException( "User is not in a room" );
        }
        
        await _currentRoom.SendMessage( this, message );
    }

    public Task OnMessageReceived( ChatMessage message )
    {
        Console.WriteLine( $"User {this.GetPrimaryKeyString()} received message: {message}" );
        return Task.CompletedTask;
    }
}