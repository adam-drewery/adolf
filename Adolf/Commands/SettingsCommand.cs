using System;
using System.Threading.Tasks;
using Adolf.Attributes;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace Adolf.Commands
{
    [Alias("set")]
    public class SettingsCommand : Command
    {
        public string Url { get; set; }

        public string Token { get; set; }
        
        public string Project { get; set; }

        public override Task Execute()
        {
            // Test the url
            if (Url != null)
            {
                var uri = new Uri(Url);
                var credential = new VssBasicCredential(string.Empty, Token);
                var connection = new VssConnection(uri, credential);
                var witClient = connection.GetClient<ProjectHttpClient>();
                Task.Run(async () => await witClient.GetProjects()).Wait();
            }
            
            var settings = new Settings(Url == null ? null : new Uri(Url), Token, Project);
            settings.Save();

            Console.WriteLine("Settings saved successfully");

            return Task.CompletedTask;
        }
    }
}