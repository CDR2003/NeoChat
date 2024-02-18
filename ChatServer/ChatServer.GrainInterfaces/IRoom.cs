namespace ChatGrainInterfaces;

public interface IRoom : IGrainWithStringKey
{
    Task Join( IUser user );
    
    Task Leave( IUser user );
    
    Task SendMessage( IUser user, string message, IUser? except = null );
}