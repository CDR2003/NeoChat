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
        await connection.StartAsync();
        C.WriteLine( "Connected." );

        var line = C.ReadLine() ?? "";
        var nickname = line.Trim();
        var proxy = connection.CreateHubProxy<IChatHub>();
        await proxy.SetNickname( nickname );
        
        C.WriteLine( "Nickname set. Type a message to send." );
    }
}