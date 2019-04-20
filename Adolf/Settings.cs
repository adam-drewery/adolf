using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Adolf
{
    public class Settings
    {
        public Settings(Uri url, string token, string project)
        {
            Url = url;
            Token = token;
            Project = project;
        }

        public Uri Url { get; private set; }

        public string Token { get; private set; }
        
        public string Project { get; private set; }

        private const string FileName = "adolf.cfg";

        public static Settings Load()
        {
            if (!File.Exists(FileName))
                throw new InvalidDataException("Couldn't find authentication settings. Please set them with the \"auth\" command.");

            // TODO: Find better way to store this?
            var base64 = File.ReadAllText(FileName);
            var bytes = Convert.FromBase64String(base64);
            var text = Encoding.UTF8.GetString(bytes);
            var split = text.Split('|');

            var parts = new
            {
                url = split.ElementAtOrDefault(0),
                token = split.ElementAtOrDefault(1),
                project = split.ElementAtOrDefault(2)
            };
            
            return new Settings(parts.url == null ? null : new Uri(parts.url), parts.token, parts.project);
        }

        public void Save()
        {
            if (File.Exists(FileName))
            {
                var existing = Load();

                if (Url == null) Url = existing.Url;
                if (Token == null) Token = existing.Token;
                if (Project == null) Project = existing.Project;
            }
            
            var bytes = Encoding.UTF8.GetBytes(Url.ToString() + '|' + Token + '|' + Project);
            var text = Convert.ToBase64String(bytes);
            File.WriteAllText(FileName, text);
        }
    }
}