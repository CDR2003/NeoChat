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
        
        C.Write( "Please enter a nickname: " );

        var line = C.ReadLine() ?? "";
        var nickname = line.Trim();
        var proxy = connection.CreateHubProxy<IChatHub>();
        await proxy.SetNickname( nickname );
        
        C.WriteLine( "Nickname set. Type a message to send." );
    }
}