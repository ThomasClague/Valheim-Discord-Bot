using Discord;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ValheimDiscordBot.Common;

namespace ValheimDiscordBot.Services
{
    public class ValheimService : IValheimService
    {
        private readonly HttpClient _http;
        private readonly CloudflareConfiguration _cloudflareConfiguration;
        private readonly string url;
        public ValheimService(
            IOptions<CloudflareConfiguration> cloudflareConfiguration,
            IHttpClientFactory httpClientFactory
            )
        {
            _cloudflareConfiguration = cloudflareConfiguration.Value;
            _http = httpClientFactory.CreateClient();
            //_http.DefaultRequestHeaders.Add("X-Auth-Email", _cloudflareConfiguration.email);
            //_http.DefaultRequestHeaders.Add("X-Auth-Key", _cloudflareConfiguration.access_key);
            //_http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //url = $"https://api.cloudflare.com/client/v4/zones/{_cloudflareConfiguration.zone_id}/dns_records?type=A&name={_cloudflareConfiguration.record_name}";
            url = "https://ifconfig.me/ip";
        }
        public async Task<string> GetServerIp()
        {
            var response = await _http.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                await Logger.Log(LogSeverity.Info, "Cloudflare API", $"Cloudflare response: { responseBody }");
            }
            else
            {
                await Logger.Log(LogSeverity.Error, "Cloudflare API", $"Cloudflare api error: { response.StatusCode} { response.ReasonPhrase } { responseBody }");
                return null;
            }
            
            var IP = responseBody;

            if (IP == null)
            {
                await Logger.Log(LogSeverity.Error, "IFConfig", $"IFConfig api error: {response.StatusCode} {response.ReasonPhrase}: {response}");
                return null;
            }

            return IP;
        }
    }
}
