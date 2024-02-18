using ChatGrainInterfaces;

namespace ChatGrains;

public class UserGrain : Grain, IUser
{
    private string _nickname = "";
    
    public Task SetNickname( string nickname )
    {
        _nickname = nickname;
        Console.WriteLine( $"Setting nickname for {this.GetPrimaryKeyString()} to '{nickname}'" );
        return Task.CompletedTask;
    }

    public Task JoinRoom( string roomName )
    {
        return Task.CompletedTask;
    }

    public Task LeaveRoom( string roomName )
    {
        return Task.CompletedTask;
    }

    public Task SendMessage( string message )
    {
        return Task.CompletedTask;
    }
}