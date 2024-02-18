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
    
    public Task SetNickname( string nickname )
    {
        Console.WriteLine( $"Setting nickname for {Context.ConnectionId} to {nickname}" );
        return Task.CompletedTask;
    }
    
    public Task SendMessage( string message )
    {
        Console.WriteLine( $"Sending message from {Context.ConnectionId}: {message}" );
        return Task.CompletedTask;
    }
}