using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimDiscordBot.Common
{
    public class SshConfiguration
    {
        public string host { get; set; }
        public int port { get; set; }
        public string user_id { get; set; }
        public string password { get; set; }
    }
}
