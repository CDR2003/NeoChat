using ChatServer;

var clientBuilder = Host.CreateDefaultBuilder( args )
    .UseOrleansClient( client => client.UseLocalhostClustering() )
    .UseConsoleLifetime();

var host = clientBuilder.Build();
await host.StartAsync();

var builder = WebApplication.CreateBuilder( args );
builder.Services.AddSignalR();
builder.Services.AddCors();
builder.Services.AddSingleton( host.Services.GetRequiredService<IClusterClient>() );

var app = builder.Build();
app.UseCors( b => b.WithOrigins( "http://localhost:5053" ).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() );
app.MapHub<ChatHub>( "/Chat" );

app.Run();