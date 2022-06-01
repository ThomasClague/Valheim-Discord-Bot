using Microsoft.Extensions.Options;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValheimDiscordBot.Common;

namespace ValheimDiscordBot.Services
{
    public class SshService : ISshService
    {
        private SshClient client;
        private readonly SshConfiguration _sshSettings;
        public SshService(IOptions<SshConfiguration> sshSettings)
        {
            _sshSettings = sshSettings.Value;
            client = new SshClient(_sshSettings.host, _sshSettings.port, _sshSettings.user_id, _sshSettings.password);
        }
        private void Connect()
        {
            try
            {
                if (!client.IsConnected)
                {
                    client.ConnectionInfo.Timeout = TimeSpan.FromMinutes(1);
                    client.Connect();
                    Console.WriteLine("SFTP client connected successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Disconnect()
        {
            try
            {
                if (client.IsConnected)
                {
                    client.Disconnect();
                    Console.WriteLine("SFTP client disconnected successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Run(string command)
        {
            try
            {
                client.RunCommand(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }


        public void RunCommand(string command)
        {
            if (!client.IsConnected)
            {
                Connect();
            }

            try
            {
                Run(command);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Disconnect();
            }
        }

        public void RunCommands(List<string> commands)
        {
            if (!client.IsConnected)
            {
                Connect();
            }

            try
            {
                foreach (string command in commands)
                {
                    Run(command);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
