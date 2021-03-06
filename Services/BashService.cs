using Discord;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValheimDiscordBot.Common;

namespace ValheimDiscordBot.Services
{
    public class BashService : IBashService
    {
        private readonly ILogger<BashService> _logger;
        public BashService(ILogger<BashService> logger)
        {
            _logger = logger;
        }
        public async Task RunBashScript(string script)
        {
            try
            {
                var process = new Process();
                var processStartInfo = new ProcessStartInfo()
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = $"/bin/bash",
                    WorkingDirectory = "/",
                    Arguments = script,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                };
                process.StartInfo = processStartInfo;
                process.Start();

                String error = process.StandardError.ReadToEnd();
                String output = process.StandardOutput.ReadToEnd();

            }
            catch (Exception ex)
            {
                _logger.LogError($"exception thrown when running bash scripr: {ex}");
                await Logger.Log(LogSeverity.Error, "BashService", $"{ex.Message} - {ex}");
                throw;
            }
        }
    }
}
