using GhostEngine.DesktopGL.Editor.Model;
using GhostEngine.DesktopGL.Editor.Model.Objects;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace GhostEngine.DesktopGL.Editor.SaveLoadData
{
    // Level <-> .json file
    internal static class SaveLevel
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions { WriteIndented = true };

        // true if the file was written
        public static bool Save(Level level, string path)
        {
            if (level == null || string.IsNullOrEmpty(path))
            {
                return false;
            }
            var data = new LevelData
            {
                Width = level.Width,
                Height = level.Height,
                Tiles = new int[level.Width * level.Height],
            };
            for (int y = 0; y < level.Height; y++)
            {
                for(int x=0; x < level.Width; x++)
                {
                    data.Tiles[y * level.Width + x] = level.GetTile(x, y)?.Id ?? 0;
                }
            }
            foreach(GameObject gameObject in level.Objects)
            {
                data.Objects.Add(new ObjectData
                {
                    Id = gameObject.Definition.Id,
                    X = gameObject.Cell.X,
                    Y = gameObject.Cell.Y
                });
            }
            try
            {
                File.WriteAllText(path, JsonSerializer.Serialize(data, Options));
                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine($"Level save failed: {ex.Message}");
                return false;
            }
        }

        // Builds a new level from the file; null if the file can't be read or isn't a level
        public static Level Load(string path)
        {
            LevelData data;
            try
            {
                data = JsonSerializer.Deserialize<LevelData>(File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Level load failed: {ex.Message}");
                return null;
            }

            if (data == null || data.Width <= 0 || data.Height <= 0)
            {
                return null;
            }

            var level = new Level(data.Width, data.Height);

            if (data.Tiles != null)
            {
                for (int y = 0; y < data.Height; y++)
                {
                    for (int x = 0; x < data.Width; x++)
                    {
                        int index = y * data.Width + x;
                        if (index < data.Tiles.Length)
                        {
                            // Unknown or 0 Id -> null -> empty cell
                            level.SetTile(x, y, TileCatalog.GetById(data.Tiles[index]));
                        }
                    }
                }
            }

            if (data.Objects != null)
            {
                foreach (ObjectData objectData in data.Objects)
                {
                    // PlaceObject ignores a null type (unknown Id) and cells outside the level
                    level.PlaceObject(objectData.X, objectData.Y, ObjectCatalog.GetById(objectData.Id));
                }
            }

            return level;
        }
    }
}
