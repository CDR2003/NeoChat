namespace ChatShared.Interfaces;

public interface IChatHub
{
    Task SetNickname( string nickname );

    Task SendMessage( string message );
}