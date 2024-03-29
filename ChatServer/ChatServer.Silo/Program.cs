﻿using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder( args )
    .UseOrleans( silo => silo.UseLocalhostClustering() )
    .UseConsoleLifetime();

using var host = builder.Build();
await host.RunAsync();