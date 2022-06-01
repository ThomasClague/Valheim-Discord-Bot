using Discord;
using Discord.Commands;
using Microsoft.Extensions.Options;
using ValheimDiscordBot.Common;
using ValheimDiscordBot.Services;
using RunMode = Discord.Commands.RunMode;

namespace ValheimDiscordBot.Modules;

public class RebootServerCommand : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    private readonly CloudflareConfiguration _cloudflareConfiguration;
    private readonly ISshService _sshService;

    public RebootServerCommand(IOptions<CloudflareConfiguration> cloudflareConfiguration, ISshService sshService)
    {
        _cloudflareConfiguration = cloudflareConfiguration.Value;
        _sshService = sshService;
    }

    [Command("rebootserver", RunMode = RunMode.Async)]
    public async Task RebootServer()
    {
        await Context.Message.ReplyAsync("Attempying to reboot the server");
        var message = "";

        try
        {
            _sshService.RunCommand("sudo rebbot");
            message = "The server was rebooted";
        }
        catch (Exception ex)
        {
            message = $"The server failed to reboot: {ex}";
        }

        await Context.Message.ReplyAsync(message);
    }

}