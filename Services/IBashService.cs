using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimDiscordBot.Services
{
    public interface IBashService
    {
        Task RunBashScript(string script);
    }
}
