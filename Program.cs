using Discord;
using Discord.Commands;
using Discord.WebSocket;
using ValheimDiscordBot.Common;
using ValheimDiscordBot.Init;
using ValheimDiscordBot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables()
    .Build();
var client = new DiscordShardedClient();


var commands = new CommandService(new CommandServiceConfig
{
    // Again, log level:
    LogLevel = LogSeverity.Info,

    // There's a few more properties you can set,
    // for example, case-insensitive commands.
    CaseSensitiveCommands = false,
});


// Setup your DI container.
Bootstrapper.Init();
Bootstrapper.RegisterInstance(client);
Bootstrapper.RegisterInstance(commands);
Bootstrapper.RegisterOptions<CloudflareConfiguration>(config.GetRequiredSection("CloudflareSettings"));
Bootstrapper.RegisterOptions<SshConfiguration>(config.GetRequiredSection("SshSettings"));
Bootstrapper.RegisterType<ICommandHandler, CommandHandler>();
Bootstrapper.RegisterType<IValheimService, ValheimService>();
Bootstrapper.RegisterType<ISshService, SshService>();
Bootstrapper.RegisterType<IBashService, BashService>();
Bootstrapper.RegisterInstance(config);

await MainAsync();

async Task MainAsync()
{
    await Bootstrapper.ServiceProvider.GetRequiredService<ICommandHandler>().InitializeAsync();

    client.ShardReady += async shard =>
    {
        await Logger.Log(LogSeverity.Info, "ShardReady", $"Shard Number {shard.ShardId} is connected and ready!");
    };

    // Login and connect.
    var token = config.GetRequiredSection("Settings")["DiscordBotToken"];
    if (string.IsNullOrWhiteSpace(token))
    {
        await Logger.Log(LogSeverity.Error, $"{nameof(Program)} | {nameof(MainAsync)}", "Token is null or empty.");
        return;
    }

    await client.LoginAsync(TokenType.Bot, token);
    await client.StartAsync();

    // Wait infinitely so your bot actually stays connected.
    await Task.Delay(Timeout.Infinite);
}