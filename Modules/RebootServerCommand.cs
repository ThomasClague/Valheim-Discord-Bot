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
    private readonly IBashService _bashService;

    public RebootServerCommand(
        IOptions<CloudflareConfiguration> cloudflareConfiguration,
        ISshService sshService,
        IBashService bashService)
    {
        _cloudflareConfiguration = cloudflareConfiguration.Value;
        _sshService = sshService;
        _bashService = bashService;
    }

    [Command("rebootserver", RunMode = RunMode.Async)]
    public async Task RebootServer()
    {
        Context.Message.ReplyAsync("Attempting to reboot the server").ConfigureAwait(false).GetAwaiter().GetResult();
        var message = "";

        try
        {
            await _bashService.RunBashScript("sudo reboot");
            message = "The server was rebooted";
        }
        catch (Exception ex)
        {
            message = $"The server failed to reboot: {ex}";
        }

        await Context.Message.ReplyAsync(message);
    }
    ////SSH version
    //[Command("rebootserver", RunMode = RunMode.Async)]
    //public async Task RebootServer()
    //{
    //    var message = "";

    //    try
    //    {
    //        _sshService.RunCommand("sudo rebbot");
    //        message = "The server was rebooted";
    //    }
    //    catch (Exception ex)
    //    {
    //        message = $"The server failed to reboot: {ex}";
    //    }

    //    await Context.Message.ReplyAsync(message);
    //}
}