using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace GhostEngine.DesktopGL.Editor.Config
{
    internal class SaveLoadConfig
    {
        private const string FolderName = "Editor/Config";
        private const string FileName = "Data.json";

        private static readonly string DataFilePath = Path.Combine(FolderName, FileName);

        public class EngineData
        {
            public bool IsDarkMode { get; set; }
        }

        public static void SaveData(EngineData settings)
        {
            string directoryPath = Path.GetDirectoryName(DataFilePath);
            
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                
            }

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(settings, options);
            
            File.WriteAllText(DataFilePath, jsonString);
        }

        public static EngineData LoadData()
        {
            if (!File.Exists(DataFilePath))
            {
                return new EngineData();
            }
            string jsonString = File.ReadAllText(DataFilePath);
            return JsonSerializer.Deserialize<EngineData>(jsonString) ?? new EngineData();
        }

        public static void Check()
        {
            if (!File.Exists(DataFilePath))
            {
                var defaultData = new EngineData
                {
                    IsDarkMode = false
                };
                SaveData(defaultData); 
            }
        }
    }
}