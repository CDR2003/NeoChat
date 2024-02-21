using Orleans.Concurrency;

namespace ChatGrainInterfaces;

public interface IRoom : IGrainWithStringKey
{
    Task Join( IUser user );
    
    Task Leave( IUser user );
    
    [OneWay]
    Task SendMessage( IUser user, string message, IUser? except = null );
}