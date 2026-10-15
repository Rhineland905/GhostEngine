using Microsoft.Xna.Framework;

namespace GhostEngine.DesktopGL.Editor.Model.Objects
{
    internal class GameObjectDefinition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } 
        public Color PreviewColor { get; set; }
        public bool IsUnique { get; set; }
    }
}
