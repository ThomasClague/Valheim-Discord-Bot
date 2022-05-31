using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValheimDiscordBot.Common
{
    public class CloudflareConfiguration
    {
        public string zone_id { get; set; }
        public string email { get; set; }
        public string access_key { get; set; }
        public string record_name { get; set; }
    }
}
