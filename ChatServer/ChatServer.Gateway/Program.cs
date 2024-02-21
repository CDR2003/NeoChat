using ChatServer;

var clientBuilder = Host.CreateDefaultBuilder( args )
    .UseOrleansClient( client => client.UseLocalhostClustering() )
    .UseConsoleLifetime();

var host = clientBuilder.Build();

for( ;; )
{
    try
    {
        await host.StartAsync();
        break;
    }
    catch( Exception e )
    {
        Console.WriteLine( "Failed to connect to silo: " + e.Message );
        Console.WriteLine( "Retrying..." );
    }
}

var builder = WebApplication.CreateBuilder( args );
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddSingleton( host.Services.GetRequiredService<IClusterClient>() );
builder.Services.AddHostedService<ChatObserver>();

var app = builder.Build();
app.UseCors( b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );
app.MapHub<ChatHub>( "/Chat" );

app.Run();