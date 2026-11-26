using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GhostEngine.DesktopGL.Editor.Settings
{
    internal class Localization
    {
        public static Dictionary<string, string> _dict = new();
        public static void load(string lan)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Content","Lan",lan+".json");

            if (!File.Exists(path)) {
                _dict = new();
                return;
            }
            try
            {
                string json = File.ReadAllText(path);
                _dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();

            }
            catch (JsonException) {
                _dict = new();
            }

        }
        
        public static string Get(string key)
        {
            return _dict.TryGetValue(key,out var value) ? value : key;
        }
    }
}
