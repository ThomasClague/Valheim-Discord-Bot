using Discord;
using Discord.Commands;
using Microsoft.Extensions.Options;
using ValheimDiscordBot.Common;
using ValheimDiscordBot.Services;
using RunMode = Discord.Commands.RunMode;

namespace ValheimDiscordBot.Modules;

public class GetValheimIPCommand : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    private readonly CloudflareConfiguration _cloudflareConfiguration;
    private readonly IValheimService _valheimService;

    public GetValheimIPCommand(IOptions<CloudflareConfiguration> cloudflareConfiguration, IValheimService valheimService)
    {
        _cloudflareConfiguration = cloudflareConfiguration.Value;
        _valheimService = valheimService;
    }

    [Command("valheimip", RunMode = RunMode.Async)]
    public async Task ValheimIPCommand()
    {
        var message = "";
        var serverIp = await _valheimService.GetServerIp();
        if(!string.IsNullOrEmpty(serverIp))
        {
            message = $"Valheim server IP: {serverIp}";
        }
        else
        {
            message = "Valheim server IP could not be found";
        }

        await Context.Message.ReplyAsync(message);
    }
}