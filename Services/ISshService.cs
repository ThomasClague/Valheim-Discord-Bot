using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimDiscordBot.Services
{
    public interface ISshService
    {
        void RunCommands(List<string> commands);
        void RunCommand(string command);
    }
}
