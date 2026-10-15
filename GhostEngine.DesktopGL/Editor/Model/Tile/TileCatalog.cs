using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model
{
    internal static class TileCatalog
    {
        public static List<TileDefinition> All { get; } = new List<TileDefinition>
        {
            new TileDefinition { Id = 1, Name = "Floor",    Category = "Terrain",   PreviewColor = new Color(120, 130, 146), Collision = TileCollision.Impassable },
            new TileDefinition { Id = 2, Name = "Platform", Category = "Platforms", PreviewColor = new Color(160, 110, 60),  Collision = TileCollision.Platform },
            new TileDefinition { Id = 3, Name = "Spikes",   Category = "Hazards",   PreviewColor = new Color(205, 70, 70),   Collision = TileCollision.Passable },
        };

        public static TileDefinition GetById(int id)
        {
            foreach (var tile in All)
            {
                if (tile.Id == id)
                {
                    return tile;
                }
            }

            return null;
        }
    }
}
