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
    private readonly IBashService _bashService;

    public RebootServerCommand(IBashService bashService)
    {
        _bashService = bashService;
    }

    [Command("rebootserver", RunMode = RunMode.Async)]
    public async Task RebootServer()
    {
        await Context.Message.ReplyAsync("Attempting to reboot the server");
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