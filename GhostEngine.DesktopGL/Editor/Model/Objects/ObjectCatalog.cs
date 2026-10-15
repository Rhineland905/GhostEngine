using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model.Objects
{
    internal static class ObjectCatalog
    {
        public static List<GameObjectDefinition> All { get; } = new List<GameObjectDefinition>
        {
            new GameObjectDefinition { Id = 1, Name = "Player Spawn", Category = "Player", PreviewColor = new Color(76, 217, 100), IsUnique = true },
            new GameObjectDefinition { Id = 2,Name = "Slime", Category = "Enemies",PreviewColor = new Color(170, 90, 230)},
            new GameObjectDefinition { Id = 3,Name = "Bat", Category = "Enemies",PreviewColor = new Color(240, 140, 50)},
            new GameObjectDefinition { Id = 4,Name = "Coin", Category = "Items",PreviewColor = new Color(255, 210, 60)},
        };

        public static GameObjectDefinition GetById(int id)
        {
            foreach (var gameObject in All)
            {
                if (gameObject.Id == id)
                {
                    return gameObject;
                }
            }

            return null;
        }
    }
}
