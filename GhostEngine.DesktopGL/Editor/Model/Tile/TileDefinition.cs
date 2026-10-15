using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model
{
    internal class TileDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color PreviewColor { get; set; }
        public string Category { get; set; } 
        public TileCollision Collision { get; set; } = TileCollision.Impassable;
    }
}
