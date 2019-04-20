using System;
using System.IO;
using System.Text;

namespace Adolf
{
    public class Settings
    {
        public Settings(Uri url, string token)
        {
            Url = url;
            Token = token;
        }

        public Uri Url { get;  }

        public string Token { get; }

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
            return new Settings(new Uri(split[0]), split[1]);
        }

        public void Save()
        {
            var bytes = Encoding.UTF8.GetBytes(Url.ToString() + '|' + Token);
            var text = Convert.ToBase64String(bytes);
            File.WriteAllText(FileName, text);
        }
    }
}