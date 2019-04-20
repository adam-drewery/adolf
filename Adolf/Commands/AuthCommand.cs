using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Adolf.Attributes;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace Adolf.Commands
{
    [Alias("auth")]
    public class AuthCommand : Command
    {
        public string Url { get; set; }

        public string Token { get; set; }

        public override Task Execute()
        {
            var uri = new Uri(Url);
            var credential = new VssBasicCredential(string.Empty, Token);
            var connection = new VssConnection(uri, credential);
            var witClient = connection.GetClient<ProjectHttpClient>();
            Task.Run(async () => await witClient.GetProjects()).Wait();

            var text = Convert.ToBase64String(Encoding.UTF8.GetBytes(Url + '|' + Token));
            File.WriteAllText("adolf.cfg", text);

            Console.WriteLine("Settings saved successfully");

            return Task.CompletedTask;
        }
    }
}