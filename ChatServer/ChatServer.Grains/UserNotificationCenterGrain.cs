using ChatGrainInterfaces;
using ChatShared.Data;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Utilities;

namespace ChatGrains;

[StatelessWorker]
public class UserNotificationCenterGrain : Grain, IUserNotificationCenter
{
    private readonly ObserverManager<IUserObserver> _observerManager;

    public UserNotificationCenterGrain( ILogger<UserGrain> logger )
    {
        _observerManager = new ObserverManager<IUserObserver>( TimeSpan.FromMinutes( 5 ), logger );
    }

    public Task Subscribe( IUserObserver observer )
    {
        _observerManager.Subscribe( observer, observer );
        return Task.CompletedTask;
    }

    public Task Unsubscribe( IUserObserver observer )
    {
        _observerManager.Unsubscribe( observer );
        return Task.CompletedTask;
    }

    public async Task NotifySendMessage( string connectionId, ChatMessage message )
    {
        await _observerManager.Notify( o => o.ReceiveMessage( connectionId, message ) );
        Console.WriteLine( $"User {this.GetPrimaryKeyString()} received message: {message}" );
    }
}