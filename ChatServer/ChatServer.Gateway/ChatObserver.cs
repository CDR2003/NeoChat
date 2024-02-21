using ChatGrainInterfaces;
using ChatShared.Data;
using ChatShared.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer;

public class ChatObserver : IHostedService, IUserObserver
{
    private readonly IClusterClient _client;

    private readonly IHubContext<ChatHub, IChatReceiver> _context;
    
    private readonly IUserObserver _reference;
    
    public ChatObserver( IClusterClient client, IHubContext<ChatHub, IChatReceiver> context )
    {
        _client = client;
        _context = context;
        _reference = _client.CreateObjectReference<IUserObserver>( this );
    }
    
    public async Task ReceiveMessage( string connectionId, ChatMessage message )
    {
        var connection = _context.Clients.Client( connectionId );
        await connection.ReceiveMessage( message );
        Console.WriteLine( $"User {connectionId} received message: {message}" );
    }

    public async Task StartAsync( CancellationToken cancellationToken )
    {
        var user = _client.GetGrain<IUserNotificationCenter>( 0 );
        await user.Subscribe( _reference );
    }

    public async Task StopAsync( CancellationToken cancellationToken )
    {
        var user = _client.GetGrain<IUserNotificationCenter>( 0 );
        await user.Unsubscribe( _reference );
    }
}