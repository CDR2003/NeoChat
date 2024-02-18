using ChatShared.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using TypedSignalR.Client;

namespace ChatClient.Console;

using C = System.Console;

class Program
{
    static async Task Main( string[] args )
    {
        var connection = new HubConnectionBuilder().WithUrl( "http://localhost:5053/Chat" ).Build();
        
        C.Write( "Connecting to server... " );
        
        for( ;; )
        {
            try
            {
                await connection.StartAsync();
                break;
            }
            catch( Exception e )
            {
                C.WriteLine( "Failed to connect to server: " + e.Message );
                C.WriteLine( "Retrying..." );
            }
        }
        
        C.WriteLine( "Connected." );

        var proxy = connection.CreateHubProxy<IChatHub>();
        var receiver = new ChatReceiver();
        connection.Register<IChatReceiver>( receiver );

        C.Write( "Please enter a nickname: " );

        var line = C.ReadLine() ?? "";
        var nickname = line.Trim();
        await proxy.SetNickname( nickname );
        
        C.WriteLine( "Nickname set. Type a message to send." );
        
        for( ;; )
        {
            try
            {
                line = C.ReadLine() ?? "";
                if( line.StartsWith( "/join", StringComparison.OrdinalIgnoreCase ) )
                {
                    var parts = line.Split( ' ' );
                    if( parts.Length < 2 )
                    {
                        C.WriteLine( "Usage: /join <room>" );
                        continue;
                    }

                    var room = parts[1];
                    await proxy.JoinRoom( room );
                    continue;
                }

                if( line.StartsWith( "/leave", StringComparison.OrdinalIgnoreCase ) )
                {
                    await proxy.LeaveRoom();
                    continue;
                }

                if( line.StartsWith( "/quit", StringComparison.OrdinalIgnoreCase ) )
                {
                    break;
                }

                await proxy.SendMessage( line );
            }
            catch( Exception e )
            {
                C.WriteLine( "Error: " + e.Message );
            }
        }
    }
}