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
    
    public Task SendMessage( string message )
    {
        Console.WriteLine( $"Sending message from {Context.ConnectionId}: {message}" );
        return Task.CompletedTask;
    }
}