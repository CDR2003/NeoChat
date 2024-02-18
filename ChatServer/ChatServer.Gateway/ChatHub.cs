using ChatGrainInterfaces;
using ChatShared.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer;

public class ChatHub : Hub<IChatHub>
{
    private readonly IClusterClient _client;

    public ChatHub( IClusterClient client )
    {
        _client = client;
    }
    
    public async Task SetNickname( string nickname )
    {
        var user = _client.GetGrain<IUser>( this.Context.ConnectionId );
        await user.SetNickname( nickname );
        
        Console.WriteLine( $"Setting nickname for {Context.ConnectionId} to {nickname}" );
    }
    
    public async Task JoinRoom( string roomName )
    {
        var user = _client.GetGrain<IUser>( this.Context.ConnectionId );
        await user.JoinRoom( roomName );
        
        Console.WriteLine( $"User {Context.ConnectionId} joined room {roomName}" );
    }

    public async Task LeaveRoom()
    {
        var user = _client.GetGrain<IUser>( this.Context.ConnectionId );
        await user.LeaveRoom();
        
        Console.WriteLine( $"User {Context.ConnectionId} left room" );
    }
    
    public async Task SendMessage( string message )
    {
        var user = _client.GetGrain<IUser>( this.Context.ConnectionId );
        await user.SendMessage( message );
        
        Console.WriteLine( $"User {Context.ConnectionId} sent message: {message}" );
    }
}