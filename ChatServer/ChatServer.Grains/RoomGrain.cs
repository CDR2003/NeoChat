using ChatGrainInterfaces;
using ChatShared.Data;

namespace ChatGrains;

public class RoomGrain : Grain, IRoom
{
    private readonly HashSet<IUser> _members = new();

    private IUser? _system;

    public override async Task OnActivateAsync( CancellationToken cancellationToken )
    {
        await base.OnActivateAsync( cancellationToken );

        _system = this.GrainFactory.GetGrain<IUser>( "SYSTEM" );
        
        var primaryKey = this.GetPrimaryKeyString();
        var manager = this.GrainFactory.GetGrain<IRoomManager>( 0 );
        await manager.AddRoom( primaryKey );
    }

    public async Task Join( IUser user )
    {
        var added = _members.Add( user );
        if( added == false )
        {
            throw new InvalidOperationException( $"User {user.GetPrimaryKeyString()} already joined in room {this.GetPrimaryKeyString()}" );
        }
        
        await this.SendMessage( _system!, $"{user.GetPrimaryKeyString()} joined the room", user );
    }

    public async Task Leave( IUser user )
    {
        var removed = _members.Remove( user );
        if( removed == false )
        {
            throw new InvalidOperationException( $"User {user.GetPrimaryKeyString()} is not in room {this.GetPrimaryKeyString()}" );
        }

        if( _members.Count == 0 )
        {
            var manager = this.GrainFactory.GetGrain<IRoomManager>( 0 );
            await manager.RemoveRoom( this.GetPrimaryKeyString() );

            return;
        }
        
        await this.SendMessage( _system!, $"{user.GetPrimaryKeyString()} left the room" );
    }

    public Task SendMessage( IUser user, string message, IUser? except = null )
    {
        var timestamp = DateTime.Now;
        var chatMessage = new ChatMessage( user.GetPrimaryKeyString(), message, timestamp );
        
        foreach( var member in _members )
        {
            if( member == except )
            {
                continue;
            }
            
            member.OnMessageReceived( chatMessage );
        }

        return Task.CompletedTask;
    }
}